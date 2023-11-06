#define E08_SPRITE
#define E08_ANIMATION

#if E08_ANIMATION
#define E08_ANIMATION_TWEEN
#define E08_ANIMATION_KEYFRAME
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스프라이트란?
 * - 3 차원 공간 상에 출력되는 2 차원 이미지를 의미한다. (즉, 스프라이트를 
 * 활용하면 색상 정보를 담고 있는 텍스처를 간단하게 화면 상에 출력하는 것이
 * 가능하다.)
 */
/** Example 8 */
public class CExample_08 : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_08;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}
