Shader "Custom/GlassWithFresnelRefractionLight2D"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (0.8, 0.9, 1, 0.3)
        
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,0.4)
        _FresnelPower ("Fresnel Power", Range(1, 10)) = 4

        _DistortionTex ("Distortion Map", 2D) = "gray" {}
        _DistortionStrength ("Distortion Strength", Range(0, 0.1)) = 0.03

        _LightStreakTex ("Light Streak Texture", 2D) = "white" {}
        _LightStreakColor ("Light Color", Color) = (1, 1, 1, 0.3)
        _LightSpeed ("Light Scroll Speed", Vector) = (0.1, 0.1, 0, 0)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Lighting Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _DistortionTex;
            sampler2D _LightStreakTex;

            float4 _MainTex_ST;
            float4 _DistortionTex_ST;
            float4 _LightStreakTex_ST;

            fixed4 _Color;
            fixed4 _FresnelColor;
            float _FresnelPower;
            float _DistortionStrength;

            fixed4 _LightStreakColor;
            float4 _LightSpeed; // x = scroll x, y = scroll y

            float _TimeY;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float2 uvDistortion : TEXCOORD2;
                float2 uvLight : TEXCOORD3;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uvDistortion = TRANSFORM_TEX(v.uv, _DistortionTex);
                o.uvLight = TRANSFORM_TEX(v.uv, _LightStreakTex);
                
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 viewDir = UnityWorldSpaceViewDir(worldPos);
                o.viewDir = normalize(viewDir);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Distortion
                float2 distortion = tex2D(_DistortionTex, i.uvDistortion).rg * 2 - 1;
                distortion *= _DistortionStrength;
                float2 distortedUV = i.uv + distortion;
                fixed4 texColor = tex2D(_MainTex, distortedUV);
                fixed4 baseColor = texColor * _Color;

                // Fresnel
                float3 normal = float3(0, 0, -1);
                float fresnel = pow(1.0 - saturate(dot(normalize(normal), i.viewDir)), _FresnelPower);
                fixed4 fresnelColor = _FresnelColor * fresnel;

                // Light streak (scrolling)
                float2 scrollUV = i.uvLight + _Time.y * _LightSpeed.xy;
                fixed4 lightTex = tex2D(_LightStreakTex, scrollUV) * _LightStreakColor;

                // Combine all
                fixed4 finalColor = baseColor + fresnelColor + lightTex;
                finalColor.a = baseColor.a + fresnelColor.a + lightTex.a;

                return finalColor;
            }
            ENDCG
        }
    }
}