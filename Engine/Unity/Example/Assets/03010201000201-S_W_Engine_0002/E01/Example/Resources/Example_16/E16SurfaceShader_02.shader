Shader "Example_16/E01S_Shader_16_02" {
	Properties {
		_SpecularPower("Specular Power", Range(5.0, 25.0)) = 10.0
		_RimPower("Rim Power", Range(1.0, 5.0)) = 2.5

		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecularColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
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

		float _SpecularPower;
		float _RimPower;

		float4 _Color;
		float4 _SpecularColor;
		float4 _RimColor;

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
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputCustom a_stOutput) {

			float4 stDiffuse = tex2D(_MainTex, a_stInput.uv_MainTex);

			a_stOutput.Albedo = stDiffuse.rgb;
			a_stOutput.Alpha = stDiffuse.a;
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
			stFinalColor += _SpecularColor.rgb * fSpecular;
			stFinalColor += _RimColor.rgb * fRim;

			return float4(stFinalColor, a_stInput.Alpha) * _Color * _LightColor0;
		}
		ENDCG
	}
}
