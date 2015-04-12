Shader "Custom/Laser" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
    SubShader {
		Tags { "RenderType"="Transparent" }
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;
            
            struct v2f
            {
                float4 pos: SV_POSITION;
                float3 normal: NORMAL;
            };
            
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.normal = mul(UNITY_MATRIX_IT_MV, float4(v.normal, 1.0)).xyz;
                return o;
            }

            fixed4 frag(v2f i): SV_Target
            {
                float d = pow(dot(i.normal, normalize(float3(0.0, 0.0, 1.0))), 2.0);
                _Color.xyz += (1.0 - d) * 0.4;
                //_Color.a *= d;
                //return _Color;
                float f = sin(i.pos.x * 0.9) * sin(i.pos.y * 0.7) * 0.2 + 0.2;
                return _Color + float4(f, f, f, 0.0);
            }

            ENDCG
        }
    }
}
