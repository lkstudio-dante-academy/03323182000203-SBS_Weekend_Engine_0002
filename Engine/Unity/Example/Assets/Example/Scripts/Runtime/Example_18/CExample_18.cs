using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 18 */
public class CExample_18 : CSceneManager {
	#region 프로퍼티
	public CE18Player Player { get; private set; } = null;
	public List<CE18NonPlayer> NonPlayerList { get; } = new List<CE18NonPlayer>();

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

	#region 접근 함수
	/** 플레이어를 변경한다 */
	public void SetPlayer(CE18Player a_oPlayer) {
		this.Player = a_oPlayer;
	}

	/** NPC 를 추가한다 */
	public void AddNonPlayer(CE18NonPlayer a_oNonPlayer) {
		this.NonPlayerList.ExAddVal(a_oNonPlayer);
	}

	/** NPC 를 제거한다 */
	public void RemoveNonPlayer(CE18NonPlayer a_oNonPlayer) {
		this.NonPlayerList.Remove(a_oNonPlayer);
	}
	#endregion // 접근 함수
}
