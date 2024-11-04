using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/** 텍스트 */
public class CE01UIText_10 : CComponent
{
	#region 변수
	private Tween m_oShowAni = null;
	private TMP_Text m_oText = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		m_oText = this.GetComponent<TMP_Text>();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		m_oShowAni?.Kill();
	}

	/** 텍스트를 출력한다 */
	public void ShowText(int a_nVal)
	{
		string oStr = (a_nVal < 0) ? $"{a_nVal}" : $"+{a_nVal}";
		Color stColor = (a_nVal < 0) ? Color.red : Color.white;

		m_oText.text = oStr;
		m_oText.color = stColor;

		m_oShowAni?.Kill();

		var oSequence = DOTween.Sequence();
		oSequence.Append(this.transform.DOMoveY(this.transform.position.y + 50.0f, 1.0f));
		oSequence.AppendCallback(() => this.OnCompleteShowAni(oSequence));

		m_oShowAni = oSequence;
	}

	/** 출력 애니메이션이 완료 되었을 경우 */
	private void OnCompleteShowAni(Sequence a_oSender)
	{
		a_oSender?.Kill();
		Destroy(this.gameObject);
	}
	#endregion // 함수
}
