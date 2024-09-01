using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 스택 */
public class CE21Stack<T>
{
	#region 변수
	private CE21ArrayList<T> m_oValList = new CE21ArrayList<T>();
	#endregion // 변수

	#region 프로퍼티
	public int NumVals => m_oValList.NumVals;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CE21Stack()
	{
		// Do Something
	}

	/** 데이터를 추가한다 */
	public void Push(T a_tVal)
	{
		m_oValList.AddVal(a_tVal);
	}

	/** 데이터를 제거한다 */
	public T Pop()
	{
		var tVal = m_oValList[m_oValList.NumVals - 1];
		m_oValList.RemoveValAt(m_oValList.NumVals - 1);

		return tVal;
	}
	#endregion // 함수
}
