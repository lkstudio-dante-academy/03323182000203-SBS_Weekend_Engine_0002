using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 충돌 전달자 */
public class CCollisionDispatcher : CComponent {
	#region 프로퍼티
	public System.Action<CCollisionDispatcher, Collision> EnterCallback { get; set; } = null;
	public System.Action<CCollisionDispatcher, Collision> StayCallback { get; set; } = null;
	public System.Action<CCollisionDispatcher, Collision> ExitCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 충돌이 시작 되었을 경우 */
	public void OnCollisionEnter(Collision other) {
		this.EnterCallback?.Invoke(this, other);
	}

	/** 충돌이 진행 중 일 경우 */
	public void OnCollisionStay(Collision other) {
		this.StayCallback?.Invoke(this, other);
	}

	/** 충돌이 종료 되었을 경우 */
	public void OnCollisionExit(Collision other) {
		this.ExitCallback?.Invoke(this, other);
	}
	#endregion // 함수
}
