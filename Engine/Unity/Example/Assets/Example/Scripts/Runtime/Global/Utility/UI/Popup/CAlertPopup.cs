using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 알림 팝업 */
public class CAlertPopup : CPopup {
	/** 매개 변수 */
	public record REParams {
		public string m_oMsg;
		public string m_oOKBtnText;
		public string m_oCancelBtnText;

		public System.Action<CAlertPopup, bool> m_oCallback;
	}

	#region 변수
	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oMsgText = null;

	[SerializeField] private Button m_oOKBtn = null;
	[SerializeField] private Button m_oCancelBtn = null;
	#endregion // 변수

	#region 프로퍼티
	public REParams Params { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
	}

	/** 초기화 */
	public virtual void Init(REParams a_reParams) {
		base.Init();
		this.Params = a_reParams;
	}

	/** 초기화 */
	public override void Show() {
		base.Show();
		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		m_oMsgText.text = this.Params.m_oMsg;

		m_oOKBtn.GetComponentInChildren<Text>().text = this.Params.m_oOKBtnText;
		m_oCancelBtn.GetComponentInChildren<Text>().text = this.Params.m_oCancelBtnText;

		m_oCancelBtn.gameObject.SetActive(!string.IsNullOrEmpty(this.Params.m_oCancelBtnText));
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchOKBtn() {
		this.Params.m_oCallback?.Invoke(this, true);
		this.Close();
	}

	/** 취소 버튼을 눌렀을 경우 */
	private void OnTouchCancelBtn() {
		this.Params.m_oCallback?.Invoke(this, false);
		this.Close();
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static REParams MakeParams(string a_oMsg,
		string a_oOKBtnText, System.Action<CAlertPopup, bool> a_oCallback, string a_oCancelBtnText = "") {

		return new REParams() {
			m_oMsg = a_oMsg,
			m_oOKBtnText = a_oOKBtnText,
			m_oCancelBtnText = a_oCancelBtnText,
			m_oCallback = a_oCallback
		};
	}
	#endregion // 클래스 팩토리 함수
}
