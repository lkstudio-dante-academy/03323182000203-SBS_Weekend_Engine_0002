using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Unity 사운드 관련 주요 컴포넌트 종류
 * - Audio Source
 * - Audio Listener
 * 
 * Audio Source 컴포넌트란?
 * - 사운드를 재생하는 역할을 수행하는 컴포넌트를 의미한다. (즉, 해당 
 * 컴포넌트는 사운드가 발생하는 근원지라는 것을 알 수 있다.)
 * 
 * Audio Source 컴포넌트는 한번에 하나의 사운드만 재생하는 것이 가능하기
 * 때문에 여러 사운드를 재생하기 위해서는 재생하고 싶은 사운드의 개수만큼
 * Audio Source 컴포넌트가 필요하다는 특징이 존재한다.
 * 
 * 또한, Audio Source 컴포넌트는 PlayClipAtPoint 메서드를 제공하며 해당
 * 메서드를 사용하면 자동적으로 사운드를 재생하기 위한 게임 객체를 생성해
 * 준다. (즉, 간단하게 여러 사운드를 중첩으로 재생하는 것이 가능하다.)
 * 
 * 단, PlayClipAtPoint 메서드는 사운드를 재생하기 위한 게임 객체를 생성
 * 후 사운드 재생이 모두 완료되고 나면 제거 되는 단점이 존재한다. (즉,
 * 사운드를 빈번하게 재생 할 경우 임시적으로 게임 객체가 생성되고 제거되는
 * 부하가 발생한다는 것을 알 수 있다.)
 * 
 * 따라서, 특정 사운드가 빈번하게 재생이 되는 경우에는 사운드 재생을 
 * 효율적으로 처리 할 수 있는 풀링 구조를 만들어 줄 필요가 있다.
 * 
 * Audio Listener 컴포넌트란?
 * - 사운드를 듣는 역할을 수행하는 컴포넌트를 의미한다. (즉, 해당 사운드는
 * Unity 씬 상에 존재하는 사람의 귀에 해당한다는 것을 알 수 있다.)
 * 
 * 단, Audio Listener 컴포넌트는 Audio Source 컴포넌트와 달리 현재 로드 된
 * Unity 씬 중에 하나만 존재해야되는 차이점이 존재한다. (즉, 해당 컴포넌트가
 * 2 개 이상 Untiy 씬에 존재 할 경우 사운드 재생에 문제가 발생한다는 것을
 * 알 수 있다.)
 */
/** Example 14 */
public class CE01Example_14 : CSceneManager
{
	#region 변수
	[SerializeField] private AudioClip m_oFXAudioClip = null;
	[SerializeField] private AudioSource m_oBGAudioSrc = null;

	[Header("=====> UIs <=====")]
	[SerializeField] private Button m_oFXSndsBtn01 = null;
	[SerializeField] private Button m_oFXSndsBtn02 = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_14;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 배경음 버튼을 눌렀을 경우 */
	public void OnTouchBGSndBtn()
	{
		m_oBGAudioSrc.Play();
	}

	/** 효과음 버튼을 눌렀을 경우 */
	public void OnTouchFXSndsBtn(Button a_oSender)
	{
		// 효과음 1 일 경우
		if(a_oSender == m_oFXSndsBtn01)
		{
			AudioSource.PlayClipAtPoint(m_oFXAudioClip,
				this.MainCamera.transform.position);
		}
		// 효과음 2 일 경우
		else if(a_oSender == m_oFXSndsBtn02)
		{
			CSndManager.Inst.PlayFXSnds("Example_14/E14FXSnds",
				this.MainCamera.transform.position);
		}
	}

	/** 배경음 음소거 버튼을 눌렀을 경우 */
	public void OnTouchIsMuteBGSndsBtn()
	{
		CSndManager.Inst.SetIsMuteBGSnd(!CSndManager.Inst.IsMuteBGSnd);
	}

	/** 효과음 음소거 버튼을 눌렀을 경우 */
	public void OnTouchIsMuteFXSndsBtn()
	{
		CSndManager.Inst.SetIsMuteFXSnds(!CSndManager.Inst.IsMuteFXSnds);
	}
	#endregion // 함수
}
