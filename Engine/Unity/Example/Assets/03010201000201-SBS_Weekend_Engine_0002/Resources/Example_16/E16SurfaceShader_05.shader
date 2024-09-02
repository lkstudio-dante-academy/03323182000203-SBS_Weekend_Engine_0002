Shader "Example_16/E01S_Shader_16_05" {
	Properties {
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		cull front

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom vertex:VSMain

		/** 입력 */
		struct Input {
			float4 color;
		};

		/** 정점 쉐이더 */
		void VSMain(inout appdata_full a_stInput) {
			a_stInput.vertex.xyz += a_stInput.normal.xyz * 0.05;
		}

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			a_stOutput.Alpha = 1.0;
			a_stOutput.Emission = float3(0.0, 0.0, 0.0);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stInput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {

			return float4(a_stInput.Emission, a_stInput.Alpha);
		}
		ENDCG

		cull back

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float4 _Color;

		/** 입력 */
		struct Input {
			float4 color;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputCustom a_stOutput) {

			a_stOutput.Albedo = float3(1.0, 1.0, 1.0);
			a_stOutput.Alpha = 1.0;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stInput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {

			float3 stNormal = normalize(a_stInput.Normal);

			float fDiffuse = dot(stNormal, a_stLightDirection);
			fDiffuse = saturate(fDiffuse);
			fDiffuse = ceil(fDiffuse * 2.0) / 2.0;

			float3 stFinalColor = a_stInput.Albedo * fDiffuse;
			return float4(stFinalColor, a_stInput.Alpha) * _Color * _LightColor0;
		}
		ENDCG
	}
}
