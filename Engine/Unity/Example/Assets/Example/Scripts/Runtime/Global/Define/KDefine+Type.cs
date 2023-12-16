using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 기본
/** 마우스 버튼 */
public enum EMouseBtn {
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	[HideInInspector] MAX_VAL
}

/** 투영 */
public enum EProjection {
	NONE = -1,
	_2D,
	_3D,
	[HideInInspector] MAX_VAL
}

/** 내비게이션 스택 이벤트 */
public enum ENavStackEvent {
	NONE = -1,
	BACK_KEY_DOWN,
	[HideInInspector] MAX_VAL
}

/** 리스트 래퍼 */
public class CListWrapper<T> {
	#region 변수
	public List<T> m_oList = new List<T>();
	public List<T> m_oAddList = new List<T>();
	public List<T> m_oRemoveList = new List<T>();
	#endregion // 변수

	#region 함수
	/** 값을 클리어한다 */
	public void Clear() {
		m_oList?.Clear();
		m_oAddList?.Clear();
		m_oRemoveList?.Clear();
	}
	#endregion // 함수
}
#endregion // 기본
