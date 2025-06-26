Shader "Custom/WaterReflectionDistortion"
{
    Properties
    {
        _MainTex ("Reflection Texture", 2D) = "white" {}
        _DistortionTex ("Distortion Noise", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Range(0,0.1)) = 0.03
        _ScrollSpeed ("Noise Scroll Speed", Vector) = (0.1, 0.05, 0, 0)
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
            sampler2D _DistortionTex;
            float4 _MainTex_ST;
            float4 _ScrollSpeed;
            float _DistortionStrength;
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
                float2 scrollUV = i.uv + (_ScrollSpeed.xy * _Time.y);
                float2 distortion = (tex2D(_DistortionTex, scrollUV).rg - 0.5) * _DistortionStrength;
                float2 finalUV = i.uv + distortion;
                fixed4 col = tex2D(_MainTex, finalUV);
                col.a *= _Alpha;
                return col;
            }
            ENDCG
        }
    }
}
