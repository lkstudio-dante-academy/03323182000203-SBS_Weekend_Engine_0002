using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/** 팝업 */
public class CPopup : CComponent {
	#region 변수
	private Sequence m_oShowAni = null;
	private Sequence m_oCloseAni = null;
	#endregion // 변수

	#region 프로퍼티
	public Image BlindImg { get; private set; } = null;

	public GameObject Contents { get; private set; } = null;
	public GameObject ContentsBG { get; private set; } = null;
	public GameObject ContentsUIs { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.transform.localScale = Vector3.one;

		this.Contents = this.transform.Find("Contents").gameObject;
		this.Contents.transform.localScale = Vector3.one;

		this.ContentsBG = this.transform.Find("Contents/BG").gameObject;
		this.ContentsUIs = this.transform.Find("Contents/BG/ContentsUIs").gameObject;
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		CNavStackManager.Inst.PushComponent(this);
	}

	/** 초기화 */
	public virtual void Init() {
		this.BlindImg = CFactory.CreateCloneGameObj<Image>("BlindImg",
			Resources.Load<GameObject>("Global/Prefabs/G_BlindImg"), this.Contents);

		this.BlindImg.color = new Color(0.0f, 0.0f, 0.0f, 0.75f);
		this.BlindImg.raycastTarget = true;

		this.BlindImg.transform.SetAsFirstSibling();

		this.BlindImg.rectTransform.anchorMin = Vector2.one / 2.0f;
		this.BlindImg.rectTransform.anchorMax = Vector2.one / 2.0f;

		this.BlindImg.rectTransform.sizeDelta =
			new Vector2(KDefine.G_DESIGN_SCREEN_WIDTH * 5.0f, KDefine.G_DESIGN_SCREEN_HEIGHT * 5.0f);
	}

	/** 애니메이션을 리셋한다 */
	public virtual void ResetAnimations() {
		m_oShowAni?.Kill();
		m_oCloseAni?.Kill();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();
		this.ResetAnimations();

		// 앱이 종료 되었을 경우
		if(CExtension.ExIsQuitApp(null)) {
			return;
		}

		CNavStackManager.Inst.PopComponent(this);
	}

	/** 내비게이션 이벤트를 수신했을 경우 */
	public override void OnReceiveNavStackEvent(ENavStackEvent a_eEvent) {
		base.OnReceiveNavStackEvent(a_eEvent);

		// 백 키 이벤트 일 경우
		if(a_eEvent == ENavStackEvent.BACK_KEY_DOWN) {
			this.Close();
		}
	}

	/** 팝업을 출력한다 */
	public virtual void Show() {
		this.ResetAnimations();
		this.ContentsBG.transform.localScale = Vector3.zero;

		var oShowAni = this.MakeShowAni(this.ContentsBG);

		var oSequence = DOTween.Sequence().SetAutoKill();
		oSequence.Append(oShowAni);
		oSequence.AppendCallback(() => this.OnCompleteShowAni(oSequence));

		m_oShowAni = oSequence;
	}

	/** 팝업을 닫는다 */
	public virtual void Close() {
		this.ResetAnimations();
		var oCloseAni = this.MakeCloseAni(this.ContentsBG);

		var oSequence = DOTween.Sequence().SetAutoKill();
		oSequence.Append(oCloseAni);
		oSequence.AppendCallback(() => this.OnCompleteCloseAni(oSequence));

		m_oCloseAni = oSequence;
	}

	/** 출력 애니메이션이 완료 되었을 경우 */
	protected virtual void OnCompleteShowAni(Sequence a_oSender) {
		a_oSender?.Kill();
	}

	/** 닫기 애니메이션이 완료 되었을 경우 */
	protected virtual void OnCompleteCloseAni(Sequence a_oSender) {
		a_oSender?.Kill();
		Destroy(this.gameObject);
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 출력 애니메이션을 생성한다 */
	protected virtual Tween MakeShowAni(GameObject a_oTarget) {
		return a_oTarget.transform.DOScale(1.0f, 0.15f).SetAutoKill();
	}

	/** 닫기 애니메이션을 생성한다 */
	protected virtual Tween MakeCloseAni(GameObject a_oTarget) {
		return a_oTarget.transform.DOScale(0.0f, 0.15f).SetAutoKill();
	}
	#endregion // 팩토리 함수
}
