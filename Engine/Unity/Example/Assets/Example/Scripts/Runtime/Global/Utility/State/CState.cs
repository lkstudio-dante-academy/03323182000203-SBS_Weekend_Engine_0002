using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 */
public partial class CState {
	#region 프로퍼티
	public object Owner { get; private set; } = null;
	#endregion 프로퍼티

	#region 함수
	/** 상태가 시작되었을 경우 */
	public virtual void OnStateEnter() {
		// Do Something
	}

	/** 상태가 종료되었을 경우 */
	public virtual void OnStateExit() {
		// Do Something
	}
	#endregion // 함수

	#region 접근 함수
	/** 소유자를 변경한다 */
	public void SetOwner(object a_oOwner) {
		this.Owner = a_oOwner;
	}
	#endregion // 접근 함수
}

/** 상태 */
public partial class CState<T> : CState, IUpdatable where T : class {
	#region 프로퍼티
	public new T Owner => base.Owner as T;
	#endregion // 프로퍼티

	#region IUpdatable
	/** 상태를 갱신한다 */
	public virtual void OnUpdate(float a_fDeltaTime) {
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnLateUpdate(float a_fDeltaTime) {
		// Do Something
	}
	#endregion // IUpdatable
}
