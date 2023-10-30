using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 충돌 전달자 */
public class CTriggerDispatcher : CComponent {
	#region 프로퍼티
	public System.Action<CTriggerDispatcher, Collider> EnterCallback { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider> StayCallback { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider> ExitCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 충돌이 시작 되었을 경우 */
	public void OnTriggerEnter(Collider other) {
		this.EnterCallback?.Invoke(this, other);
	}

	/** 충돌이 진행 중 일 경우 */
	public void OnTriggerStay(Collider other) {
		this.StayCallback?.Invoke(this, other);
	}

	/** 충돌이 종료 되었을 경우 */
	public void OnTriggerExit(Collider other) {
		this.ExitCallback?.Invoke(this, other);
	}
	#endregion // 함수
}
