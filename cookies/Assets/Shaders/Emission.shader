Shader "Custom/Emission" {
    Properties {
        _Illumi ("Illumi Color", Color) = (1,1,1,1)
        _EmissionLM ("Emission (Lightmapper)", Float) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert
 
        fixed4 _Illumi;
		float _EmissionLM;

        struct Input {
            float4 color : COLOR;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            o.Emission = _Illumi.rgb * _EmissionLM;

        }
        ENDCG
    }

}