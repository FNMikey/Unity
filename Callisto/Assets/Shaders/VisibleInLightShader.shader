Shader"Custom/VisibleInLightShader" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
LOD 100
        
        Blend
SrcAlpha OneMinusSrcAlpha

ZWrite Off

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"
            
struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
    float3 normal : TEXCOORD1;
    float3 worldPos : TEXCOORD2;
};

sampler2D _MainTex;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.normal = UnityObjectToWorldNormal(v.normal);
    o.uv = v.uv;
    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
    return o;
}
            
fixed4 frag(v2f i) : SV_Target
{
                // Texture sampling
    fixed4 col = tex2D(_MainTex, i.uv);
    col.a = 0; // Start with fully transparent
                
                // Lighting calculation
    float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
    float diff = max(0, dot(i.normal, lightDir));
                
                // If the fragment is facing the light, make it visible
    if (diff > 0.5)
    { // Adjust threshold to your needs
        col.a = 1;
    }
                
    return col;
}
            ENDCG
        }
    }
FallBack"Diffuse"
}
