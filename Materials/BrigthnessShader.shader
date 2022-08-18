Shader "Custom/2D/BrightnessShader"
{
    Properties{
        [PerRendererData] _MainTex("Main texture", 2D) = "white" {}
        [PerRendererData] _Color("Color", Color) = (1, 1, 1, 1) 
        [PerRendererData] _Brightness("Brightness", Range(0., 1)) = 0.
    }

        SubShader{

            Tags { "Queue" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };


                v2f vert(appdata_base v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    return o;
                }

                sampler2D _MainTex;
                float _Brightness;

                fixed4 frag(v2f i) : SV_Target {
                    float4 c = tex2D(_MainTex, i.uv);
                    float4 val = tex2D(_MainTex, i.uv);
                    val.a = 0;
                    c += val * _Brightness;
                    return c;
                }
                ENDCG
            }
        }
}