using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 배열 리스트 */
public class CE01ArrayList_21<T>
{
	#region 변수
	private T[] m_oVals = null;
	#endregion // 변수

	#region 프로퍼티
	public int NumVals { get; private set; } = 0;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CE01ArrayList_21(int a_nSize = 5)
	{
		/*
		 * Assert 메서드는 입력으로 넘겨지는 조건이 거짓 일 경우 예외를
		 * 발생시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면 개발
		 * 과정에서 발생하는 실수를 감지하는 것이 가능하다.)
		 * 
		 * 또한, 해당 메서드는 디버그 환경에서만 정상적으로 동작하도록 설계
		 * 되어있기 때문에 릴리즈 환경에서는 해당 메서드 호출에 의한 성능
		 * 저하를 고려하지 않아도 된다는 장점이 존재한다.
		 */
		Debug.Assert(a_nSize >= 1);
		m_oVals = new T[a_nSize];
	}

	/** 인덱서 */
	public T this[int a_nIdx]
	{
		get
		{
			return m_oVals[a_nIdx];
		}
		set
		{
			m_oVals[a_nIdx] = value;
		}
	}

	/** 데이터를 추가한다 */
	public void AddVal(T a_tVal)
	{
		// 배열이 가득 찼을 경우
		if(this.NumVals >= m_oVals.Length)
		{
			System.Array.Resize(ref m_oVals, m_oVals.Length * 2);
		}

		m_oVals[this.NumVals] = a_tVal;
		this.NumVals += 1;
	}

	/** 데이터를 추가한다 */
	public void InsertVal(int a_nIdx, T a_tVal)
	{
		// 배열이 가득 찼을 경우
		if(this.NumVals >= m_oVals.Length)
		{
			System.Array.Resize(ref m_oVals, m_oVals.Length * 2);
		}

		Debug.Assert(a_nIdx >= 0 && a_nIdx <= this.NumVals);

		for(int i = this.NumVals; i > a_nIdx; --i)
		{
			m_oVals[i] = m_oVals[i - 1];
		}

		m_oVals[a_nIdx] = a_tVal;
		this.NumVals += 1;
	}

	/** 데이터를 제거한다 */
	public void RemoveVal(T a_tVal)
	{
		int nResult = this.FindVal(a_tVal);

		// 데이터 제거가 불가능 할 경우
		if(nResult < 0)
		{
			return;
		}

		this.RemoveValAt(nResult);
	}

	/** 데이터를 제거한다 */
	public void RemoveValAt(int a_nIdx)
	{
		Debug.Assert(a_nIdx >= 0 && a_nIdx < this.NumVals);

		for(int i = a_nIdx; i < this.NumVals - 1; ++i)
		{
			m_oVals[i] = m_oVals[i + 1];
		}

		this.NumVals -= 1;
	}

	/** 값을 탐색한다 */
	public int FindVal(T a_tVal)
	{
		for(int i = 0; i < this.NumVals; ++i)
		{
			// 값이 동일 할 경우
			if(m_oVals[i].Equals(a_tVal))
			{
				return i;
			}
		}

		return -1;
	}
	#endregion // 함수
}
