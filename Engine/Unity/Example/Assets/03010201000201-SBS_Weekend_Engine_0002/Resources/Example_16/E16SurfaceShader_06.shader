Shader "Example_16/E01S_Shader_16_06" {
	Properties {
		_Weight("Weight", Range(0.0, 1.0)) = 0.0
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)

		_MainTex("Main Texture", 2D) = "white" { }
		_SubTex("Sub Texture", 2D) = "white" { }
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float _Weight;
		float4 _Color;

		sampler2D _MainTex;
		sampler2D _SubTex;

		/** 입력 */
		struct Input {
			float2 uv_MainTex;
			float2 uv_SubTex;
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

			float4 stMainColor = tex2D(_MainTex, a_stInput.uv_MainTex);
			float4 stSubColor = tex2D(_SubTex, a_stInput.uv_SubTex);

			a_stOutput.Albedo = lerp(stMainColor.rgb, stSubColor.rgb, _Weight);
			a_stOutput.Alpha = 1.0;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stInput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {

			float3 stNormal = normalize(a_stInput.Normal);

			float fDiffuse = dot(stNormal, a_stLightDirection);
			fDiffuse = saturate(fDiffuse);

			float3 stFinalColor = a_stInput.Albedo * fDiffuse;
			return float4(stFinalColor, a_stInput.Alpha) * _Color * _LightColor0;
		}
		ENDCG
	}
}
