using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 27 */
public class CE01Example_27 : CSceneManager
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

		/** 스페이스 키를 눌렀을 경우 */
		if(Input.GetKeyDown(KeyCode.Space))
		{
			m_oParticleA.Play(true);
			m_oParticleB.Play(true);
		}
	}

	/** 파티클이 중지되었을 경우 */
	private void HandleOnParticleStopped(CEventDispatcher a_oSender)
	{
		Debug.Log("HandleOnParticleStopped");
	}
	#endregion // 함수
}
