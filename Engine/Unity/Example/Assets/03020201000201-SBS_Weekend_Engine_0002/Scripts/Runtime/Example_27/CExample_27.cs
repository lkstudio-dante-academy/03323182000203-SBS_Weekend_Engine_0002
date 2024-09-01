using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 27 */
public class CExample_27 : CSceneManager
{
	#region ����
	[SerializeField] private ParticleSystem m_oParticleA = null;
	[SerializeField] private ParticleSystem m_oParticleB = null;
	#endregion // ����

	#region ������Ƽ
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_27;
	#endregion // ������Ƽ

	#region �Լ�
	/** �ʱ�ȭ */
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

	/** ���¸� �����Ѵ� */
	public override void Update()
	{
		base.Update();

		// ��ƼŬ ���� Ű�� ������ ���
		if(Input.GetKeyDown(KeyCode.Space))
		{
			m_oParticleA.Play(true);
			m_oParticleB.Play(true);
		}
	}

	/** ��ƼŬ ���Ḧ ó���Ѵ� */
	private void HandleOnParticleStopped(CEventDispatcher a_oSender)
	{
		Debug.Log("HandleOnParticleStopped");
	}
	#endregion // �Լ�
}
