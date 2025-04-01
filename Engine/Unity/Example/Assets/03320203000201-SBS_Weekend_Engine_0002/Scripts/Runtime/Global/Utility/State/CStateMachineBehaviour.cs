using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 머신 이벤트 처리자 */
public class CStateMachineBehaviour : StateMachineBehaviour
{
	#region 프로퍼티
	public System.Action<CStateMachineBehaviour, Animator, AnimatorStateInfo, int> EnterCallback { get; set; } = null;
	public System.Action<CStateMachineBehaviour, Animator, AnimatorStateInfo, int> ExitCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 상태가 시작되었을 경우 */
	public override void OnStateEnter(Animator a_oSender,
		AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{

		this.EnterCallback?.Invoke(this,
			a_oSender, a_stStateInfo, a_nLayerIdx);
	}

	/** 상태가 종료되었을 경우 */
	public override void OnStateExit(Animator a_oSender,
		AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{

		this.ExitCallback?.Invoke(this,
			a_oSender, a_stStateInfo, a_nLayerIdx);
	}
	#endregion // 함수
}
