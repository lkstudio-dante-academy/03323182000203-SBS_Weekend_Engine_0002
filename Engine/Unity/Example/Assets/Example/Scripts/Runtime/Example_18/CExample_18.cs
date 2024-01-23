using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 18 */
public class CExample_18 : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_18;
	public CGameObjsPoolManager GameObjsPoolManager { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.GameObjsPoolManager = CFactory.CreateGameObj<CGameObjsPoolManager>("GameObjsPoolManager",
			this.gameObject);
	}
	#endregion // 함수
}
