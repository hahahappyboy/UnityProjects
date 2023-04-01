Shader "Custom/VFPhongNormalShader"
{
    Properties
    {
        _MainTexture("主纹理",2D)=""{}
        _MainColor("主颜色",Color)=(1,1,1,1)
        _BumpTexture("法线纹理",2D)="bump"{}
        _BumpScale("法线深度系数", Range(0, 1))=0.5
        _SpecularColor("高光反射颜色",Color)=(1,1,1,1)
        _Gloss("光晕系数",Range(4,256))=4
    }
    SubShader
    {
        Tags{ "LightMode" = "ForwardBase" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

            sampler2D _MainTexture;
            float4 _MainTexture_ST;
            
            fixed4 _MainColor;
            
            sampler2D _BumpTexture;
            float4 _BumpTexture_ST;
            float _BumpScale;
            
            fixed4 _SpecularColor;
            float _Gloss;
            
            struct c2v
            {
                float4 vertexPos : POSITION;
                float3 normal : NORMAL;
                float4 texcoodr:TEXCOORD0;
                float4 tangent : TANGENT;

            };
            struct v2f
            {
                float4 vertexClipPos : SV_POSITION;
            
                float2 uvNormalTexture : TEXCOORD1;
                float2 uvMainTexture : TEXCOORD2;

                float4 worldPos: TEXCOORD3;
                
                float3 matrixRow1 : TEXCOORD4;
                float3 matrixRow2 : TEXCOORD5;
                float3 matrixRow3 : TEXCOORD6;

            };


            v2f vert(c2v o)
            {
                v2f r;
                r.vertexClipPos = UnityObjectToClipPos(o.vertexPos);
                r.uvMainTexture = o.texcoodr.xy * _MainTexture_ST.xy +  _MainTexture_ST.zw;
                r.uvNormalTexture = o.texcoodr.xy * _BumpTexture_ST.xy +  _BumpTexture_ST.zw;
                r.worldPos = mul(unity_ObjectToWorld,o.vertexPos);
                //粗模顶点法线
                float3 worldNormal = mul((float3x3)unity_ObjectToWorld,o.normal);
                //粗模顶点切线
                float3 worldTangent = mul((float3x3)unity_ObjectToWorld,o.tangent.xyz);
                //粗模顶点副切线
                float3 worldBinormal = cross(worldNormal,worldTangent)*o.tangent.w;
                //切线空间->世界空间的变换矩阵
                r.matrixRow1 = float3(worldTangent.x,worldBinormal.x,worldNormal.x);
                r.matrixRow2 = float3(worldTangent.y,worldBinormal.y,worldNormal.y);
                r.matrixRow3 = float3(worldTangent.z,worldBinormal.z,worldNormal.z);
                return r;
            }

            fixed4 frag(v2f o) : SV_Target{
                //解出法线纹理
                fixed4 bumpColor = tex2D(_BumpTexture,o.uvNormalTexture);
                //解出法线纹理,这是对法线纹理的采样结果的一个反映射操作， normal.xy = packednormal.xy * 2 - 1;
                fixed3 bump = UnpackNormal(bumpColor);
                //法线深度系数
                bump *= _BumpScale;
                //得到副切线
				bump.z = sqrt(1 - max(0, dot(bump.xy, bump.xy)));
                bump = fixed3(
                    dot(o.matrixRow1,bump),
                    dot(o.matrixRow2,bump),
                    dot(o.matrixRow3,bump)
                    );
                //漫反射光照
                fixed4  mainTextureColor = tex2D(_MainTexture,o.uvMainTexture)* _MainColor;
                fixed3 diffuseColor = _LightColor0.rgb*mainTextureColor.rgb*  (dot(normalize(bump),normalize(_WorldSpaceLightPos0.xyz))*0.5+0.5);
                //高光反射
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz-o.worldPos.xyz);
                fixed3 reflectDir = normalize(reflect(normalize(-_WorldSpaceLightPos0.xyz),normalize(bump)));
                fixed3 specularColor = _LightColor0.rgb*_SpecularColor.rgb*pow(max(0,dot(viewDir,reflectDir)),_Gloss);
                
                fixed3 color = UNITY_LIGHTMODEL_AMBIENT.xyz * mainTextureColor.rgb + diffuseColor + specularColor;
                return fixed4(color,1);
            }
            
            ENDCG
            
        }
    }
}
