Shader "Example_16/E16SurfaceShader_04" {
	Properties {
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Main Texture", Cube) = "white" { }
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
		samplerCUBE _MainTex;

		/** 입력 */
		struct Input {
			float4 color;

			float3 worldRefl;
			float3 worldNormal;
			
			/*
			INTERNAL_DATA 키워드는 정점의 법선 정보와 노말 맵의 법선 정보를
			동시에 사용 할 때 충돌을 방지하는 역할을 수행한다. (즉, 해당
			키워드를 명시하지 않으면 내부적으로 컴파일 에러가 발생한다는 것을
			알 수 있다.)
			*/
			INTERNAL_DATA
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

			/*
			texCUBE 함수는 큐브 맵을 기반으로 샘플링을 수행하는 함수이다.

			단, 큐브 맵의 특정 데이터를 가져오기 위해서는 UV 좌표가 아닌 방향
			정보를 입력으로 전달해줘야하는 차이점이 존재한다.

			WorldReflectionVector 함수는 월드 공간 상에 존재하는 반사 벡터를
			계산하는 역할을 수행한다.

			단, 해당 함수는 worldRefl 멤버를 사용해서 반사 벡터를 계산하기
			때문에 반드시 입력으로 전달되는 구조체 변수에 worldRefl 멤버를
			추가시켜줘야하는 특징이 존재한다.
			*/
			float4 stColor = texCUBE(_MainTex, 
				WorldReflectionVector(a_stInput, a_stInput.worldNormal));

			a_stOutput.Albedo = stColor.rgb;
			a_stOutput.Alpha = stColor.a;
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
			fSpecular = pow(fSpecular, 15.0);

			float3 stFinalColor = a_stInput.Albedo * fDiffuse;
			stFinalColor += float3(1.0, 1.0, 1.0) * fSpecular;

			return float4(stFinalColor, a_stInput.Alpha) * _Color * _LightColor0;
		}
		ENDCG
	}
}
