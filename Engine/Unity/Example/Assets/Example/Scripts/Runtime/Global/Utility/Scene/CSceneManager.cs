using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent {
	#region 프로퍼티
	public abstract string SceneName { get; }

	public Camera MainCamera { get; private set; } = null;
	public EventSystem EventSystem { get; private set; } = null;

	public GameObject UIs { get; private set; } = null;
	public GameObject PopupUIs { get; private set; } = null;

	public GameObject Objs { get; private set; } = null;
	public GameObject StaticObjs { get; private set; } = null;

	public bool IsActiveScene => this.SceneName.Equals(this.ActiveSceneName);

	/*
	 * SceneManager.GetActiveScene 메서드는 액티브 씬을 가져오는 역할을 수행한다.
	 * 
	 * 액티브 씬이란?
	 * - 일반적으로 Single 모드로 가장 먼저 로드가 된 씬을 의미한다.
	 * 
	 * Unity 가 지원이 몇몇 메서드는 액티브 씬을 대상으로만 동작하기 때문에
	 * 액티브 씬이 어떤 씬인지에 따라 메서드 호출 결과가 달라질 수 있다는 것을
	 * 알 수 있다.
	 */
	public string ActiveSceneName => SceneManager.GetActiveScene().name;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
    {
        base.Awake();
		var oRootGameObjects = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oRootGameObjects.Length; ++i) {
			var oMainCamera = oRootGameObjects[i].transform.Find("MainCamera");
			var oEventSystem = oRootGameObjects[i].transform.Find("EventSystem");

			var oUIs = oRootGameObjects[i].transform.Find("Canvas/UIs");
			var oPopupUIs = oRootGameObjects[i].transform.Find("Canvas/PopupUIs");

			var oObjs = oRootGameObjects[i].transform.Find("Objs");
			var oStaticObjs = oRootGameObjects[i].transform.Find("StaticObjs");

			this.MainCamera = this.MainCamera ?? oMainCamera?.GetComponent<Camera>();
			this.EventSystem = this.EventSystem ?? oEventSystem?.GetComponent<EventSystem>();

			this.UIs = this.UIs ?? oUIs?.gameObject;
			this.PopupUIs = this.PopupUIs ?? oPopupUIs?.gameObject;

			this.Objs = this.Objs ?? oObjs?.gameObject;
			this.StaticObjs = this.StaticObjs ?? oStaticObjs?.gameObject;
		}

		var oAudioListener = this.MainCamera.GetComponent<AudioListener>();
		oAudioListener.enabled = this.IsActiveScene;

		this.EventSystem.gameObject.SetActive(this.IsActiveScene);
    }

	/** 상태를 갱신한다 */
	public virtual void Update() {
		// Escape 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Escape)) {
			this.ShowQuitAlertPopup();
		}
	}

	/** 종료 알림 팝업을 출력한다 */
	protected virtual void ShowQuitAlertPopup() {
		// 종료 알림 팝업 출력이 불가능 할 경우
		if(!this.IsEnableShowQuitAlertPopup()) {
			return;
		}

		var reParams = CAlertPopup.MakeParams("메뉴 씬으로 이동하시겠습니까?",
			"확인", this.OnReceiveQuitAlertPopupCallback, "취소");

		var oAlertPopup = CFactory.CreateCloneGameObj<CAlertPopup>("AlertPopup",
			Resources.Load<GameObject>("Global/Prefabs/G_AlertPopup"), this.PopupUIs);

		oAlertPopup.Init(reParams);
		oAlertPopup.Show();
	}

	/** 종료 알림 팝업 콜백을 수신했을 경우 */
	protected virtual void OnReceiveQuitAlertPopupCallback(CAlertPopup a_oSender,
		bool a_bIsOK) {

		// 취소 버튼을 눌렀을 경우
		if(!a_bIsOK) {
			return;
		}

		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_EXAMPLE_00);
	}
	#endregion // 함수

	#region 접근 함수
	/** 종료 알림 팝업 출력 가능 여부를 검사한다 */
	protected bool IsEnableShowQuitAlertPopup() {
		bool bIsEnable = !this.SceneName.Equals(KDefine.G_SCENE_N_EXAMPLE_00);
		return bIsEnable && this.PopupUIs.transform.Find("AlertPopup") == null;
	}
	#endregion // 접근 함수
}
