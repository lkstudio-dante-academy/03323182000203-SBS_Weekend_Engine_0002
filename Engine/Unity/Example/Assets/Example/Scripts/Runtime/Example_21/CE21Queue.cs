using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 큐 */
public class CE21Queue<T> {
	#region 변수
	private CE21LinkedList<T> m_oValList = new CE21LinkedList<T>();
	#endregion // 변수

	#region 프로퍼티
	public int NumVals => m_oValList.NumVals;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CE21Queue() {
		// Do Something
	}

	/** 데이터를 추가한다 */
	public void Enqueue(T a_tVal) {
		m_oValList.AddVal(a_tVal);
	}

	/** 데이터를 제거한다 */
	public T Dequeue() {
		var tVal = m_oValList[0];
		m_oValList.RemoveValAt(0);

		return tVal;
	}
	#endregion // 함수
}
