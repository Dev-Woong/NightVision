Shader "Custom/WaterWobble"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amplitude ("Amplitude", Range(0, 0.05)) = 0.01
        _Frequency ("Frequency", Range(0, 20)) = 10
        _Speed ("Speed", Range(0, 10)) = 2
        _Alpha ("Alpha", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Amplitude;
            float _Frequency;
            float _Speed;
            float _Alpha;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float wave = sin(i.uv.y * _Frequency + _Time.y * _Speed) * _Amplitude;
                float2 distortedUV = i.uv + float2(wave, 0);  // X축으로 일렁임
                fixed4 col = tex2D(_MainTex, distortedUV);
                col.a *= _Alpha;
                return col;
            }
            ENDCG
        }
    }
}
