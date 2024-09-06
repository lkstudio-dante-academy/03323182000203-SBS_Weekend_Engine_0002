using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * partial 키워드를 활용하면 특정 클래스를 여러 파일에 나누어서 정의하는
 * 것이 가능하다. (즉, 클래스가 지니는 기능이 많을수록 해당 클래스에 작성되는
 * 명령문은 많아지기 때문에 이는 곧 관리의 용이성이 떨어지는 단점이 존재한다는
 * 것을 알 수 있다.)
 * 
 * 따라서, partial 키워드를 활용해서 여러 파일에 나누어서 정의 된 클래스는
 * 컴파일 단계에서 하나의 클래스로 합쳐진다는 것을 알 수 있다.
 */
/** 전역 상수 */
public static partial class KDefine
{
	#region 상수
	// 해상도
	public const float G_DESIGN_SCREEN_WIDTH = 1920.0f;
	public const float G_DESIGN_SCREEN_HEIGHT = 1080.0f;

	// 씬 이름
	public const string G_SCENE_N_EXAMPLE_00 = "E01Example_00 (메뉴)";
	public const string G_SCENE_N_EXAMPLE_01 = "E01Example_01 (기초)";
	public const string G_SCENE_N_EXAMPLE_02 = "E01Example_02 (프리팹, 스크립트)";
	public const string G_SCENE_N_EXAMPLE_03 = "E01Example_03 (카메라, 광원)";
	public const string G_SCENE_N_EXAMPLE_04 = "E01Example_04 (물리)";
	public const string G_SCENE_N_EXAMPLE_05 = "E01Example_05 (플래피 버드 - 시작)";
	public const string G_SCENE_N_EXAMPLE_06 = "E01Example_06 (플래피 버드 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_07 = "E01Example_07 (플래피 버드 - 결과)";
	public const string G_SCENE_N_EXAMPLE_08 = "E01Example_08 (스프라이트, 애니메이션)";
	public const string G_SCENE_N_EXAMPLE_09 = "E01Example_09 (두더지 잡기 - 시작)";
	public const string G_SCENE_N_EXAMPLE_10 = "E01Example_10 (두더지 잡기 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_11 = "E01Example_11 (두더지 잡기 - 결과)";
	public const string G_SCENE_N_EXAMPLE_12 = "E01Example_12 (UI)";
	public const string G_SCENE_N_EXAMPLE_13 = "E01Example_13 (스케줄링)";
	public const string G_SCENE_N_EXAMPLE_14 = "E01Example_14 (사운드)";
	public const string G_SCENE_N_EXAMPLE_15 = "E01Example_15 (내비게이션 메쉬)";
	public const string G_SCENE_N_EXAMPLE_16 = "E01Example_16 (쉐이더)";
	public const string G_SCENE_N_EXAMPLE_17 = "E01Example_17 (3D TPS - 시작)";
	public const string G_SCENE_N_EXAMPLE_18 = "E01Example_18 (3D TPS - 플레이)";
	public const string G_SCENE_N_EXAMPLE_19 = "E01Example_19 (3D TPS - 결과)";
	public const string G_SCENE_N_EXAMPLE_20 = "E01Example_20 (쓰레드)";
	public const string G_SCENE_N_EXAMPLE_21 = "E01Example_21 (자료구조)";
	public const string G_SCENE_N_EXAMPLE_22 = "E01Example_22 (네트워크)";
	public const string G_SCENE_N_EXAMPLE_23 = "E01Example_23 (틱택토 - 시작)";
	public const string G_SCENE_N_EXAMPLE_24 = "E01Example_24 (틱택토 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_25 = "E01Example_25 (틱택토 - 결과)";
	public const string G_SCENE_N_EXAMPLE_26 = "E01Example_26 (URP)";
	public const string G_SCENE_N_EXAMPLE_27 = "E01Example_27 (파티클)";
	#endregion // 상수

	#region 프로퍼티
	public static Vector3 DeviceScreenSize
	{
		get
		{
#if UNITY_EDITOR
			return new Vector3(Camera.main.pixelWidth,
				Camera.main.pixelHeight, 0.0f);
#else
			return new Vector3(Screen.width, Screen.height, 0.0f);
#endif
		}
	}
	#endregion // 프로퍼티
}
