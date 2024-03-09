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
	public const string G_SCENE_N_EXAMPLE_00 = "Example_00 (메뉴)";
	public const string G_SCENE_N_EXAMPLE_01 = "Example_01 (기초)";
    public const string G_SCENE_N_EXAMPLE_02 = "Example_02 (프리팹, 스크립트)";
    public const string G_SCENE_N_EXAMPLE_03 = "Example_03 (카메라, 광원)";
    public const string G_SCENE_N_EXAMPLE_04 = "Example_04 (물리)";
	public const string G_SCENE_N_EXAMPLE_05 = "Example_05 (플래피 버드 - 시작)";
	public const string G_SCENE_N_EXAMPLE_06 = "Example_06 (플래피 버드 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_07 = "Example_07 (플래피 버드 - 결과)";
	public const string G_SCENE_N_EXAMPLE_08 = "Example_08 (스프라이트, 애니메이션)";
	public const string G_SCENE_N_EXAMPLE_09 = "Example_09 (두더지 잡기 - 시작)";
	public const string G_SCENE_N_EXAMPLE_10 = "Example_10 (두더지 잡기 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_11 = "Example_11 (두더지 잡기 - 결과)";
	public const string G_SCENE_N_EXAMPLE_12 = "Example_12 (UI)";
	public const string G_SCENE_N_EXAMPLE_13 = "Example_13 (스케줄링)";
	public const string G_SCENE_N_EXAMPLE_14 = "Example_14 (사운드)";
	public const string G_SCENE_N_EXAMPLE_15 = "Example_15 (내비게이션 메쉬)";
	public const string G_SCENE_N_EXAMPLE_16 = "Example_16 (쉐이더)";
	public const string G_SCENE_N_EXAMPLE_17 = "Example_17 (3D TPS - 시작)";
	public const string G_SCENE_N_EXAMPLE_18 = "Example_18 (3D TPS - 플레이)";
	public const string G_SCENE_N_EXAMPLE_19 = "Example_19 (3D TPS - 결과)";
	public const string G_SCENE_N_EXAMPLE_20 = "Example_20 (쓰레드)";
	public const string G_SCENE_N_EXAMPLE_21 = "Example_21 (자료구조)";
	public const string G_SCENE_N_EXAMPLE_22 = "Example_22 (네트워크)";
	public const string G_SCENE_N_EXAMPLE_23 = "Example_23 (틱택토 - 시작)";
	public const string G_SCENE_N_EXAMPLE_24 = "Example_24 (틱택토 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_25 = "Example_25 (틱택토 - 결과)";
	public const string G_SCENE_N_EXAMPLE_26 = "Example_26 (URP)";
	#endregion // 상수

	#region 프로퍼티
	public static Vector3 DeviceScreenSize {
		get {
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
