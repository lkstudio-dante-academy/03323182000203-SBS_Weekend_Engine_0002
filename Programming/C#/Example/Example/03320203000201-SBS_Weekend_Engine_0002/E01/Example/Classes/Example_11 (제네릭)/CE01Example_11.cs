//#define E11_GENERIC_01
//#define E11_GENERIC_02
#define E11_GENERIC_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 제네릭이란?
 * - 자료형을 명시하지 않고 메서드 또는 클래스를 구현 할 수 있는 기능을 의미한다.
 * (즉, 제네릭을 활용하면 자료형에 따로 동일한 동작을 수행하는 메서드를 여러개
 * 구현 할 필요 없이 하나만 구현하는 것이 가능하다.)
 * 
 * 따라서, 제네릭은 자료형에 상관 없이 동작하는 메서드 또는 클래스를 구현하는데
 * 활용 된다는 것을 알 수 있다.
 * 
 * Ex)
 * public void SomeMethod<T>(T a_tVal);
 * 
 * public class CSomeClass<T> {
 *     // Do Something
 * }
 * 
 * 특정 메서드나 클래스를 제네릭으로 구현하기 위해서는 반드시 제네릭 형식 인자를
 * 명시해줘야한다.
 * 
 * 제네릭 형식 인자란?
 * - 기존에 자료형 명시 될 위치를 임시적으로 대체하고 있는 키워드를 의미한다.
 * 
 * 제네릭 형식 인자는 위치는 메서드의 경우 호출 되었을 때 비로서 실질적인 
 * 자료형으로 변경 된다.
 * 
 * 따라서, 제네릭 형식 인자는 임시적으로 자료형의 역할을 수행한다는 것을 
 * 알 수 있다.
 */
namespace Example._03320203000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_11
{
	class CE01Example_11
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E11_GENERIC_01
			int nLhs = 10;
			int nRhs = 20;

			float fLhs = 10.0f;
			float fRhs = 20.0f;

			string oLhs = "ABC";
			string oRhs = "DEF";

			Console.WriteLine("=====> 교환 전 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);
			Console.WriteLine("{0}, {1}", fLhs, fRhs);
			Console.WriteLine("{0}, {1}", oLhs, oRhs);

			/*
			 * 제네릭 메서드를 호출하기 위해서는 반드시 해당 메서드의 제네릭 형식 인자를
			 * 대체 할 자료형을 명시해야한다. 만약, 자료형 명시를 생략 할 경우 C# 
			 * 컴파일러에 의해서 자동으로 자료형이 결정되는 특징이 존재한다.
			 * 
			 * 단, C# 컴파일러 자료형을 자동을 결정 할 수 있는 경우는 매개 변수를 기반으로
			 * 자료형을 추론하기 때문에 매개 변수만으로 자료형을 추론 할 수 없는 상황에서는
			 * 반드시 특정 자료형을 직접 명시해줘야한다.
			 */
			Swap(ref nLhs, ref nRhs);
			Swap(ref fLhs, ref fRhs);
			Swap(ref oLhs, ref oRhs);

			Console.WriteLine("\n=====> 교환 후 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);
			Console.WriteLine("{0}, {1}", fLhs, fRhs);
			Console.WriteLine("{0}, {1}", oLhs, oRhs);
#elif E11_GENERIC_02
			var oRandom = new Random();

			var oValList01 = new List<int>();
			var oValList02 = new List<float>();

			for(int i = 0; i < 10; ++i) {
				oValList01.Add(oRandom.Next(0, 100));
				oValList02.Add(oRandom.Next(0, 100) / 100.0f);
			}

			Console.WriteLine("=====> 교환 전 <=====");
			PrintVals(oValList01);
			PrintVals(oValList02);

			SortVals(oValList01);
			SortVals(oValList02);

			Console.WriteLine("\n=====> 교환 후 <=====");
			PrintVals(oValList01);
			PrintVals(oValList02);
#elif E11_GENERIC_03
			/*
			 * 제네릭 클래스는 제네릭 메서드와 달리 제네릭 형식 인자를 대체 할 자료형을
			 * 반드시 직접 명시해야되는 차이점이 존재한다.
			 * 
			 * 이는, 클래스는 메서드와 달리 객체를 생성하는 구문만으로는 제네릭 형식 인자의
			 * 자료형을 유추하는 것이 불가능하기 때문이다.
			 */
			var oValList01 = new CArrayList<int>();
			var oValList02 = new CArrayList<float>();

			for(int i = 0; i < 10; ++i)
			{
				oValList01.Add(i + 1);
				oValList02.Add(i + 1.0f);
			}

			oValList01.Remove(5);
			oValList02.Remove(5.0f);

			oValList01.Insert(0, 100);
			oValList02.Insert(0, 100.0f);

			Console.WriteLine("=====> 배열 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < oValList02.Count; ++i)
			{
				Console.Write("{0}, ", oValList02[i]);
			}

			Console.WriteLine();
#endif
		}

#if E11_GENERIC_01
		/** 값을 교환한다 */
		public static void Swap<T>(ref T a_tLhs, ref T a_tRhs) {
			T tTemp = a_tLhs;
			a_tLhs = a_tRhs;
			a_tRhs = tTemp;
		}
#elif E11_GENERIC_02
		/** 값을 출력한다 */
		public static void PrintVals<T>(List<T> a_oValList) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine();
		}

		/*
		 * C# 제네릭은 기본적으로 모든 자료형에 동작하도록 명령문이 구성 되어 있어야한다.
		 * (즉, 특정 자료형에만 동작하는 명령문을 작성하는 것에 제약이 있다다는 것을
		 * 알 수 있다.)
		 *  
		 * 만약, 특정 조건을 만족하는 자료형에만 동작하도록 명령문을 구성하고 싶다면
		 * where 키워드를 사용해야한다.
		 * 
		 * C# where 키워드 사용 방법
		 * - where T : class			<- 참조 형식 자료형을 제한
		 * - where T : struct			<- 값 형식 자료형으로 제한
		 * - where T : CSomeClass		<- 명시 된 클래스를 직/간접적으로 상속하는 자료형으로 제한
		 * - where T : ISomeInterface	<- 명시 된 인터페이스를 직/간접적으로 따르는 자료형으로 제한
		 */
		/** 값을 정렬한다 */
		public static void SortVals<T>(List<T> a_oValList) where T : IComparable<T> {
			for(int i = a_oValList.Count - 1; i >= 1; --i) {
				for(int j = 0; j < i; ++j) {
					// 정렬 기준에 맞지 않을 경우
					if(a_oValList[j].CompareTo(a_oValList[j + 1]) > 0) {
						T tTemp = a_oValList[j];
						a_oValList[j] = a_oValList[j + 1];
						a_oValList[j + 1] = tTemp;
					}
				}
			}
		}
#elif E11_GENERIC_03
		/** 배열 리스트 */
		public class CArrayList<T>
		{
			private int m_nNumVals = 0;
			private T[] m_oVals = null;

			public int Count => m_nNumVals;

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

			/** 생성자 */
			public CArrayList(int a_nSize = 5)
			{
				m_oVals = new T[a_nSize];
			}

			/** 값을 추가한다 */
			public void Add(T a_tVal)
			{
				// 배열이 가득 찼을 경우
				if(m_nNumVals >= m_oVals.Length)
				{
					Array.Resize(ref m_oVals, m_oVals.Length * 2);
				}

				m_oVals[m_nNumVals++] = a_tVal;
			}

			/** 값을 추가한다 */
			public void Insert(int a_nIdx, T a_tVal)
			{
				// 배열의 범위를 벗어났을 경우
				if(a_nIdx < 0 || a_nIdx >= m_nNumVals)
				{
					return;
				}

				// 배열이 가득 찼을 경우
				if(m_nNumVals >= m_oVals.Length)
				{
					Array.Resize(ref m_oVals, m_oVals.Length * 2);
				}

				for(int i = m_nNumVals; i > a_nIdx; --i)
				{
					m_oVals[i] = m_oVals[i - 1];
				}

				m_nNumVals += 1;
				m_oVals[a_nIdx] = a_tVal;
			}

			/** 값을 제거한다 */
			public void Remove(T a_tVal)
			{
				int nResult = this.FindVal(a_tVal);

				// 값이 존재 할 경우
				if(nResult >= 0)
				{
					this.RemoveAt(nResult);
				}
			}

			/** 값을 제거한다 */
			public void RemoveAt(int a_nIdx)
			{
				for(int i = a_nIdx; i < m_nNumVals - 1; ++i)
				{
					m_oVals[i] = m_oVals[i + 1];
				}

				m_nNumVals -= 1;
			}

			/** 값을 탐색한다 */
			public int FindVal(T a_tVal)
			{
				for(int i = 0; i < m_nNumVals; ++i)
				{
					// 값이 존재 할 경우
					if(m_oVals[i].Equals(a_tVal))
					{
						return i;
					}
				}

				return -1;
			}
		}
#endif
	}
}
