Shader "Custom/2D_RectGlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (1,1,0,1)
        _GlowSize ("Glow Size", Range(0.0, 1.0)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _GlowColor;
            float _GlowSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // Glow 강도는 중심에서 UV 외곽 거리 기반
                float glow = smoothstep(0.5, 0.5 - _GlowSize, dist);

                fixed4 finalColor = texColor;
                finalColor.rgb += _GlowColor.rgb * glow * _GlowColor.a;

                finalColor.a = texColor.a + glow * _GlowColor.a;

                return finalColor;
            }
            ENDCG
        }
    }
}
