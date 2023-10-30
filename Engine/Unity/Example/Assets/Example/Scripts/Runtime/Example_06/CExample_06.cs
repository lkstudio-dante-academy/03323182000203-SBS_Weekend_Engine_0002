using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 6 */
public class CExample_06 : CSceneManager {
	#region 변수
	[SerializeField] private float m_fJumpPower = 0.0f;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oPlayer = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_06;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public void Update() {
		// 점프 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space)) {
			var oRigidbody = m_oPlayer.GetComponent<Rigidbody>();
			oRigidbody.velocity = Vector3.zero;

			oRigidbody.AddForce(Vector3.up * m_fJumpPower, ForceMode.VelocityChange);
		}
	}
	#endregion // 함수
}
