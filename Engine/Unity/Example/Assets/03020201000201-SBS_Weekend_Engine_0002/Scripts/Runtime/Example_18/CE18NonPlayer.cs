using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

/** NPC */
public class CE18NonPlayer : CComponent
{
	#region 변수
	[Header("=====> Etc <=====")]
	[SerializeField] private int m_nHP = 0;
	[SerializeField] private int m_nATK = 0;

	[SerializeField] private float m_fSightAngle = 0.0f;
	[SerializeField] private float m_fAttackDelay = 0.0f;
	[SerializeField] private float m_fAttackRange = 0.0f;
	[SerializeField] private float m_fTrackingRange = 0.0f;

	[SerializeField] private Collider m_oCollider = null;
	[SerializeField] private Animator m_oAnimator = null;
	[SerializeField] private NavMeshAgent m_oNavMeshAgent = null;

	private bool m_bIsDirtyUpdate = true;
	private bool m_bIsEnableUpdate = true;

	private int m_nMaxHP = 0;

	[Header("=====> UIs <=====")]
	[SerializeField] private Image m_oHPImg = null;
	[SerializeField] private Canvas m_oCanvas = null;
	#endregion // 변수

	#region 프로퍼티
	public CStateMachine<CState<CE18NonPlayer>> StateMachine { get; } = new CStateMachine<CState<CE18NonPlayer>>();
	public bool IsEnableHit { get; set; } = false;

	public int ATK => m_nATK;
	public float AttackDelay => m_fAttackDelay;

	public Collider Collider => m_oCollider;
	public Animator Animator => m_oAnimator;
	public NavMeshAgent NavMeshAgent => m_oNavMeshAgent;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

		m_nMaxHP = m_nHP;
		m_oNavMeshAgent.enabled = false;

		this.StateMachine.SetOwner(this);
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.GetSceneManager().AddNonPlayer(this);

		m_oNavMeshAgent.enabled = true;
		this.StateMachine.SetState(this.CreateIdleState());
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		this.GetSceneManager().RemoveNonPlayer(this);
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);

		// 상태 갱신이 불가능 할 경우
		if(!m_bIsEnableUpdate)
		{
			return;
		}

		this.StateMachine.OnStateUpdate(a_fDeltaTime);
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime)
	{
		base.OnLateUpdate(a_fDeltaTime);

		// 상태 갱신이 필요 할 경우
		if(m_bIsDirtyUpdate)
		{
			m_bIsDirtyUpdate = false;
			this.UpdateUIsState();
		}

		// 상태 갱신이 불가능 할 경우
		if(!m_bIsEnableUpdate)
		{
			return;
		}

		this.StateMachine.OnStateLateUpdate(a_fDeltaTime);
	}

	/** UI 상태를 갱신한다 */
	public void UpdateUIsState()
	{
		var stPos = m_oHPImg.rectTransform.anchoredPosition;
		float fPercent = m_nHP / (float)m_nMaxHP;

		m_oCanvas.gameObject.SetActive(m_nHP > 0);

		m_oHPImg.fillAmount = fPercent;
		m_oHPImg.rectTransform.anchoredPosition = new Vector2(fPercent * m_oHPImg.rectTransform.rect.size.x, stPos.y);
	}

	/** 피격되었을 경우 */
	public void OnHit(int a_nDamage,
		System.Action<CE18NonPlayer> a_oDeathCallback)
	{

		m_nHP = Mathf.Max(0, m_nHP - a_nDamage);
		m_bIsDirtyUpdate = true;
		m_bIsEnableUpdate = false;

		// 생존 상태 일 경우
		if(m_nHP > 0)
		{
			this.Animator.SetTrigger("Hit");
			this.NavMeshAgent.isStopped = true;

			var oBehaviour = this.Animator.GetBehaviour<CE18HitStateMachineBehaviour>();
			oBehaviour.ExitCallback = this.HandleOnStateExitHit;
		}
		else
		{
			a_oDeathCallback?.Invoke(this);
			this.StateMachine.SetState(this.CreateDeathState());
		}
	}

	/** 피격 상태 종료를 처리한다 */
	private void HandleOnStateExitHit(CStateMachineBehaviour a_oSender,
		Animator a_oAnimator, AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{

		m_bIsEnableUpdate = true;

		// 생존 상태 일 경우
		if(m_nHP > 0)
		{
			this.StateMachine.SetState(this.CreateIdleState());
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 공격 가능 여부를 검사한다 */
	public bool IsEnableAttack()
	{
		var oPlayer = this.GetSceneManager().Player;
		float fDistance = this.GetDistance(oPlayer.transform.position);

		return fDistance.ExIsLessEquals(m_fAttackRange);
	}

	/** 추적 가능 여부를 검사한다 */
	public bool IsEnableTracking()
	{
		var oPlayer = this.GetSceneManager().Player;
		float fDistance = this.GetDistance(oPlayer.transform.position);

		var stForward = this.transform.forward;
		var stDirection = oPlayer.transform.position - this.transform.position;

		float fDot = Vector3.Dot(stForward, stDirection.normalized);
		float fAngle = Mathf.Acos(fDot) * Mathf.Rad2Deg;

		return fAngle.ExIsLess(m_fSightAngle / 2.0f) &&
			fDistance.ExIsLessEquals(m_fTrackingRange);
	}

	/** 거리를 반환한다 */
	public float GetDistance(Vector3 a_stPos)
	{
		var stPos = this.transform.position;
		stPos.y = 0.0f;

		a_stPos.y = 0.0f;
		return Vector3.Distance(stPos, a_stPos);
	}

	/** 씬 관리자를 반환한다 */
	public CExample_18 GetSceneManager()
	{
		return CSceneManager.GetSceneManager<CExample_18>(KDefine.G_SCENE_N_EXAMPLE_18);
	}
	#endregion // 접근 함수

	#region 팩토리 함수
	/** 대기 상태를 생성한다 */
	public CState<CE18NonPlayer> CreateIdleState()
	{
		return new CE18NonPlayerIdleState();
	}

	/** 추적 상태를 생성한다 */
	public CState<CE18NonPlayer> CreateTrackingState()
	{
		return new CE18NonPlayerTrackingState();
	}

	/** 전투 상태를 생성한다 */
	public CState<CE18NonPlayer> CreateBattleState()
	{
		return new CE18NonPlayerBattleState();
	}

	/** 죽음 상태를 생성한다 */
	public CState<CE18NonPlayer> CreateDeathState()
	{
		return new CE18NonPlayerDeathState();
	}
	#endregion // 팩토리 함수
}
