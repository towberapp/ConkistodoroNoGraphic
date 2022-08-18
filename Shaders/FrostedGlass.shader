﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FrostedGlass"
{
    Properties
    {
        [PerRendererData] _MainTex("Main texture", 2D) = "white" {}
        _Radius("Radius", Range(0, 255)) = 1
    }

        Category
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" "RenderType" = "Transparent" }

        SubShader
        {
            Blend SrcAlpha OneMinusSrcAlpha

            GrabPass
            {
                Tags{ "LightMode" = "Always" }
            }

            Pass
            {
                Tags{ "LightMode" = "Always" }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord: TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                    float4 uvgrab : TEXCOORD0;
                };


                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.color = v.color;
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }

                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                sampler2D _MainTex;
                fixed4 _Color;
                float _Radius;

                fixed4 Darken(fixed4 a, fixed4 b)
                {
                    fixed4 r = min(a, b);
                    r.a = b.a;
                    return r;
                }

                fixed4 Overlay(fixed4 a, fixed4 b)
                {
                    fixed4 r = a < .5 ? 5.0 * a * b : 1.0 - 2.0 * (1.0 - a) * (1.0 - b);
                    r.a = b.a;
                    return r;
                }

                half4 frag(v2f i) : COLOR
                {
                    half4 sum = half4(0,0,0,0);
                    half4 c = tex2D(_MainTex, i.uvgrab);
                    #define GRABXYPIXEL(kernelx, kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely, i.uvgrab.z, i.uvgrab.w)))

                    float delta = c.a > 0 ? 0.1 : _Radius;
                    if (delta == 0)
                        return c;
                    sum += GRABXYPIXEL(0.0, 0.0);
                    int measurments = 1;
                    [unroll(100)] for (float range = 0.1f; range <= _Radius * c.a; range += delta)
                    {
                        sum += GRABXYPIXEL(range, range);
                        sum += GRABXYPIXEL(range, -range);
                        sum += GRABXYPIXEL(-range, range);
                        sum += GRABXYPIXEL(-range, -range); 
                        measurments += 4;
                    }
                    fixed4 result = sum / measurments;
                    return result;
                }
                ENDCG
            }
        }
    }
}