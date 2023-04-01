
Shader "Interview/带纹理贴图的HalfLambert"
{
    Properties
    {
        _MainColor("漫反射颜色",Color)=(1,0,0,1)
        _MainTex("纹理贴图",2D)=""
    }
    SubShader
    {
        Tags{"LightMode"="ForwardBase"}
        Pass
        {
           CGPROGRAM
           # pragma vertex vert
           # pragma fragment frag
           #include "Lighting.cginc"
           fixed4 _MainColor;
           sampler2D _MainTex;
           float4 _MainTex_ST;
           struct c2v
           {
               float4 vertex:POSITION;
               float3 normal:NORMAL;
               float2 uv : TEXCOORD0;
           };
           struct v2f
           {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float2 uv : TEXCOORD0;
           };
            v2f vert(c2v data)
            {
                v2f o;
                //模型空间转裁剪空间,这两种都可以，这是顶点着色器一定要做的事情
                o.pos = UnityObjectToClipPos(data.vertex);
                // o.pos = mul(unity_MatrixMVP,data.vertex);
                //模型空间转世界空间
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, data.normal);
                o.worldNormal = normalize(o.worldNormal);
                //计算uv
                o.uv = data.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;; 
                return o;
            }
            fixed4 frag(v2f data):SV_Target
            {
                //世界坐标空间法线
                float3 worldNormal = data.worldNormal;
                //世界坐标光方向
                float3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                //物体纹理
                fixed3 texColor = tex2D(_MainTex,data.uv)* _MainColor.rgb;
                //漫反射颜色
                fixed3 diffuseColor = _LightColor0.rgb * texColor * (dot(worldNormal,worldLightDir)*0.5+0.5);
                //phong光照+环境光
                fixed3 phongColor = diffuseColor +  UNITY_LIGHTMODEL_AMBIENT;                                 
                return fixed4(phongColor,1);
            }
           ENDCG
        }
    }
}
