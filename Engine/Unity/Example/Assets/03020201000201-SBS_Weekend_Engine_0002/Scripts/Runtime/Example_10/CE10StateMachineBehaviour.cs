using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 머신 이벤트 처리자 */
public class CE10StateMachineBehaviour : StateMachineBehaviour
{
	#region 프로퍼티
	public System.Action<CE10StateMachineBehaviour, Animator, AnimatorStateInfo, int> StateExitCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	public override void OnStateExit(Animator a_oSender,
		AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{

		this.StateExitCallback?.Invoke(this, a_oSender, a_stStateInfo, a_nLayerIdx);
	}
	#endregion // 함수
}
