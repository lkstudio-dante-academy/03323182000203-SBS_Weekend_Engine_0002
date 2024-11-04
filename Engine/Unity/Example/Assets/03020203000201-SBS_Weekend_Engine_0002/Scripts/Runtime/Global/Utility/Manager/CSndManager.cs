using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 관리자 */
public class CSndManager : CSingleton<CSndManager>
{
	#region 변수
	private bool m_bIsMuteFXSnds = false;
	private float m_fFXSndsVolume = 1.0f;

	private CSnd m_oBGSnd = null;
	private Dictionary<string, List<CSnd>> m_oFXSndDictContainer = new Dictionary<string, List<CSnd>>();
	#endregion // 변수

	#region 프로퍼티
	public bool IsMuteBGSnd => m_oBGSnd.IsMute;
	public bool IsMuteFXSnds => m_bIsMuteFXSnds;

	public float BGSndVolume => m_oBGSnd.Volume;
	public float FXSndsVolume => m_fFXSndsVolume;

	public AudioListener AudioListener { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oBGSnd = CFactory.CreateCloneGameObj<CSnd>("BGSnd",
			Resources.Load<GameObject>("Prefabs/Global/G_Prefab_BGM"), this.gameObject);
	}

	/** 배경음을 재생한다 */
	public void PlayBGSnd(string a_oSndFilePath)
	{
		m_oBGSnd.SetIsMute(this.IsMuteBGSnd);
		m_oBGSnd.SetVolume(this.BGSndVolume);

		m_oBGSnd.Play(Resources.Load<AudioClip>(a_oSndFilePath),
			false, true);
	}

	/** 효과음을 재생한다 */
	public void PlayFXSnds(string a_oSndFilePath,
		Vector3 a_stPos, bool a_bIsLoop = false)
	{

		var oFXSnds = this.FindPlayableFXSnds(a_oSndFilePath);

		// 재생 가능한 사운드가 없을 경우
		if(oFXSnds == null)
		{
			return;
		}

		this.SetIsMuteFXSnds(this.IsMuteFXSnds);
		this.SetFXSndsVolume(this.FXSndsVolume);

		oFXSnds.Play(Resources.Load<AudioClip>(a_oSndFilePath),
			!a_stPos.Equals(this.AudioListener.transform.position), a_bIsLoop);

		oFXSnds.transform.position = a_stPos;
	}

	/** 배경음을 중지한다 */
	private void StopBGSnd()
	{
		m_oBGSnd.Stop();
	}

	/** 효과음을 중지한다 */
	private void StopFXSnds()
	{
		this.EnumerateFXSnds((a_oFXSnds) =>
		{
			a_oFXSnds.Stop();
		});
	}

	/** 재생 가능한 효과음을 탐색한다 */
	private CSnd FindPlayableFXSnds(string a_oSndFilePath)
	{
		// 사운드 풀이 없을 경우
		if(!m_oFXSndDictContainer.TryGetValue(a_oSndFilePath,
			out List<CSnd> oFXSndsList))
		{

			oFXSndsList = new List<CSnd>();
			m_oFXSndDictContainer.TryAdd(a_oSndFilePath, oFXSndsList);
		}

		// 사운드 풀이 가득 찼을 경우
		if(oFXSndsList.Count >= 10)
		{
			for(int i = 0; i < oFXSndsList.Count; i++)
			{
				// 재생 중인 사운드 일 경우
				if(oFXSndsList[i].IsPlaying)
				{
					continue;
				}

				return oFXSndsList[i];
			}

			return null;
		}

		var oFXSnds = CFactory.CreateCloneGameObj<CSnd>("FXSnds",
			Resources.Load<GameObject>("Prefabs/Global/G_Prefab_SFX"), this.gameObject);

		oFXSndsList.ExAddVal(oFXSnds);
		return oFXSnds;
	}

	/** 효과음을 순회한다 */
	private void EnumerateFXSnds(System.Action<CSnd> a_oCallback)
	{
		foreach(var stKeyVal in m_oFXSndDictContainer)
		{
			for(int i = 0; i < stKeyVal.Value.Count; ++i)
			{
				a_oCallback?.Invoke(stKeyVal.Value[i]);
			}
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 배경음 음소거 여부를 변경한다 */
	public void SetIsMuteBGSnd(bool a_bIsMute)
	{
		m_oBGSnd.SetIsMute(a_bIsMute);
	}

	/** 효과음 음소거 여부를 변경한다 */
	public void SetIsMuteFXSnds(bool a_bIsMute)
	{
		m_bIsMuteFXSnds = a_bIsMute;

		this.EnumerateFXSnds((a_oFXSnds) =>
		{
			a_oFXSnds.SetIsMute(a_bIsMute);
		});
	}

	/** 배경음 볼륨을 조절한다 */
	public void SetBGSndVolume(float a_fVolume)
	{
		m_oBGSnd.SetVolume(a_fVolume);
	}

	/** 효과음 볼륨을 조절한다 */
	public void SetFXSndsVolume(float a_fVolume)
	{
		m_fFXSndsVolume = Mathf.Clamp01(a_fVolume);

		this.EnumerateFXSnds((a_oFXSnds) =>
		{
			a_oFXSnds.SetVolume(a_fVolume);
		});
	}

	/** 오디오 리스너를 변경한다 */
	public void SetAudioListener(AudioListener a_oListener)
	{
		this.AudioListener = a_oListener;
	}
	#endregion // 접근 함수
}
