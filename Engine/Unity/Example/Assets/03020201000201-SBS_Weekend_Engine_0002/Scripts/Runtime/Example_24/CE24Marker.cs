using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** 마커 */
public class CE24Marker : CComponent
{
	#region 변수
	[Header("=====> UIs <=====")]
	[SerializeField] private TMP_Text m_oText = null;
	#endregion // 변수

	#region 접근 함수
	/** 텍스트를 변경한다 */
	public void SetText(string a_oStr)
	{
		m_oText.text = a_oStr;
	}
	#endregion // 접근 함수
}
