using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** NPC 상태 */
public class CE18NonPlayerState : CState<CE18NonPlayer>
{
	#region 함수
	/** 상태가 시작되었을 경우 */
	public override void OnStateEnter()
	{
		base.OnStateEnter();

		this.Owner.NavMeshAgent.isStopped = true;
		this.Owner.Animator.SetBool("IsTracking", false);

		this.Owner.Animator.ResetTrigger("Attack");
		this.Owner.Animator.ResetTrigger("Hit");
		this.Owner.Animator.ResetTrigger("Die");
	}
	#endregion // 함수
}

/** NPC 대기 상태 */
public class CE18NonPlayerIdleState : CE18NonPlayerState
{
	#region 함수
	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);

		// 공격 가능 할 경우
		if(this.Owner.IsEnableAttack())
		{
			this.Owner.StateMachine.SetState(this.Owner.CreateBattleState());
		}
		// 추적 가능 할 경우
		else if(this.Owner.IsEnableTracking())
		{
			this.Owner.StateMachine.SetState(this.Owner.CreateTrackingState());
		}
	}
	#endregion // 함수
}

/** NPC 추적 상태 */
public class CE18NonPlayerTrackingState : CE18NonPlayerState
{
	#region 함수
	/** 상태가 시작되었을 경우 */
	public override void OnStateEnter()
	{
		base.OnStateEnter();
		this.Owner.NavMeshAgent.isStopped = false;

		this.Owner.Animator.SetBool("IsTracking", true);
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);
		var oSceneManager = this.Owner.GetSceneManager();

		this.Owner.NavMeshAgent.SetDestination(oSceneManager.Player.transform.position);

		// 공격 가능 할 경우
		if(this.Owner.IsEnableAttack())
		{
			this.Owner.StateMachine.SetState(this.Owner.CreateBattleState());
		}
		// 추적 불가능 할 경우
		else if(!this.Owner.IsEnableTracking())
		{
			this.Owner.StateMachine.SetState(this.Owner.CreateIdleState());
		}
	}
	#endregion // 함수
}

/** NPC 전투 상태 */
public class CE18NonPlayerBattleState : CE18NonPlayerState
{
	#region 변수
	private bool m_bIsAttacking = false;
	private float m_fRemainAttackDelay = 0.0f;
	#endregion // 변수

	#region 함수
	/** 상태가 시작되었을 경우 */
	public override void OnStateEnter()
	{
		base.OnStateEnter();
		m_fRemainAttackDelay = this.Owner.AttackDelay;

		var oBehaviour = this.Owner.Animator.GetBehaviour<CE18AttackStateMachineBehaviour>();
		oBehaviour.ExitCallback = this.HandleOnStateExit;
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);
		m_fRemainAttackDelay -= a_fDeltaTime;

		bool bIsNeedTracking = !this.Owner.IsEnableAttack();
		bool bIsEnableAttack = m_fRemainAttackDelay.ExIsLessEquals(0.0f);

		// 추적이 필요 할 경우
		if(bIsNeedTracking)
		{
			this.Owner.StateMachine.SetState(this.Owner.CreateIdleState());
		}
		// 공격 가능 할 경우
		else if(bIsEnableAttack && !m_bIsAttacking)
		{
			m_bIsAttacking = true;
			this.Owner.IsEnableHit = true;
			this.Owner.Animator.SetTrigger("Attack");
		}
	}

	/** 상태 종료를 처리한다 */
	private void HandleOnStateExit(CStateMachineBehaviour a_oSender,
		Animator a_oAnimator, AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{

		m_bIsAttacking = false;
		this.Owner.IsEnableHit = false;
		this.Owner.StateMachine.SetState(this.Owner.CreateIdleState());
	}
	#endregion // 함수
}

/** NPC 죽음 상태 */
public class CE18NonPlayerDeathState : CE18NonPlayerState
{
	#region 함수
	/** 상태가 시작되었을 경우 */
	public override void OnStateEnter()
	{
		base.OnStateEnter();
		this.Owner.Animator.SetTrigger("Die");

		this.Owner.Collider.enabled = false;
	}
	#endregion // 함수
}
