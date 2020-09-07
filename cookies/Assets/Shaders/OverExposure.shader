Shader "Unlit/OverExposure"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HighThreshold ("High Threshold", Range(0,1)) = 1
        _LowThreshold ("Low Threshold", Range(0,1)) = 0
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HighThreshold;
            float _LowThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                
                if (col.r >= _HighThreshold)
                {
                    col.r = 1;
                }
                else if (col.r < _LowThreshold)
                {
                    col.r = 0;
                }

                if (col.g >= _HighThreshold)
                {
                    col.g = 1;
                }
                else if (col.g < _LowThreshold)
                {
                    col.g = 0;
                }

                if (col.b >= _HighThreshold)
                {
                    col.b = 1;
                }
                else if (col.b < _LowThreshold)
                {
                    col.b = 0;
                }
                

                /*
                float average = (col.r + col.g + col.b) / 3;
                if (average >= _HighThreshold)
                {
                    col.r = 1;
                    col.g = 1;
                    col.b = 1;
                }
                else if (average < _LowThreshold)
                {
                    col.r = 0;
                    col.g = 0;
                    col.b = 0;
                }
                */

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
