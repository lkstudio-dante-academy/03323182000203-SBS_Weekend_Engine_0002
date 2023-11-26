//#define E12_IMGUI
#define E12_UNITY_GUI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

/*
 * Unity UI 제작 방법
 * - ImGUI
 * - Unity GUI
 * - UI Toolkit
 * 
 * ImGUI 란?
 * - 과거 Unity 버전에서 UI 를 제작하기 위해서 제공되는 기능을 의미한다.
 * 
 * 단, ImGUI 는 코드만을 통해서 UI 를 제작하는 것이 가능했기 때문에 특정
 * 사용처를 제외하고는 실질적으로 UI 를 제작하는데 거의 활용되지 않는다.
 * 
 * 따라서, ImGUI 를 기반으로 UI 를 제작하고 싶다면 커스텀 에디터 UI 를
 * 제작할때 사용하면 된다. (즉, ImGUI 는 플레이 모드 상에서 보여지는 UI 이
 * 외에 에디터 상에서 보여지는 UI 를 제작하는것이 가능하다.)
 * 
 * Unity GUI 란?
 * - 현재 가장 많이 활용되는 UI 를 제작하는데 활용되는 기능을 의미한다.
 * 
 * 해당 방법을 통해서 UI 를 제작 할 경우 에디터 상에서 UI 를 직접 배치하고
 * 실시간으로 결과를 확인 할 수 있기 때문에 코드를 통해서 UI 를 제작하는
 * ImGUI 방식에 비해 손쉽게 UI 를 제작하는 것이 가능하다.
 * 
 * UI Toolkit 이란?
 * - 차세대 UI 제작 방식을 의미하며 Unity GUI 에서 발생하는 단점들을 개선한
 * 방법으로 Unity GUI 에 비해 좀 더 수월하게 좋은 성능 발휘하는 UI 를 제작
 * 하는 것이 가능하다.
 * 
 * 단, 해당 방식은 아직 개발이 진행 중이기 때문에 당장 상용 프로젝트 사용하는
 * 것이 리스크가 크기 때문에 현재는 쓰이지 않고 있다.
 */
/** Example 12 */
public class CExample_12 : CSceneManager {
	#region 변수
	[Header("=====> Button <=====")]
	[SerializeField] private Button m_oBtn01 = null;
	[SerializeField] private Button m_oBtn02 = null;
	[SerializeField] private Button m_oBtn03 = null;

	[Header("=====> Toggle <=====")]
	[SerializeField] private Toggle m_oToggle01 = null;
	[SerializeField] private Toggle m_oToggle02 = null;
	[SerializeField] private Toggle m_oToggle03 = null;

	[Header("=====> Input <=====")]
	[SerializeField] private Slider m_oSlider = null;
	[SerializeField] private TMP_Dropdown m_oDropdown = null;
	[SerializeField] private TMP_InputField m_oInputField = null;

	[Header("=====> Chat UIs <=====")]
	[SerializeField] private GameObject m_oOriginText = null;
	[SerializeField] private GameObject m_oScrollViewContents = null;

	[SerializeField] private ScrollRect m_oScrollRect = null;
	[SerializeField] private TMP_InputField m_oChatUIsInputField = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_12;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		/*
		 * RemoveAllListeners 메서드를 활용하면 기존에 설정 되어있던 리스너를
		 * 제거하는 것이 가능하다. 
		 * 
		 * 단, 해당 메서드를 통해서 제거 가능한 리스너는 스크립트를 통해서 설정
		 * 된 리스너만 제거 가능하기 때문에 Unity 에디터 상에서 설정한 리스너는
		 * SetPersistentListenerState 메서드를 사용해서 비활성화시켜야한다.
		 * (즉, Unity 에디터 상에서 설정한 리스너를 제거하는 것은 불가능하다는
		 * 것을 알 수 있다.)
		 */
		m_oBtn01.onClick.RemoveAllListeners();
		m_oBtn02.onClick.RemoveAllListeners();
		m_oBtn03.onClick.RemoveAllListeners();

		/*
		 * AddListener 메서드는 버튼 클릭 이벤트를 처리 할 메서드를 설정하는
		 * 역할을 수행한다. (즉, 해당 메서드를 활용하면 프로그램이 동작 중에
		 * 버튼의 이벤트 리스너를 설정하고 처리하는 것이 가능하다.)
		 * 
		 * 단, 해당 메서드를 통해서 설정 할 수 있는 리스너는 아무런 입력도
		 * 전달 받지 않는 리스너만 설정하는 것이 가능하기 때문에 매개 변수가
		 * 존재하는 리스너를 설정하기 위해서는 람다 or 무명 메서드를 활용 할
		 * 필요가 있다.
		 */
		m_oBtn01.onClick.AddListener(() => this.OnTouchBtn(m_oBtn01));
		m_oBtn02.onClick.AddListener(() => this.OnTouchBtn(m_oBtn02));
		m_oBtn03.onClick.AddListener(() => this.OnTouchBtn(m_oBtn03));

		for(int i = 0; i < m_oBtn01.onClick.GetPersistentEventCount(); ++i) {
			m_oBtn01.onClick.SetPersistentListenerState(i, UnityEventCallState.Off);
			m_oBtn02.onClick.SetPersistentListenerState(i, UnityEventCallState.Off);
			m_oBtn03.onClick.SetPersistentListenerState(i, UnityEventCallState.Off);
		}

		m_oToggle01.onValueChanged.AddListener((a_bIsOn) => this.OnChangeToggle(m_oToggle01, a_bIsOn));
		m_oToggle02.onValueChanged.AddListener((a_bIsOn) => this.OnChangeToggle(m_oToggle02, a_bIsOn));
		m_oToggle03.onValueChanged.AddListener((a_bIsOn) => this.OnChangeToggle(m_oToggle03, a_bIsOn));

		m_oSlider.onValueChanged.AddListener(this.OnChangeSlider);
		m_oDropdown.onValueChanged.AddListener(this.OnChangeDropdown);

		m_oInputField.onEndEdit.AddListener(this.OnEndInputField);
		m_oInputField.onValueChanged.AddListener(this.OnChangeInputField);
	}

	/** GUI 를 그린다 */
	public void OnGUI() {
#if E12_IMGUI
		var stRect = new Rect(0.0f, 0.0f, Camera.main.pixelWidth, 100.0f);

		// 버튼을 눌렀을 경우
		if(GUI.Button(stRect, "버튼")) {
			Debug.Log("버튼을 눌렀습니다.");
		}
#endif
	}

	/** 버튼을 눌렀을 경우 */
	public void OnTouchBtn(Button a_oSender) {
		Debug.Log($"OnTouchBtn: {a_oSender.name}");
	}

	/** 토글이 변경 되었을 경우 */
	public void OnChangeToggle(Toggle a_oSender, bool a_bIsOn) {
		Debug.Log($"OnChangeToggle: {a_oSender.name}, {a_bIsOn}");
	}

	/** 슬라이더가 변경 되었을 경우 */
	public void OnChangeSlider(float a_fVal) {
		Debug.Log($"OnChangeSlider: {a_fVal}");
	}

	/** 드랍 다운이 변경 되었을 경우 */
	public void OnChangeDropdown(int a_nIdx) {
		Debug.Log($"OnChangeDropdown: {a_nIdx}");
	}

	/** 입력 필드가 종료 되었을 경우 */
	public void OnEndInputField(string a_oStr) {
		Debug.Log($"OnEndInputField: {a_oStr}");
	}

	/** 입력 필드가 변경 되었을 경우 */
	public void OnChangeInputField(string a_oStr) {
		Debug.Log($"OnChangeInputField: {a_oStr}");
	}

	/** 전송 버튼을 눌렀을 경우 */
	public void OnTouchSendBtn() {
		// 입력 필드가 비어있을 경우
		if(m_oChatUIsInputField.text.Length <= 0) {
			return;
		}

		var oText = Instantiate(m_oOriginText, 
			Vector3.zero, Quaternion.identity);

		oText.transform.SetParent(m_oScrollViewContents.transform, false);
		oText.GetComponent<TMP_Text>().text = m_oChatUIsInputField.text;

		/*
		 * 스크롤 뷰 컨텐츠에 특정 요소를 추가 후 위치를 변경 할 경우에는
		 * 반드시 약간의 지연이 필요하다.
		 * 
		 * 이는 컨텐츠의 크기가 즉시 계산이 되는 것이 아니라 다음 프레임에
		 * 계산이 완료되어서 새롭게 크기가 설정되기 때문이라는 것을 알 수 있다.
		 */
		StartCoroutine(this.TryUpdateScrollViewContentsPos());
	}

	/** 스크롤 뷰 컨텐츠 위치를 갱신한다 */
	private IEnumerator TryUpdateScrollViewContentsPos() {
		yield return new WaitForEndOfFrame();
		m_oScrollRect.verticalNormalizedPosition = 0.0f;
	}

	/** 채팅 UI 입력 필드를 종료했을 경우 */
	public void OnEndChatUIsInputField(string a_oStr) {
		// 입력 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Return)) {
			this.OnTouchSendBtn();
		}
	}
	#endregion // 함수
}
