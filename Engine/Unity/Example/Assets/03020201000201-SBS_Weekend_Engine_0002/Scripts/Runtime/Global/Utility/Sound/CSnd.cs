using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 */
public class CSnd : CComponent
{
	#region 변수
	private AudioSource m_oAudioSrc = null;
	#endregion // 변수

	#region 프로퍼티
	public bool IsMute => m_oAudioSrc.mute;
	public bool IsPlaying => m_oAudioSrc.isPlaying;

	public float Volume => m_oAudioSrc.volume;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oAudioSrc = this.GetComponent<AudioSource>();
		m_oAudioSrc.playOnAwake = false;
	}

	/** 사운드를 재생한다 */
	public void Play(AudioClip a_oAudioClip, bool a_bIs3D, bool a_bIsLoop)
	{
		m_oAudioSrc.clip = a_oAudioClip;
		m_oAudioSrc.spatialBlend = a_bIs3D ? 1.0f : 0.0f;
		m_oAudioSrc.loop = a_bIsLoop;

		m_oAudioSrc.Play();
	}

	/** 사운드를 중지한다 */
	public void Stop()
	{
		m_oAudioSrc.Stop();
	}
	#endregion // 함수

	#region 접근 함수
	/** 음소거 여부를 변경한다 */
	public void SetIsMute(bool a_bIsMute)
	{
		m_oAudioSrc.mute = a_bIsMute;
	}

	/** 볼륨을 변경한다 */
	public void SetVolume(float a_fVolume)
	{
		m_oAudioSrc.volume = Mathf.Clamp01(a_fVolume);
	}
	#endregion // 접근 함수
}
