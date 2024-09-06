//#define E07_METHOD_01
//#define E07_METHOD_02
//#define E07_METHOD_03
//#define E07_METHOD_04
//#define E07_METHOD_05
#define E07_METHOD_06

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 메서드 (함수) 란?
 * - 명령문의 특정 부분을 그룹화 시켜서 필요할 때 재사용 할 수 있는 기능을
 * 의미한다. (즉, 메서드를 활용하면 명령문을 작성 할 때 발생하는 중복적인
 * 구문을 줄이는 것이 가능하다.)
 * 
 * C# 메서드 유형
 * - 입력 O, 출력 O		<- int SomeMethod(int a_nVal);
 * - 입력 O, 출력 X		<- void SomeMethod(int a_nVal);
 * - 입력 X, 출력 O		<- int SomeMethod();
 * - 입력 X, 출력 X		<- void SomeMethod();
 *  
 * C# 메서드 구현 방법
 * - 보호 수준 + 반환 형 + 메서드 이름 + 매개 변수 + 메서드 몸체
 * 
 * Ex)
 * public int SomeMethod(int a_nVal01, int a_nVal02)
 * {
 *      // 메서드가 호출 될 때 동작 할 명령문 (메서드 몸체)
 * }
 * 
 * 특정 메서드의 매개 변수 (입력) 는 필요에 따라 여러 개를 명시하는 것이 
 * 가능하지만 반환 형 (출력) 은 여러 개를 명시하는 것이 불가능하다.
 * 
 * 따라서, 특정 메서드가 여러 데이터를 반환하기 위해서는 컬렉션 등을 활용
 * 할 필요가 있다는 것을 알 수 있다.
 */
namespace Example._03010201000201_S_W_Engine_0002.E01.Example.Classes.Example_07
{
	class CE01Example_07
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E07_METHOD_01
			Console.Write("정수 (2 개) 입력 : ");
			string[] oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nLhs);
			int.TryParse(oTokens[1], out int nRhs);

			/*
			 * 특정 메서드가 입력을 요구 할 경우 반드시 메서드를 호출하면서
			 * 해당 메서드가 요구하는만큼 입력 데이터를 명시 할 필요가 있다.
			 * (즉, 입력을 2 개 요구하는 메서드는 반드시 호출 할 때 2 개의
			 * 데이터를 명시해야 된다는 것을 알 수 있다.)
			 * 
			 * 또한, 메서드 호출하고 나면 프로그램의 흐름은 호출 한 메서드로
			 * 이동하기 때문에 호출한 메서드가 완료되기 전까지는 해당 메서드를
			 * 호출한 위치에서 더이상 진행되지 않고 완료를 대기하고 있는
			 * 특징 존재한다.
			 */
			int nSumVal = GetSumVal(nLhs, nRhs);
			int nSubVal = GetSubVal(nLhs, nRhs);
			int nMultiplyVal = GetMultiplyVal(nLhs, nRhs);
			float fDivideVal = GetDivideVal(nLhs, nRhs);

			Console.WriteLine("\n=====> 사칙 연산 <====");
			Console.WriteLine("{0} + {1} = {2}", nLhs, nRhs, nSumVal);
			Console.WriteLine("{0} - {1} = {2}", nLhs, nRhs, nSubVal);
			Console.WriteLine("{0} * {1} = {2}", nLhs, nRhs, nMultiplyVal);
			Console.WriteLine("{0} / {1} = {2}", nLhs, nRhs, fDivideVal);
#elif E07_METHOD_02
			int nVal = 0;

			SetVal(nVal, 10);
			Console.WriteLine("SetVal 메서드 호출 후 : {0}", nVal);

			SetValByRef(ref nVal, 10);
			Console.WriteLine("SetValByRef 메서드 호출 후 : {0}", nVal);
#elif E07_METHOD_03
			int nVal;

			/*
			 * ref 키워드 vs out 키워드
			 * - 두 키워드 모두 특성 메서드에 참조를 전달하는 역할을 수행한다.
			 * ref 키워드는 단순히 참조만을 전달하기 때문에 만약 초기화 되어있지
			 * 않은 지역 변수를 참조로 전달 할 경우 컴파일 에러가 발생한다.
			 * 
			 * (즉, C# 은 기본적으로 초기화 되지 않은 지역 변수는 사용 할 수 없다는
			 * 규칙 존재한다는 것을 알 수 있다.)
			 * 
			 * 따라서, 특정 지역 변수를 ref 키워드로 다른 메서드에 참조로 전달하기
			 * 위해서는 반드시 해당 메서드를 호출하기 전에 지역 변수가 초기화 되어
			 * 있어야한다.
			 * 
			 * 반면, out 키워드는 초기화 되지 않은 지역 변수의 참조를 전달하는 것이
			 * 가능하다는 차이점이 존재한다.
			 * 
			 * 이는 out 키워드로 전달 된 참조는 해당 참조를 전달 받은 메서드에서 
			 * 반드시 특정 값을 설정해 줄 필요가 있기 때문에 C# 컴파일러는 초기화되지
			 * 않은 지역 변수라 하더라도 out 키워드를 통해 참조를 전달하는 것이
			 * 가능하다는 것을 알 수 있다.
			 * 
			 * 단, out 키워드로 참조를 전달 받은 메서드에서는 반드시 해당 참조 변수를
			 * 사용하기 전에 값을 설정 해 줄 필요가 있으며 만약 특정 값을 설정하지
			 * 않았을 경우 컴파일 에러가 발생한다는 특징이 존재한다.
			 */
			//SetValByRef(ref nVal, 10);
			SetValByOut(out nVal, 10);

			Console.WriteLine("결과 : {0}", nVal);
#elif E07_METHOD_04
			int nSumVal01 = GetSumVal(1, 2, 3);
			int nSumVal02 = GetSumVal(1, 2, 3, 4, 5, 6);
			int nSumVal03 = GetSumVal(1, 2, 3, 4, 5, 6, 7, 8, 9);

			Console.WriteLine("결과 : {0}, {1}, {2}", 
				nSumVal01, nSumVal02, nSumVal03);
#elif E07_METHOD_05
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nLhs);
			int.TryParse(oTokens[1], out int nRhs);

			/*
			 * 메서드 오버로딩을 활용하면 자료형에 따라 호출해야되는 메서드를
			 * 직접 판단 할 필요 없이 컴파일러가 해당 작업을 수행하기 때문에
			 * 좀 더 편리하게 메서드를 호출하는 것이 가능하다는 것을 알 수
			 * 있다.
			 */
			int nSumVal = GetSumVal(nLhs, nRhs);
			float fSumVal = GetSumVal((float)nLhs, (float)nRhs);

			Console.WriteLine("결과 : {0}, {1}", nSumVal, fSumVal);
#elif E07_METHOD_06
			int[] oVals = new int[10];
			SetupVals(oVals);

			Console.WriteLine("=====> 배열 요소 <=====");

			for(int i = 0; i < oVals.Length; ++i)
			{
				Console.Write("{0}, ", oVals[i]);
			}

			int nSumVal = GetSumVal(oVals, 0);
			Console.WriteLine("\n\n합계 : {0}", nSumVal);
#endif
		}

#if E07_METHOD_01
		/** 덧셈 결과를 반환한다 */
		public static int GetSumVal(int a_nLhs, int a_nRhs)
		{
			/*
			 * return 키워드란?
			 * - 메서드를 종료하고 프로그램의 흐름을 해당 메서드를 호출한 곳으로
			 * 돌려보내는 역할을 수행한다. 또한, 메서드가 반환 값이 존재 할 경우
			 * 반환 값을 메서드를 호출 곳으로 전달하는 역할도 수행한다.
			 * 
			 * 따라서, 반환 값이 존재하는 메서드는 반드시 return 키워드를 명시해서
			 * 특정 데이터를 메서드를 호출한 곳으로 전달 할 필요가 있다는 것을
			 * 알 수 있다. (즉, 반환 값이 존재하지 않는 메서드는 return 키워드를
			 * 명시하지 않아도 상관 없다는 것을 알 수 있다.)
			 */
			return a_nLhs + a_nRhs;
		}

		/** 뺄셈 결과를 반환한다 */
		public static int GetSubVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs - a_nRhs;
		}

		/** 곱셈 결과를 반환한다 */
		public static int GetMultiplyVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs * a_nRhs;
		}

		/** 나눗셈 결과를 반환한다 */
		public static float GetDivideVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs / (float)a_nRhs;
		}
#elif E07_METHOD_02
		/** 값을 변경한다 */
		public static void SetVal(int a_nVar, int a_nSetVal)
		{
			a_nVar = a_nSetVal;
		}

		/** 값을 변경한다 */
		public static void SetValByRef(ref int a_nVar, int a_nSetVal)
		{
			a_nVar = a_nSetVal;
		}

		/** 임시 메서드 */
		public static void TempMethod()
		{
			/*
			 * 특정 지역 내부에 선언 된 변수는 지역 변수라고 하며 지역 변수는
			 * 해당 변수가 선언 된 지역 외부에서는 사용하는 것이 불가능하다는
			 * 특징이 존재한다.
			 * 
			 * 이는 지역 변수는 해당 변수가 선언 된 지역을 벗어나면 메모리에서
			 * 제거 되는 특징이 존재하기 때문이다. (즉, 지역 변수는 시스템에
			 * 의해서 자동으로 메모리가 관리 된다는 것을 알 수 있다.)
			 * 
			 * 따라서, 아래 명령문은 다른 지역에 존재하는 지역 변수에 접근하는
			 * 시도를 하고 있기 때문에 컴파일 에러가 발생한다는 것을 알 수 있다.
			 * 
			 * 또한, 메서드의 입력 값을 전달 받는 매개 변수도 지역 변수와 동일한
			 * 특징을 지니고 있기 때문에 매개 변수도 해당 메서드 이외의 지역에서는
			 * 접근하는 것이 불가능하다.
			 */
			//nVal = 10;
		}
#elif E07_METHOD_03
		/** 값을 변경한다 */
		public static void SetValByRef(ref int a_nVar, int a_nSetVal)
		{
			a_nVar = a_nSetVal;
		}

		/** 값을 변경한다 */
		public static void SetValByOut(out int a_nVar, int a_nSetVal)
		{
			a_nVar = a_nSetVal;
		}
#elif E07_METHOD_04
		/*
		 * params 키워드란?
		 * - 가변 길이 매개 변수를 선언하는 역할을 수행하는 키워드를 의미한다.
		 * (즉, params 키워드로 명시 된 매개 변수는 입력으로 필요로 하는 데이터의
		 * 개수가 정해져 있지 않다는 것을 알 수 있다.)
		 * 
		 * 일반적으로 매개 변수는 특정 개수로 지정 되어있지만 params 키워드를 통한
		 * 가변 길이 매개 변수를 명시하면 따로 개수가 지정되지 않기 때문에 필요에
		 * 따라 얼마든지 많은 입력 값을 전달 받는 것이 가능하다.
		 */
		/** 합계를 반환한다 */
		public static int GetSumVal(params object[] a_oVals)
		{
			int nSumVal = 0;

			for(int i = 0; i < a_oVals.Length; ++i)
			{
				/*
				 * is 키워드란?
				 * - 데이터가 특정 자료형에 해당하는 데이터인지 검사하는 역할을
				 * 수행한다. 
				 * 
				 * 따라서, object 자료형으로 관리 중에 특정 데이터를 원하는
				 * 자료형에 맞게 형 변환이 필요 할 경우 해당 키워드를 통해 자료형을
				 * 검사 후 형 변환해주면 명령문을 좀 더 안전하게 작성하는 것이
				 * 가능하다는 것을 알 수 있다.
				 */
				// 정수 일 경우
				if(a_oVals[i] is int)
				{
					nSumVal += (int)a_oVals[i];
				}
			}

			return nSumVal;
		}
#elif E07_METHOD_05
		/*
		 * 메서드 오버로딩이란?
		 * - 매개 변수 정보가 다르다면 동일한 이름을 지닌 메서드를 구현 할 수
		 * 있는 기능을 의미한다. (즉, 메서드 오버로딩을 활용하면 특정 기능을 
		 * 수행하는 메서드를 동일한 이름으로 구현하는 것이 가능하다는 것을 알 
		 * 수 있다.)
		 * 
		 * 단, 메서드 오버로딩은 매개 변수 정보를 기반으로 판단하기 때문에
		 * 반환 형은 메서드 오버로딩과는 상관 없다는 겂을 알 수 있다.
		 * 
		 * Ex)
		 * int SomeMethod(int, int);
		 * int SomeMethod(int, int, int);
		 * 
		 * 위의 경우 매개 변수의 개수가 서로 다르기 때문에 메서드 오버로딩
		 * 조건을 만족한다는 것을 알 수 있다.
		 * 
		 * int SomeMethod(int, int);
		 * int SomeMethod(int, float);
		 * 
		 * 위의 경우 매개 변수의 자료 형이 서로 다르기 때문에 메서드 오버로딩
		 * 조건을 만족한다는 것을 알 수 있다.
		 *
		 * int SomeMethod(int, int);
		 * void SomeMethod(int, int);
		 * 
		 * 위의 경우 반환 형이 다르지만 반환 형은 메서드 오버로딩 조건에 해당
		 * 하지 않기 때문에 해당 경우에는 컴파일 에러가 발생한다는 것을 
		 * 알 수 있다.
		 */
		/** 덧셈 결과를 반환한다 */
		public static int GetSumVal(int a_nLhs, int a_nRhs)
		{
			return a_nLhs + a_nRhs;
		}

		/** 덧셈 결과를 반환한다 */
		public static float GetSumVal(float a_fLhs, float a_fRhs)
		{
			return a_fLhs + a_fRhs;
		}
#elif E07_METHOD_06
		/** 값을 설정한다 */
		public static void SetupVals(int[] a_oVals)
		{
			for(int i = 0; i < a_oVals.Length; ++i)
			{
				a_oVals[i] = i + 1;
			}
		}

		/** 덧셈 결과를 반환한다 */
		public static int GetSumVal(int[] a_oVals, int a_nIdx)
		{
			// 마지막 요소 일 경우
			if(a_nIdx >= a_oVals.Length - 1)
			{
				return a_oVals[a_nIdx];
			}

			/*
			 * 재귀호출이란?
			 * - 특정 메서드가 자기 자신을 다시 호출 할 수 있는 기능을 의미한다.
			 * 따라서, 재귀호출 사용 할 경우 무한히 자기 자신을 호출하는 무한
			 * 재귀 현상에 빠지지 않도록 주의 할 필요가 있다.
			 * 
			 * 메서드를 호출하는 것은 내부적으로 추가적인 데이터를 필요로 하기
			 * 때문에 특정 메서드를 너무 많이 호출 할 경우 데이터를 저장하기
			 * 위한 메모리가 부족해짐으로서 프로그램의 크래시가 발생 할 수 있기
			 * 때문이다.
			 */
			return a_oVals[a_nIdx] + GetSumVal(a_oVals, a_nIdx + 1);
		}
#endif
	}
}
