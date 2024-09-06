using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 머신 */
public partial class CStateMachine
{
	#region 프로퍼티
	public object Owner { get; private set; } = null;
	#endregion 프로퍼티

	#region 접근 함수
	/** 소유자를 변경한다 */
	public void SetOwner(object a_oOwner)
	{
		this.Owner = a_oOwner;
	}
	#endregion // 접근 함수
}

/** 상태 머신 */
public partial class CStateMachine<T> : CStateMachine where T : CState, IUpdatable
{
	#region 프로퍼티
	public T CurState { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 갱신한다 */
	public void OnStateUpdate(float a_fDeltaTime)
	{
		this.CurState?.OnUpdate(a_fDeltaTime);
	}

	/** 상태를 갱신한다 */
	public void OnStateLateUpdate(float a_fDeltaTime)
	{
		this.CurState?.OnLateUpdate(a_fDeltaTime);
	}
	#endregion // 함수

	#region 접근 함수
	/** 상태를 변경한다 */
	public void SetState(T a_oState)
	{
		bool bIsInvalidA = this.CurState == a_oState;

		bool bIsInvalidB = this.CurState != null &&
			(this.CurState.GetType() == a_oState.GetType());

		// 상태 변경이 필요 없을 경우
		if(bIsInvalidA || bIsInvalidB)
		{
			return;
		}

		var oPrevState = this.CurState;
		oPrevState?.OnStateExit();

		this.CurState = a_oState;
		this.CurState?.SetOwner(this.Owner);
		this.CurState?.OnStateEnter();
	}
	#endregion // 접근 함수
}
