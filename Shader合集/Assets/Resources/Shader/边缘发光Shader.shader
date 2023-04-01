Shader "Unlit/边缘发光Shader"
{
    Properties
    {
        //主颜色 
		_MainColor("主颜色", Color) = (1,0,0,1)
        //边缘发光颜色
        _RimColor("边缘发光颜色", Color) = (0.5,0.5,0.5,1)
        //边缘发光强度
        _RimPower("边缘发光强度", Range(0,25)) = 0
        //边缘发光系数
        _RimIntensity("边缘发光系数", Range(0,100)) = 3
    }
    SubShader
    {
        //渲染类型为Opaque，不透明
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            Tags{ "LightMode" = "ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            //主颜色
            fixed3 _MainColor;
            //边缘光颜色
            fixed3 _RimColor;
            //边缘光强度
            float _RimPower;
            //边缘光强度系数
            float _RimIntensity;
            
            struct c2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float4 worldPos : TEXCOORD1;
            };


            v2f vert (c2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                //获取顶点在世界空间中的法线向量坐标
                o.worldNormal = mul((float3x3)unity_ObjectToWorld,v.normal);
                //获得顶点在世界空间中的位置坐标
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               
                //法线方向
                float3 worldNorm = normalize(i.worldNormal);
                //光照方向
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                //计算光源衰减
                float attenuation = LIGHT_ATTENUATION(i);
                //光源衰减后的颜色
                fixed3 lightColor = attenuation * _LightColor0.rgb;
                //漫反射
                fixed3 diffuseColor = lightColor * _MainColor.rgb * (dot(worldNorm,lightDir)*0.5+0.5);
                //视角方向
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                //边缘强度
                half rim = 1.0 - max(0, dot(i.worldNormal, viewDir));
                //边缘颜色
                fixed3 emissive = _RimColor.rgb * pow(rim,_RimPower) *_RimIntensity;
                //最终颜色 漫反射+自发光颜色+环境光
                fixed3 finalColor = diffuseColor + emissive + UNITY_LIGHTMODEL_AMBIENT.rgb;
                return fixed4(finalColor,1); 
            }
            ENDCG
        }
    }
}
