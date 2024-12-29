Shader "Unlit/SpriteTiling"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _Tiling ("Tiling", Vector) = (10,10, 0, 0)
        _Offset ("Offset", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Uniforms
            uniform float4 _Tiling;
            uniform float4 _Offset;
            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // Áp dụng Tiling và Offset cho UV
                o.uv = v.uv * _Tiling.xy + _Offset.xy;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv); // Trả về màu từ texture
            }
            ENDCG
        }
    }
    FallBack "Sprite/Default"
}
