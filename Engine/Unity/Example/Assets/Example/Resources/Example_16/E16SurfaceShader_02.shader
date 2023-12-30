Shader "Example_16/E16SurfaceShader_02" {
	Properties {
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Main Texture", 2D) = "white" { }
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float4 _Color;
		sampler2D _MainTex;

		/** 입력 */
		struct Input {
			float2 uv_MainTex;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;

			float3 m_stColor;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputCustom a_stOutput) {

			float4 stDiffuse = tex2D(_MainTex, a_stInput.uv_MainTex);

			a_stOutput.m_stColor = stDiffuse.rgb;
			a_stOutput.Alpha = stDiffuse.a;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stInput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {

			float3 stNormal = normalize(a_stInput.Normal);

			float fDiffuse = dot(stNormal, a_stLightDirection);
			fDiffuse = saturate(fDiffuse);

			float3 stFinalColor = a_stInput.m_stColor * fDiffuse;
			return float4(stFinalColor, a_stInput.Alpha) * _Color;
		}
		ENDCG
	}
}
