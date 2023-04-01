Shader "Custom/PhongShaderPiexl"
{
    Properties
    {
        _DiffuseColor("漫反射颜色",Color)=(1,0,0,1)
        _Gloss("光晕系数",Range(10,255))=0
        _HightLightColor("高光颜色",Color)=(1,0,0,1)
    }
    SubShader
    {
        Tags{"LightMode"="ForwardBase"}

        Pass
        {
           CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"
            float _Gloss;
            fixed4 _HightLightColor;
            fixed4 _DiffuseColor;
            struct c2v
            {
                float4 vertPos : POSITION;
                float3 normal : NORMAL;
            };
            struct v2f
            {
                float4 svPos : SV_POSITION;
                float3 vertWorldNormalPos : TEXCOORD0;
                float4 vertWorldPos : NORMAL ;
            };

            v2f vert(c2v c2v)
            {
                v2f v2f;
                v2f.svPos = UnityObjectToClipPos(c2v.vertPos);
                //点的世界坐标
                v2f.vertWorldPos = mul(unity_ObjectToWorld,c2v.vertPos);
                //点的世界法线
                v2f.vertWorldNormalPos = normalize(mul((float3x3)unity_ObjectToWorld,c2v.normal));
                return v2f;
            }
            fixed4 frag(v2f v2f):SV_Target
            {
                //光源颜色
                fixed3 lightColor = _LightColor0.rgb;
                //观察方向向量
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz-v2f.vertWorldPos.xyz);
                //反射
                float3 reflectDir = normalize(reflect(-_WorldSpaceLightPos0.xyz,v2f.vertWorldNormalPos));
                //高光反射
                fixed3 specularColor = lightColor.rgb*_HightLightColor.rgb*pow(max(0,dot(viewDir,reflectDir)),_Gloss);
                //漫反射光
                fixed3 diffuseColor = lightColor.rgb*_DiffuseColor.rgb*max(0,dot(v2f.vertWorldNormalPos,_WorldSpaceLightPos0.xyz));
                return fixed4(specularColor+diffuseColor,1)+UNITY_LIGHTMODEL_AMBIENT;
            }
            ENDCG

        }
    }
}
