Shader "Example_16/E01S_Shader_16_03" {
	Properties {
		_SpecularPower("Specular Power", Range(5.0, 25.0)) = 10.0
		_RimPower("Rim Power", Range(1.0, 5.0)) = 2.5

		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecularColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)

		_MainTex("Main Texture", 2D) = "white" { }
		_NormalTex("Normal Texture", 2D) = "bump" { }
		_SpecularTex("Specular Texture", 2D) = "white" { }
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
		sampler2D _NormalTex;
		sampler2D _SpecularTex;

		/** 입력 */
		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalTex;
			float2 uv_SpecularTex;
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
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputCustom a_stOutput) {

			/*
			UnpackNormal 함수는 탄젠트 (오브젝트) 공간에 존재하는 법선 정보를
			월드 공간 상으로 변환하는 역할을 수행한다. (즉, 노말 맵을 생성해주는
			툴은 법선 정보를 탄젠트 공간을 기준으로 생성해주기 때문에 해당 정보를
			사용해서 광원 연산을 처리하기 위해서는 반드시 두 공간을 일치시켜주는
			작업이 필요하다는 것을 알 수 있다.)
			*/
			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));
			float4 stDiffuse = tex2D(_MainTex, a_stInput.uv_MainTex);
			float4 stSpecular = tex2D(_SpecularTex, a_stInput.uv_SpecularTex);

			a_stOutput.Albedo = stDiffuse.rgb;
			a_stOutput.Alpha = stDiffuse.a;
			a_stOutput.Normal = stNormal;

			a_stOutput.m_stSpecular = stSpecular;
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

			return float4(stFinalColor, a_stInput.Alpha) * _Color * _LightColor0;
		}
		ENDCG
	}
}
