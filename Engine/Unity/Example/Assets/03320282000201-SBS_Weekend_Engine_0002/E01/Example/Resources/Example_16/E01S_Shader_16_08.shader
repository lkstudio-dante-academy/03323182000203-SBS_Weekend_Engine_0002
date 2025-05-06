Shader "Example_16/E01S_Shader_16_08" {
	Properties {
		_Cutout("_Cutout", Range(0.0, 1.0)) = 0.5
		_SpecularPower("Specular Power", Range(5.0, 25.0)) = 10.0
		_RimPower("Rim Power", Range(1.0, 5.0)) = 2.5

		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecularColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_CutoutColor("Cutout Color", Color) = (1.0, 1.0, 1.0, 1.0)

		_MainTex("Main Texture", 2D) = "white" { }
		_NormalTex("Normal Texture", 2D) = "bump" { }
		_SpecularTex("Specular Texture", 2D) = "white" { }
		_NoiseTex("Noise Texture", 2D) = "white" { }
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom alpha:fade

		float _Cutout;
		float _SpecularPower;
		float _RimPower;

		float4 _Color;
		float4 _SpecularColor;
		float4 _RimColor;
		float4 _CutoutColor;

		sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _SpecularTex;
		sampler2D _NoiseTex;

		/** 입력 */
		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalTex;
			float2 uv_SpecularTex;
			float2 uv_NoiseTex;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			float Gloss;
			float Alpha;
			float Specular;

			float3 Albedo;
			float3 Normal;
			float3 Emission;

			float4 m_stSpecular;
			float4 m_stNoise;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputCustom a_stOutput) {

			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));
			float4 stDiffuse = tex2D(_MainTex, a_stInput.uv_MainTex);
			float4 stSpecular = tex2D(_SpecularTex, a_stInput.uv_SpecularTex);
			float4 stNoise = tex2D(_NoiseTex, a_stInput.uv_NoiseTex);

			a_stOutput.Albedo = stDiffuse.rgb;
			a_stOutput.Alpha = stDiffuse.a;
			a_stOutput.Normal = stNormal;

			a_stOutput.m_stSpecular = stSpecular;
			a_stOutput.m_stNoise = stNoise;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stInput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation) {

			float3 stNormal = normalize(a_stInput.Normal);
			float3 stReflect = normalize(reflect(-a_stLightDirection, stNormal));

			float fDiffuse = dot(stNormal, a_stLightDirection);
			fDiffuse = saturate(fDiffuse);

			float fSpecular = dot(stReflect, a_stViewDirection);
			fSpecular = saturate(fSpecular);
			fSpecular = pow(fSpecular, _SpecularPower);

			float fRim = dot(stNormal, a_stViewDirection);
			fRim = 1.0 - saturate(fRim);
			fRim = pow(fRim , _RimPower);

			float3 stFinalColor = a_stInput.Albedo * fDiffuse;
			stFinalColor += _SpecularColor.rgb * fSpecular * a_stInput.m_stSpecular.r;
			stFinalColor += _RimColor.rgb * fRim;

			float fAlpha = 1.0;
			float fOutline = 0.0;

			// 투명 처리가 필요 할 경우
			if(min(0.99999, a_stInput.m_stNoise.r * 5.0) < _Cutout) {
				fAlpha = 0.0;
			}

			// 외곽선 처리가 필요 할 경우
			if(min(0.99999, a_stInput.m_stNoise.r * 5.0) < _Cutout * 2.0) {
				fOutline = 1.0;
			}

			stFinalColor += _CutoutColor.rgb * fOutline;
			return float4(stFinalColor, fAlpha) * _Color * _LightColor0;
		}
		ENDCG
	}
}
