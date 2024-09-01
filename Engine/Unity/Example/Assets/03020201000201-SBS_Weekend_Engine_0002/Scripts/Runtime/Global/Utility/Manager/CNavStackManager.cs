using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** 내비게이션 스택 관리자 */
public class CNavStackManager : CSingleton<CNavStackManager>
{
	#region 변수
	private List<CComponent> m_oComponentList = new List<CComponent>();
	#endregion // 변수

	#region 함수
	/** 내비게이션 스택 이벤트를 전달한다 */
	public void SendNavStackEvent(ENavStackEvent a_eEvent)
	{
		var oComponent = m_oComponentList.LastOrDefault();
		oComponent?.OnReceiveNavStackEvent(a_eEvent);
	}

	/** 컴포넌트를 추가한다 */
	public void PushComponent(CComponent a_oComponent)
	{
		int nID = a_oComponent.GetInstanceID();

		int nResult = m_oComponentList.FindIndex((a_oComponent) =>
			a_oComponent.GetInstanceID() == nID);

		// 추가가 불가능 할 경우
		if(nResult.ExIsValidIdx())
		{
			return;
		}

		m_oComponentList.ExAddVal(a_oComponent);
	}

	/** 컴포넌트를 제거한다 */
	public void PopComponent(CComponent a_oComponent)
	{
		int nID = a_oComponent.GetInstanceID();

		int nResult = m_oComponentList.FindIndex((a_oComponent) =>
			a_oComponent.GetInstanceID() == nID);

		// 제거가 불가능 할 경우
		if(!nResult.ExIsValidIdx())
		{
			return;
		}

		for(int i = m_oComponentList.Count - 1; i >= nResult; --i)
		{
			m_oComponentList.ExRemoveValAt(i);
		}
	}
	#endregion // 함수
}
