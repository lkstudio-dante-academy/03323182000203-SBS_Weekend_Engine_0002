using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 스케줄 관리자 */
public class CScheduleManager : CSingleton<CScheduleManager> {
	#region 변수
	private CListWrapper<CComponent> m_oComponentListWrapper = new CListWrapper<CComponent>();
	#endregion // 변수

	#region 함수
	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();
		m_oComponentListWrapper.Clear();
	}

	/** 상태를 갱신한다 */
	public void Update() {
		for(int i = 0; i < m_oComponentListWrapper.m_oList.Count; i++) {
			var oComponent = m_oComponentListWrapper.m_oList[i];

			// 제거 되었을 경우
			if(oComponent.ExIsDestroy()) {
				this.RemoveComponent(oComponent);
			}
			// 상태 갱신이 가능 할 경우
			else if(oComponent.ExIsEnableUpdate()) {
				oComponent.OnUpdate(Time.deltaTime);
			}
		}
	}

	/** 상태를 갱신한다 */
	public void LateUpdate() {
		for(int i = 0; i < m_oComponentListWrapper.m_oList.Count; i++) {
			var oComponent = m_oComponentListWrapper.m_oList[i];

			// 제거 되었을 경우
			if(oComponent.ExIsDestroy()) {
				this.RemoveComponent(oComponent);
			}
			// 상태 갱신이 가능 할 경우
			else if(oComponent.ExIsEnableUpdate()) {
				oComponent.OnLateUpdate(Time.deltaTime);
			}
		}

		this.UpdateComponentListWrapperState();
	}

	/** 상태를 갱신한다 */
	public void FixedUpdate() {
		for(int i = 0; i < m_oComponentListWrapper.m_oList.Count; i++) {
			var oComponent = m_oComponentListWrapper.m_oList[i];

			// 제거 되었을 경우
			if(oComponent.ExIsDestroy()) {
				this.RemoveComponent(oComponent);
			}
			// 상태 갱신이 가능 할 경우
			else if(oComponent.ExIsEnableUpdate()) {
				oComponent.OnFixedUpdate(Time.fixedDeltaTime);
			}
		}
	}

	/** 컴포넌트를 추가한다 */
	public void AddComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();

		int nResult = m_oComponentListWrapper.m_oList.FindIndex((a_oComponent) =>
			a_oComponent.GetInstanceID() == nID);

		// 추가가 불가능 할 경우
		if(nResult.ExIsValidIdx()) {
			return;
		}

		m_oComponentListWrapper.m_oAddList.ExAddVal(a_oComponent);
	}

	/** 컴포넌트를 제거한다 */
	public void RemoveComponent(CComponent a_oComponent) {
		int nID = a_oComponent.GetInstanceID();

		int nResult = m_oComponentListWrapper.m_oList.FindIndex((a_oComponent) =>
			a_oComponent.GetInstanceID() == nID);

		// 제거가 불가능 할 경우
		if(!nResult.ExIsValidIdx()) {
			return;
		}

		m_oComponentListWrapper.m_oRemoveList.ExAddVal(a_oComponent);
	}

	/** 컴포넌트 리스트 상태를 갱신한다 */
	private void UpdateComponentListWrapperState() {
		for(int i = 0; i < m_oComponentListWrapper.m_oAddList.Count; ++i) {
			var oComponent = m_oComponentListWrapper.m_oAddList[i];
			m_oComponentListWrapper.m_oList.ExAddVal(oComponent);
		}

		for(int i = 0; i < m_oComponentListWrapper.m_oRemoveList.Count; ++i) {
			var oComponent = m_oComponentListWrapper.m_oRemoveList[i];
			m_oComponentListWrapper.m_oList.Remove(oComponent);
		}

		m_oComponentListWrapper.m_oAddList.Clear();
		m_oComponentListWrapper.m_oRemoveList.Clear();
	}
	#endregion // 함수
}
