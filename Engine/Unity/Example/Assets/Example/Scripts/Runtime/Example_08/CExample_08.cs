//#define E08_SPRITE
#define E08_ANIMATION

#if E08_ANIMATION
//#define E08_ANIMATION_TWEEN
#define E08_ANIMATION_KEYFRAME
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * 스프라이트란?
 * - 3 차원 공간 상에 출력되는 2 차원 이미지를 의미한다. (즉, 스프라이트를 
 * 활용하면 색상 정보를 담고 있는 텍스처를 간단하게 화면 상에 출력하는 것이
 * 가능하다.)
 * 
 * Unity 애니메이션 처리 방식 종류
 * - 트윈
 * - 키 프레임 (레거시)
 * - 메카님
 * 
 * 트윈 애니메이션이란?
 * - 특정 대상의 상태를 보간함으로서 애니메이션 연출을 하는 방법을 의미한다.
 * (즉, 트윈 애니메이션은 단순한 애니메이션 연출을 제작할 때 적합하다는 것을
 * 알 수 있다.)
 * 
 * 단, Unity 자체는 트윈 애니메이션을 제작하기 위한 기능이 존재하지 않기
 * 때문에 DOTween 을 비롯한 3rd Party 라이브러리를 활용 할 필요가 있다.
 * 
 * Unity 대표 트윈 애니메이션 제작 라이브러리 (에셋)
 * - iTween
 * - DOTween
 * 
 * 해당 에셋 모두 Unity 에셋 스토어를 통해서 무료로 사용하는 것이 가능하다.
 * 
 * 키 프레임 (레거시) 애니메이션이란?
 * - 특정 대상의 상태를 키의 형태로 저장 후 시간의 흐름에 키에 저장되어 있는
 * 상태를 보간함으로서 애니메이션을 연출하는 방법을 의미한다.
 * 
 * 단, 키 프레임 애니메이션은 트윈 애니메이션 달리 복잡한 구조를 지니는
 * 애니메이션 연출이 가능하기 때문에 해당 애니메이션은 장면 연출과 같은 복잡성을
 * 지니는 애니메이션 제작에 주로 활용된다.
 * 
 * 메카님 애니메이션이란?
 * - FSM (Finite State Machine) 구조로 애니메이션을 제어하기 위한 시스템을
 * 의미한다. (즉, 메카님 시스템 자체는 애니메이션을 제작하기 위한 기능이 
 * 아니라는 것을 알 수 있다.)
 * 
 * 따라서, 메카님 시스템을 활용하면 특정 조건에 따라 애니메이션을 전환시키기 
 * 위한 구조를 좀 더 쉽게 작성하는 것이 가능하다.
 * 
 * 또한, 최근 버전 Unity 키 프레임 애니메이션을 제작 할 때 메카님 시스템을 
 * 활용하도록 구조에 변화가 발생했기 때문에 더 이상 키 프레임 애니메이션을 
 * 단독으로 사용하는 것이 불가능하다는 특징이 존재한다.
 */
/** Example 8 */
public class CExample_08 : CSceneManager {
	#region 변수
	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oSpriteRoot = null;
	[SerializeField] private GameObject m_oAniTweenRoot = null;
	[SerializeField] private GameObject m_oAniKeyframeRoot = null;

	[Header("=====> Animation - Tween <=====")]
	[SerializeField] private GameObject m_oAniTweenTarget01 = null;
	[SerializeField] private GameObject m_oAniTweenTarget02 = null;

	private Vector3 m_stAniTweenTarget01Pos = Vector3.zero;
	private Vector3 m_stAniTweenTarget02Pos = Vector3.zero;

	private Sequence m_oAniTweenTarget01Sequence = null;
	private Sequence m_oAniTweenTarget02Sequence = null;

	[Header("=====> Animation - Keyframe <=====")]
	[SerializeField] private GameObject m_oAniKeyframeTarget = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_08;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oSpriteRoot.SetActive(false);
		m_oAniTweenRoot.SetActive(false);
		m_oAniKeyframeRoot.SetActive(false);

#if E08_SPRITE
		m_oSpriteRoot.SetActive(true);
#elif E08_ANIMATION_TWEEN
		m_oAniTweenRoot.SetActive(true);

		m_stAniTweenTarget01Pos = m_oAniTweenTarget01.transform.position;
		m_stAniTweenTarget02Pos = m_oAniTweenTarget02.transform.position;
#elif E08_ANIMATION_KEYFRAME
		m_oAniKeyframeRoot.SetActive(true);

		var oDispatcher = m_oAniKeyframeTarget.GetComponent<CEventDispatcher>();
		oDispatcher.AniEventCallback = this.HandleOnAniEvent;
#endif
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

#if E08_ANIMATION_TWEEN
		/*
		 * DOTween 애니메이션이 적용 된 대상이 제거 될 경우 반드시 DOTween
		 * 애니메이션도 중단해줄 필요가 있다.
		 * 
		 * 만약, 애니메이션을 중단하지 않았을 경우 이미 제거가 된 객체를
		 * 대상은 애니메이션 처리를 시도하기 때문에 내부적으로 예외가 발생한다는
		 * 것을 알 수 있다.
		 */
		m_oAniTweenTarget01Sequence?.Kill();
		m_oAniTweenTarget02Sequence?.Kill();
#endif
	}

	/** 상태를 갱신한다 */
	public void Update() {
#if E08_ANIMATION_TWEEN
		/** 스페이스 키를 눌렀을 경우 */
		if(Input.GetKeyDown(KeyCode.Space)) {
			m_oAniTweenTarget01Sequence?.Kill();
			m_oAniTweenTarget02Sequence?.Kill();

			m_oAniTweenTarget01.transform.rotation = Quaternion.identity;
			m_oAniTweenTarget01.transform.position = m_stAniTweenTarget01Pos;

			m_oAniTweenTarget02.transform.rotation = Quaternion.identity;
			m_oAniTweenTarget02.transform.position = m_stAniTweenTarget02Pos;

			/*
			 * DOTween 은 대부분의 기능이 확장 메서드 형태로 제공되기 때문에
			 * 특정 컴포넌트가 DOTween 에 의해서 애니메이션을 연출 할 수 있는지
			 * 알고싶다면 해당 컴포넌트의 DO 계열 메서드를 검색하면 된다.
			 * (즉, DOTween 관련 기능은 모두 DO 접두어로 시작한다는 것을 알 수 있다.)
			 */
			var oMoveAni01 = m_oAniTweenTarget01.transform.DOMoveX(m_stAniTweenTarget01Pos.x + 400.0f,
				2.0f);

			var oRotateAni01 = m_oAniTweenTarget01.transform.DORotate(new Vector3(0.0f, 0.0f, -360.0f),
				2.0f, RotateMode.WorldAxisAdd);

			var oMoveAni02 = m_oAniTweenTarget02.transform.DOMoveX(m_stAniTweenTarget02Pos.x + 400.0f,
				2.0f);

			var oRotateAni02 = m_oAniTweenTarget02.transform.DORotate(new Vector3(0.0f, 0.0f, -360.0f),
				2.0f, RotateMode.WorldAxisAdd);

			/*
			 * DOTween 은 애니메이션 이징 기능을 지원하기 때문에 해당 기능을
			 * 활용하면 시간에 따라 계산되는 애니메이션 결과 값을 변경하는
			 * 것이 가능하다.
			 */
			oMoveAni01.SetEase(Ease.Linear);
			oRotateAni01.SetEase(Ease.Linear);

			/*
			 * Sequence 를 활용하면 여러 애니메이션을 순차적으로 재생하거나
			 * 동시에 재생하는 것이 가능하다. (즉, 순차적으로 애니메이션을
			 * 재생하고 싶다면 Append 메서드를 사용하고 동시에 재생하고 싶다면
			 * Join 메서드를 사용하면 된다는 것을 알 수 있다.)
			 */
			m_oAniTweenTarget01Sequence = DOTween.Sequence();
			m_oAniTweenTarget01Sequence.Append(oMoveAni01);
			m_oAniTweenTarget01Sequence.Append(oRotateAni01);

			m_oAniTweenTarget02Sequence = DOTween.Sequence();
			m_oAniTweenTarget02Sequence.Join(oMoveAni02);
			m_oAniTweenTarget02Sequence.Join(oRotateAni02);
		}
#elif E08_ANIMATION_KEYFRAME
		// 스페이스 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space)) {
			var oAnimator = m_oAniKeyframeTarget.GetComponent<Animator>();
			oAnimator.SetTrigger("Start");
		}
#endif
	}

	/** 애니메이션 이벤트를 처리한다 */
	private void HandleOnAniEvent(CEventDispatcher a_oSender,
		string a_oParams) {

		Debug.Log($"HandleOnAniEvent: {a_oParams}");
	}
	#endregion // 함수
}
