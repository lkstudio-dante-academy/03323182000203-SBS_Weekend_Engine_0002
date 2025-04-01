using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 타겟 */
public class CE01Target_10 : CComponent
{
	/** 타겟 타입 */
	public enum ETargetType
	{
		NONE = -1,
		A,
		D,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	[SerializeField] private RuntimeAnimatorController m_oController01 = null;
	[SerializeField] private RuntimeAnimatorController m_oController02 = null;

	private bool m_bIsOpen = false;
	private Animator m_oAnimator = null;
	#endregion // 변수

	#region 프로퍼티
	public ETargetType TargetType { get; private set; } = ETargetType.NONE;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		m_oAnimator = this.GetComponent<Animator>();
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		StartCoroutine(this.TryOpen());
	}

	/** 두더지를 잡는다 */
	public bool TryCatch()
	{
		// 등장 상태가 아닐 경우
		if(!m_bIsOpen)
		{
			return false;
		}

		m_bIsOpen = false;

		m_oAnimator.SetTrigger("Catch");
		m_oAnimator.ResetTrigger("Open");

		return true;
	}

	/** 애니메이션 상태 종료를 처리한다 */
	private void HandleOnStateExit(CE01StateMachineBehaviour_10 a_oSender,
		Animator a_oAnimator, AnimatorStateInfo a_stStateInfo, int a_nLayerIdx)
	{

		m_bIsOpen = false;
		StartCoroutine(this.TryOpen());
	}

	/** 두더지를 등장 시킨다 */
	private IEnumerator TryOpen()
	{
		yield return new WaitForSeconds(Random.Range(1.0f, 8.0f));
		int nRandVal = Random.Range(0, (int)ETargetType.MAX_VAL);

		m_bIsOpen = true;
		this.TargetType = (ETargetType)nRandVal;

		/*
		 * 실행 중에 애니메이터 컨트롤러를 변경해야 될 경우 해당 작업은 반드시
		 * 매개 변수를 설정하기 이전에 해줘야한다.
		 * 
		 * 이는 매개 변수를 설정하는 메서드는 현재 설정 되어있는 애니메이터
		 * 컨트롤러를 대상으로 수행되기 때문에 해당 순서가 뒤바뀔 경우 설정해
		 * 놓은 매개 변수 값이 유효하지 않게 될 수 있다.
		 */
		m_oAnimator.runtimeAnimatorController = (nRandVal <= 0) ?
			m_oController01 : m_oController02;

		var oBehaviours = m_oAnimator.GetBehaviours<CE01StateMachineBehaviour_10>();

		for(int i = 0; i < oBehaviours.Length; ++i)
		{
			oBehaviours[i].StateExitCallback = this.HandleOnStateExit;
		}

		m_oAnimator.SetTrigger("Open");
		m_oAnimator.ResetTrigger("Catch");
	}
	#endregion // 함수
}
