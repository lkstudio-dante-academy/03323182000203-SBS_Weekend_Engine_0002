using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 파티클이란?
 * - 작은 입자를 의미하며 해당 입자를 통해 다양한 불규칙한 현상을 만들어내는
 * 것을 파티클 효과라고 한다. (즉, 파티클을 사용하면 자연 현상과 연출을
 * 제작하는 것이 가능하다.)
 * 
 * Unity 파티클 시스템 종류
 * - 슈리켄
 * - Visual Effect Graph
 * 
 * 슈리켄 시스템이란?
 * - Unity 가 정통적으로 지원하던 파티클 효과를 제작하는 방법으로서
 * Particle System 컴포넌트에 존재하는 다양한 옵션을 기반으로 파티클 연출을
 * 제작하는 것이 가능하다.
 * 
 * Visual Effect Graph 시스템이란?
 * - 노드를 기반으로 파티클 효과를 제작하는 차세대 제작 방식으로서 현재
 * 사용하기에는 성능에 제한이 존재하지만 퀄리티 좋은 결과물을 만들낼 수 있다는
 * 장점이 존재한다.
 */
/** Example 27 */
public class CExample_27 : CSceneManager
{
	#region 변수
	[SerializeField] private ParticleSystem m_oParticleA = null;
	[SerializeField] private ParticleSystem m_oParticleB = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_27;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		var stMainModuleA = m_oParticleA.main;
		var stMainModuleB = m_oParticleB.main;

		stMainModuleA.playOnAwake = false;
		stMainModuleB.playOnAwake = false;

		m_oParticleA.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		m_oParticleB.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

		var oDispatcher = m_oParticleB.GetComponent<CEventDispatcher>();
		oDispatcher.ParticleCallback = this.HandleOnParticleStopped;
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 파티클 실행 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space))
		{
			m_oParticleA.Play(true);
			m_oParticleB.Play(true);
		}
	}

	/** 파티클 종료를 처리한다 */
	private void HandleOnParticleStopped(CEventDispatcher a_oSender)
	{
		Debug.Log("HandleOnParticleStopped");
	}
	#endregion // 함수
}
