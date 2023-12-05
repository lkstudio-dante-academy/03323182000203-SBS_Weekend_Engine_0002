using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 스케줄 관리자 */
public class CScheduleManager : CSingleton<CScheduleManager> {
	#region 변수
	private CListWrapper<Component> m_oComponentListWrapper = new CListWrapper<Component>();
	#endregion // 변수

	#region 함수
	/** 컴포넌트를 추가한다 */
	public void AddComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();

		int nResult = m_oComponentListWrapper.m_oList.FindIndex((a_oComponent) => 
			a_oComponent.GetInstanceID() == nID);

		// 컴포넌트 추가가 불가능 할 경우
		if(nResult >= 0) {
			return;
		}

		m_oComponentListWrapper.m_oAddList.Add(a_oComponent);
	}
	#endregion // 함수
}
