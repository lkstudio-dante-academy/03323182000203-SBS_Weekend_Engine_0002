using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** 게임 객체 풀 관리자 */
public class CGameObjsPoolManager : CComponent
{
	#region 변수
	private Dictionary<string, CPoolListWrapper> m_oPoolListWrapperDict = new Dictionary<string, CPoolListWrapper>();
	#endregion // 변수

	#region 함수
	/** 객체를 활성한다 */
	public GameObject SpawnGameObj(string a_oKey,
		System.Func<GameObject> a_oCreator)
	{

		var oPoolListWrapper = this.GetPoolListWrapper(a_oKey);
		var oGameObj = oPoolListWrapper.m_oInactiveList.FirstOrDefault() ?? a_oCreator();

		oPoolListWrapper.m_oActiveList.Add(oGameObj);
		oPoolListWrapper.m_oInactiveList.Remove(oGameObj);

		oGameObj.SetActive(true);
		return oGameObj;
	}

	/** 객체를 비활성한다 */
	public void DespawnGameObj(string a_oKey, GameObject a_oGameObj)
	{
		var oPoolListWrapper = this.GetPoolListWrapper(a_oKey);
		oPoolListWrapper.m_oActiveList.Remove(a_oGameObj);
		oPoolListWrapper.m_oInactiveList.Add(a_oGameObj);

		a_oGameObj.SetActive(false);
	}
	#endregion // 함수

	#region 접근 함수
	/** 풀 리스트 래퍼를 반환한다 */
	private CPoolListWrapper GetPoolListWrapper(string a_oKey)
	{
		var oPoolListWrapper = m_oPoolListWrapperDict.GetValueOrDefault(a_oKey) ?? new CPoolListWrapper();
		m_oPoolListWrapperDict.TryAdd(a_oKey, oPoolListWrapper);

		return oPoolListWrapper;
	}
	#endregion // 접근 함수
}
