using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * URP (Universal Render Pipeline) 란?
 * - 스크립터블 렌더링 파이프라인 중 하나로서 모바일을 비롯한 다양한
 * 플랫폼에서 구동되는 프로그램을 제작하기위한 렌더링 파이프라인이다.
 * 
 * URP 를 사용하면 쉐이더 그래프와 같은 여러 편리 기능들을 사용하는 것이
 * 가능하기 때문에 현재 추세는 점점 URP 를 기본으로 하는 구조로 바뀌고 
 * 있다. (즉, 기존의 Built-in 렌더링 파이프라인에서는 쉐이더 그래프를 
 * 사용하는 것이 불가능하다는 것을 알 수 있다.)
 * 
 * 또한, URP 는 스크립터블 렌더링 파이프라인이기 때문에 필요에 따라 렌더링
 * 과정 중 일부를 커스텀하게 제어하는 것이 가능하다. (즉, Untiy 의 렌더링
 * 과정 중 불필요한 과정을 제거해서 렌더링 성능을 향상시키는 것이 가능하다.)
 */
/** Example 26 */
public class CExample_26 : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_26;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}
	#endregion // 함수
}
