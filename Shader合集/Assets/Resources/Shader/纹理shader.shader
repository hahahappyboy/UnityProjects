
Shader "Unlit/纹理shader"
{
    Properties
    {
        _MainTex ("主纹理", 2D) = "white" {}
        _MainColor("主颜色", Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _MainColor;
            
            struct c2v
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            

            v2f vert (c2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;; 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                fixed4 texColor = tex2D(_MainTex,i.uv);
                return texColor*_MainColor;
           
            }
            
            ENDCG
        }
    }
}
