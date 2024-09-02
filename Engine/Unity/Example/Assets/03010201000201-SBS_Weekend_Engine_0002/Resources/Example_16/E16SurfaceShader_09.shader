Shader "Example_16/E01S_Shader_16_09" {
	Properties {
		_Refraction("Refraction", Range(-1.0, 1.0)) = 0.0

		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_NoiseTex("Noise Texture", 2D) = "white" { }
	}
	SubShader {
		Tags {
			"Queue" = "Transparent+2"
			"RenderType" = "Transparent"
		}

		zwrite off
		GrabPass { }

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Standard alpha:fade

		float _Refraction;
		float4 _Color;

		sampler2D _NoiseTex;
		sampler2D _GrabTexture;

		/** 입력 */
		struct Input {
			float4 screenPos;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, 
			inout SurfaceOutputStandard a_stOutput) {

			float3 stNoiseUV = a_stInput.screenPos.xyz / a_stInput.screenPos.w;

			float4 stNoise = tex2D(_NoiseTex, stNoiseUV);
			stNoise *= _Refraction;

			float4 stDiffuse = tex2D(_GrabTexture, stNoiseUV + stNoise.xy);

			a_stOutput.Albedo = stDiffuse.rgb * _Color.rgb;
			a_stOutput.Alpha = stDiffuse.a * _Color.a;
		}
		ENDCG
	}
}
