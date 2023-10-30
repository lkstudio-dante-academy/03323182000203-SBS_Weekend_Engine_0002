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
    public const string G_SCENE_N_EXAMPLE_01 = "Example_01 (기초)";
    public const string G_SCENE_N_EXAMPLE_02 = "Example_02 (프리팹, 스크립트)";
    public const string G_SCENE_N_EXAMPLE_03 = "Example_03 (카메라, 광원)";
    public const string G_SCENE_N_EXAMPLE_04 = "Example_04 (물리)";
	public const string G_SCENE_N_EXAMPLE_05 = "Example_05 (플래피 버드 - 시작)";
	public const string G_SCENE_N_EXAMPLE_06 = "Example_06 (플래피 버드 - 플레이)";
	public const string G_SCENE_N_EXAMPLE_07 = "Example_07 (플래피 버드 - 결과)";
	#endregion // 상수
}
