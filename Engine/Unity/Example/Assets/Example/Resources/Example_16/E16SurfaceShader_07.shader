Shader "Example_16/E16SurfaceShader_07" {
	Properties {
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_NormalTex("Normal Texture", 2D) = "bump" { }
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float4 _RimColor;
		sampler2D _NormalTex;

		/** 입력 */
		struct Input {
			float4 color;
			float3 worldPos;

			float2 uv_NormalTex;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;

			float3 m_stWorldPos;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputCustom a_stOutput) {

			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));
			
			a_stOutput.Normal = stNormal;
			a_stOutput.m_stWorldPos = a_stInput.worldPos;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stInput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {

			float3 stNormal = normalize(a_stInput.Normal);

			float fRim01 = dot(stNormal, a_stViewDirection);
			fRim01 = 1.0 - saturate(fRim01);

			float fRim02 = frac(a_stInput.m_stWorldPos.y * 0.01 - _Time.y);
			fRim02 = pow(fRim02, 5.0);
			fRim02 = saturate(fRim02) * 0.25;

			return float4(_RimColor.rgb, saturate(fRim01 + fRim02)) * _LightColor0;
		}
		ENDCG
	}
}
