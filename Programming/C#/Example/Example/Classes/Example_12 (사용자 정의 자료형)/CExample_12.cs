#define E12_DATA_TYPE_01
#define E12_DATA_TYPE_02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 사용자 정의 자료형이란?
 * - 언어가 기본적으로 제공해주는 자료형이 아니라 사용자 (프로그래머) 가 필요에
 * 따라 직접 정의해서 사용하는 자료형을 의미한다. (즉, 사용자 정의 자료형을 
 * 활용하면 제작하는 프로그램에 맞는 전용 자료형을 정의하는 것이 가능하다.)
 * 
 * C# 사용자 정의 자료형 종류
 * - 클래스
 * - 구조체
 * - 열거형
 * 
 * 구조체란?
 * - 변수와 메서드를 하나의 그룹을 관리 할 수 있는 자료형을 의미한다. (즉,
 * 구조체의 기본적인 동작은 클래스와 유사하다는 것을 알 수 있다.)
 * 
 * 구조체는 값 형식 자료형에 해당하기 때문에 가비지 컬렉션에 의해 메모리가
 * 관리되는 클래스보다 좀 더 가벼운 데이터를 표현하는데 적합하다는 것을 알 수
 * 있다.
 * 
 * C# 구조체 구현 방법
 * - struct + 구조체 이름 + 구조체 멤버 (변수, 메서드 등등...)
 * 
 * Ex)
 * public struct STData {
 *     public int m_nVal;
 *     public float m_fVal;
 * }
 */
namespace Example.Classes.Example_12 {
	class CExample_12 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E12_DATA_TYPE_01
			var stData01 = new STData();
			stData01.m_nVal = 10;
			stData01.m_fVal = 3.14f;

			var stData02 = new STData() {
				m_nVal = 10,
				m_fVal = 3.14f
			};

			Console.WriteLine("=====> 데이터 - 1 <=====");
			Console.WriteLine("{0}, {1}", stData01.m_nVal, stData01.m_fVal);

			Console.WriteLine("\n=====> 데이터 - 2 <=====");
			Console.WriteLine("{0}, {1}", stData02.m_nVal, stData02.m_fVal);
#elif E12_DATA_TYPE_02

#endif
		}

#if E12_DATA_TYPE_01
		/** 데이터 */
		public struct STData {
			public int m_nVal;
			public float m_fVal;
		}
#elif E12_DATA_TYPE_02

#endif
	}
}
