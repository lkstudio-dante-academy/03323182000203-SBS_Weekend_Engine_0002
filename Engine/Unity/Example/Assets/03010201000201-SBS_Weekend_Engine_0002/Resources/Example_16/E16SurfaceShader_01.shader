/*
Unity 쉐이더는 반드시 Shader 영역으로 시작해야 되며 해당 영역은 Shader 를
식별 할 수 있는 식별자를 명시하는 역할을 수행한다.

쉐이더는 일반적인 에셋과 달리 실제 쉐이더 파일이 있는 경로가 아닌 Shader 영역에
명시된 경로를 기반으로 쉐이더를 실행 중에 로드 할 수 있기 때문에 해당 영역에
명시되는 경로는 중복을 허용하지 않는다.
*/
Shader "Example_16/E01S_Shader_16_01" {
	/*
	Properties 영역은 Unity 에디터 상에서 쉐이더가 동작하는데 필요한 여러
	정보를 설정 할 수 있게 설정 필드를 명시하는 역할을 수행한다. (즉, 해당
	영역에 명시 된 속성은 Unity 인스펙터 상에 출력된다는 것을 알 수 있다.)
	*/
	Properties {
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex("Main Texture", 2D) = "white" { }
	}
	SubShader {
		/*
		Tags 영역은 Unity 가 해당 쉐이더를 처리하기 위한 부가 정보를 설정하는
		역할을 수행한다. (즉, 해당 영역에는 쉐이더의 동작 방식 및 순서 등을
		명시하는 것이 가능하다.)
		*/
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		/*
		Unity 는 Shader Lab 언어 이외에도 CG 와 같은 쉐이더 언어를 지원하며
		해당 언어를 사용하고 싶다면 PROGRAM 과 END 구문 사이에 해당 명령문을
		작성하면 된다.

		Unity 가 지원하는 쉐이더 언어 종류
		- Shader Lab
		- CG (C for Graphics)
		- HLSL (High Level Shader Langauge)
		- GLSL (OpenGL Shader Language)

		Unity 는 기본적으로 Built in 렌더링 파이프라인에 동작하는 쉐이더를
		작성 할 때는 CG 언어를 추천하며 URP or HDRP 상에서 동작하는 쉐이더를
		작성 할 때는 HLSL 언어를 추천한다. (즉, 해당 언어 이외에도 쉐이더를
		제작하는 것이 가능하지만 해당 파이프라인에 맞는 최적화 된 명령문을
		작성하기 위해서는 추천 된 언어를 사용해야 한다는 것을 알 수 있다.)

		Ex)
		CGPROGRAM
			// CG 명령문
		ENDCG

		HLSLPROGRAM
			// HLSL 명령문
		ENDHLSL
		*/
		CGPROGRAM
		/*
		target 속성은 쉐이더 버전을 지원하는 역할을 수행하며 버전이 낮을 수록
		많은 디바이스 상에서 구동되는 쉐이더를 제작하는 것이 가능하지만 사용
		할 수 있는 기능이 제한된다는 단점이 존재한다.

		따라서, 현재는 3.0 버전을 사용 할 경우 대부분의 디바이스 상에서 
		구동되는 쉐이더를 제작하는 것이 가능하기 때문에 범용적인 쉐이더를
		제작하고 싶다면 해당 버전을 사용하는 것을 추천한다.
		*/
		#pragma target 3.0

		/*
		surface 속성은 서피스 쉐이더가 동작 할 때 가장 먼저 실행 될 진입 
		함수의 이름을 명시하는 역할을 수행한다.

		또한, 서피스 쉐이더는 내부적으로 PBS (Physically Based Shading) 처리
		방식에 따라 물체의 색상을 계산하는 쉐이더를 지원하며 해당 쉐이더를
		사용하고 싶다면 Standard 옵션을 명시하면 된다.

		이처럼 서피스 쉐이더는 내부적으로 여러 연산을 자동으로 처리해주며 
		처리하고 싶은 연산은 옵션을 통해 명시하는 것이 가능하다. 
		(Ex. alpha:fade 등등...)
		*/
		#pragma surface SSMain Standard alpha:fade

		/*
		CG 를 비롯한 쉐이더는 언어는 특정 데이터를 제어하기 위한 여러가지
		자료형을 제공하며 필요에 따라 float 가 아닌 정수 타입 등을 사용하는
		것이 가능하다.

		단, 쉐이더는 그래픽 카드에 의해 처리되며 그래픽 카드는 부동 소수점에
		가장 최적화가 되어있기 때문에 정수 타입을 사용 할 경우 내부적으로
		부하가 발생한다는 것을 알 수 있다.

		쉐이더 주요 자료형 종류
		- fixed ~ fixed4
		- half ~ half4
		- float ~ float4
		- sampler2D
		- samplerCUBE
		*/
		float4 _Color;
		sampler2D _MainTex;

		/*
		쉐이더는 동작 할 때 정점으로부터 입력 된 데이터가 필요 할 경우 Input
		구조체를 해당 데이터를 입력 받는 것이 가능하다. (즉, 쉐이더 동작 할 때
		필요한 정보를 재질 or 정점을 통해서 입력받는 것이 가능하다.)

		단, 구조체 이름은 변경하는 불가능하기 때문에 이는 주의가 필요하다.

		재질을 통한 데이터 입력 vs 정점을 통한 데이터 입력
		- 재질을 통해 입력 된 데이터는 모든 정점이 공유하는 데이터 인 반면
		정점을 통해 입력 된 데이터는 해당 정점에만 적용된 데이터라는 차이점이
		존재한다. (즉, 재질을 통해 입력 된 데이터는 공용적으로 사용되는
		데이터라는 것을 알 수 있다.)
		*/
		/** 입력 */
		struct Input {
			float2 uv_MainTex;
		};

		/*
		쉐이더는 GPU 상에서 구동되는 프로그램이기 때문에 쉐이더 동작 할 때
		가장 먼저 실행 될 함수가 필요하다는 것을 알 수 있다. (즉, 진입 함수가
		존재해야한다는 것을 의미한다.)
		*/
		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputStandard a_stOutput) {

			/*
			tex2D 함수는 쉐이더에서 미리 제공하는 내장 함수로서 특정 텍스처에
			존재하는 데이터를 가져오는 역할을 수행한다. (즉, 샘플링을 한다는
			것을 알 수 있다.)

			텍스처는 색상 정보 이외에도 필요에 따라 다양한 정보를 설정하는
			것이 가능하며 해당 텍스처에 어떤 정보가 들어있냐에 따라서 텍스처의
			용도가 달라진다는 것을 알 수 있다. (즉, 이미지는 색상 정보가 담겨
			있는 텍스처라는 것을 수 있다.)
			*/
			float4 stDiffuse = tex2D(_MainTex, a_stInput.uv_MainTex);

			/*
			서피스 쉐이더에 의해 연산 된 색상 정보는 Albedo or Emission 
			멤버에 설정해줌으로서 최종적으로 화면에 출력 대상을 색상을 
			계산하는 것이 가능하다.

			Albedo vs Emission
			- Albedo 에 설정 된 색상은 최종적으로 광원에 의해 음영 처리가 되는
			반면 Emission 에 설정 된 색상은 광원과 상관 없이 설정 된 색상이
			그대로 화면 상에 출력된다는 차이점이 존재한다.

			따라서, Albedo 는 물체의 색상을 계산하기 위한 난반사 재질에 
			해당하며 Emission 는 물체가 자체적으로 색상을 발산하는 발산 재질에 
			해당하는 것을 알 수 있다.
			*/
			a_stOutput.Albedo = stDiffuse.rgb * _Color.rgb;
			a_stOutput.Alpha = stDiffuse.a * _Color.a;
		}
		ENDCG
	}
}
