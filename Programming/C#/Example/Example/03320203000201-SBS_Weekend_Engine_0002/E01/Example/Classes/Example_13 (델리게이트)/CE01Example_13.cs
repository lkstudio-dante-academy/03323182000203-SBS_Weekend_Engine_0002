//#define E13_DELEGATE_01
//#define E13_DELEGATE_02
//#define E13_DELEGATE_03
//#define E13_DELEGATE_04
#define E13_DELEGATE_05

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 델리게이트란?
 * - 특정 메서드를 저장 및 제어 할 수 있는 기능을 의미한다. (즉, 델리게이트를
 * 활용하면 메서드를 데이터처럼 특정 메서드의 입력으로 전달하거나 메서드가
 * 반환 값으로 특정 메서드를 반환하는 것이 가능하다.)
 * 
 * 또한, 델리게이트를 활용하면 델리게이트 변수를 통해서 저장되어있는 메서드를
 * 간접적으로 호출하는 것이 가능하다. (즉, 특정 메서드를 호출하기 위해서 해당
 * 메서드의 이름을 직접적으로 명시하지 않아도 메서드 호출이 가능하다는 것을
 * 알 수 있다.)
 * 
 * 따라서, 델리게이트는 명령문을 조립하는 수단으로 활용된다는 것을 알 수 있다.
 * 
 * C# 델리게이트 선언 방법
 * - delegate + 반환형 + 델리게이트 이름 + 매개 변수
 * 
 * Ex)
 * delegate int Compare(int a_nVal01, int a_nVal02);
 * 
 * public int CompareByAscending(int a_nVal01, int a_nVal02) {
 *     // Do Something
 * }
 * 
 * Compare oCompare = CompareByAscending;
 * oCompare(10, 20);
 */
namespace Example._03320282000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_13
{
	class CE01Example_13
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E13_DELEGATE_01
			Console.Write("수식 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nVal01);
			int.TryParse(oTokens[2], out int nVal02);

			Calc oCalc = GetCalc(oTokens[1]);

			// 올바른 수식을 입력했을 경우
			if(oCalc != null) {
				Console.WriteLine("결과 : {0}", oCalc(nVal01, nVal02));
			} else {
				Console.WriteLine("잘못된 수식을 입력했습니다.");
			}
#elif E13_DELEGATE_02
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nVal01);
			int.TryParse(oTokens[1], out int nVal02);

			Compare oCompare01 = CompareByAscending;
			Compare oCompare02 = CompareByDescending;

			/*
			 * 델리게이트를 이용해서 변수를 선언하면 해당 변수에 특정 메서드를 저장
			 * 후 델리게이트 변수를 이용해서 메서드를 간접적으로 호출하는 것이
			 * 가능하다.
			 */
			Console.WriteLine("비교 결과 : {0}, {1}",
				oCompare01(nVal01, nVal02), oCompare02(nVal01, nVal02));
#elif E13_DELEGATE_03
			var oMonster = new CMonster(50);

			var oPlayer = new CPlayer(oMonster);
			oPlayer.Attack();
			oPlayer.UseSkill();
#elif E13_DELEGATE_04
			var oRandom = new Random();

			var oValList01 = new List<int>();
			var oValList02 = new List<float>();

			for(int i = 0; i < 10; ++i) {
				oValList01.Add(oRandom.Next(0, 100));
				oValList02.Add((float)oRandom.NextDouble() * 100.0f);
			}

			Console.WriteLine("=====> 정렬 전 <=====");
			PrintVals(oValList01);
			PrintVals(oValList02);

			SortVals(oValList01, CompareByAscending);
			SortVals(oValList02, CompareByDecending);

			Console.WriteLine("\n=====> 정렬 후 <=====");
			PrintVals(oValList01);
			PrintVals(oValList02);
#elif E13_DELEGATE_05
			/*
			 * 델리게이트 체인이란?
			 * - 델리게이트 변수에 여러 메서드를 등록 및 제어 할 수 있는 기능을 의미한다.
			 * (즉, 델리게이트 체인을 활용하면 특정 이벤트가 발생했을 때 처리 해야 될
			 * 여러 단계를 순차적으로 호출하는 구조를 만들어내는 것이 가능하다.)
			 * 
			 * 델리게이트 변수에 특정 메서드를 추가하거나 제거하는 것은 +, - 연산자를
			 * 통해서 가능하며 = (할당 연산자) 를 사용해서 특정 메서드를 할당 할 경우
			 * 기존에 생성 된 델리게이트 체인 정보는 제거 된다는 특징이 존재한다.
			 */
			EventHandler oEventHandler = HandleEvent01;
			oEventHandler += HandleEvent02;
			oEventHandler += HandleEvent03;

			Console.WriteLine("=====> 델리게이트 체인 호출 - 1 <=====");
			oEventHandler(string.Empty);

			oEventHandler -= HandleEvent02;

			Console.WriteLine("\n=====> 델리게이트 체인 호출 - 2 <=====");
			oEventHandler(string.Empty);

			oEventHandler = HandleEvent03;

			Console.WriteLine("\n=====> 델리게이트 체인 호출 - 3 <=====");
			oEventHandler(string.Empty);
#endif
		}

#if E13_DELEGATE_01
		public delegate decimal Calc(int a_nVal01, int a_nVal02);

		/** 계산 메서드를 반환한다 */
		public static Calc GetCalc(string a_oOperator) {
			switch(a_oOperator) {
				case "+": return GetSumVal;
				case "-": return GetSubVal;
				case "*": return GetMultiplyVal;
				case "/": return GetDivideVal;
			}

			return null;
		}

		/** 덧셈 결과를 반환한다 */
		public static decimal GetSumVal(int a_nVal01, int a_nVal02) {
			return a_nVal01 + a_nVal02;
		}

		/** 뺄셈 결과를 반환한다 */
		public static decimal GetSubVal(int a_nVal01, int a_nVal02) {
			return a_nVal01 - a_nVal02;
		}

		/** 곱셈 결과를 반환한다 */
		public static decimal GetMultiplyVal(int a_nVal01, int a_nVal02) {
			return a_nVal01 * a_nVal02;
		}

		/** 나눗셈 결과를 반환한다 */
		public static decimal GetDivideVal(int a_nVal01, int a_nVal02) {
			return a_nVal01 / (decimal)a_nVal02;
		}
#elif E13_DELEGATE_02
		delegate int Compare(int a_nVal01, int a_nVal02);

		/** 오름차순으로 비교한다 */
		public static int CompareByAscending(int a_nVal01, int a_nVal02) {
			return a_nVal01 - a_nVal02;
		}

		/** 내림차순으로 비교한다 */
		public static int CompareByDescending(int a_nVal01, int a_nVal02) {
			return a_nVal02 - a_nVal01;
		}
#elif E13_DELEGATE_03
		public delegate void Event(CMonster a_oSender);

		/** 플레이어 */
		public class CPlayer {
			private CMonster m_oTarget = null;

			/** 생성자 */
			public CPlayer(CMonster a_oTarget) {
				m_oTarget = a_oTarget;
				m_oTarget.OnDeath = this.HandleOnDeathMonster;
			}

			/** 공격한다 */
			public void Attack() {
				Console.WriteLine("몬스터를 공격했습니다.");
				m_oTarget.OnHit(this, 10);
			}

			/** 스킬을 사용한다 */
			public void UseSkill() {
				Console.WriteLine("몬스터를 스킬로 공격했습니다.");
				m_oTarget.OnHit(this, 50);
			}

			/** 몬스터가 제거 되었을 경우 */
			private void HandleOnDeathMonster(CMonster a_oSender) {
				Console.WriteLine("몬스터가 제거 되었습니다.");
			}
		}

		/** 몬스터 */
		public class CMonster {
			public Event OnDeath = null;
			private int m_nHP = 0;

			/** 생성자 */
			public CMonster(int a_nHP) {
				m_nHP = a_nHP;
			}

			/** 피격 되었을 경우 */
			public void OnHit(CPlayer a_oAttacker, int a_nDamage) {
				m_nHP -= a_nDamage;

				// 체력이 없을 경우
				if(m_nHP <= 0) {
					OnDeath(this);
				}
			}
		}
#elif E13_DELEGATE_04
		public delegate int Compare<T>(T a_tVal01, T a_tVal02);

		/** 오름차순으로 비교한다 */
		public static int CompareByAscending<T>(T a_tVal01, T a_tVal02) where T : IComparable<T> {
			return a_tVal01.CompareTo(a_tVal02);
		}

		/** 내림차순으로 비교한다 */
		public static int CompareByDecending<T>(T a_tVal01, T a_tVal02) where T : IComparable<T> {
			return a_tVal02.CompareTo(a_tVal01);
		}

		/** 값을 정렬한다 */
		public static void SortVals<T>(List<T> a_oValList,
			Compare<T> a_oCompare) where T : IComparable<T> {
			for(int i = a_oValList.Count - 1; i >= 1; --i) {
				for(int j = 0; j < i; ++j) {
					// 정렬 기준에 맞지 않을 경우
					if(a_oCompare(a_oValList[j], a_oValList[j + 1]) > 0) {
						T tTemp = a_oValList[j];
						a_oValList[j] = a_oValList[j + 1];
						a_oValList[j + 1] = tTemp;
					}
				}
			}
		}

		/** 값을 출력한다 */
		public static void PrintVals<T>(List<T> a_oValList) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine();
		}
#elif E13_DELEGATE_05
		public delegate void EventHandler(string a_oEvent);

		/** 이벤트를 처리한다 */
		public static void HandleEvent01(string a_oEvent)
		{
			Console.WriteLine("HandleEvent01 호출");
		}

		/** 이벤트를 처리한다 */
		public static void HandleEvent02(string a_oEvent)
		{
			Console.WriteLine("HandleEvent02 호출");
		}

		/** 이벤트를 처리한다 */
		public static void HandleEvent03(string a_oEvent)
		{
			Console.WriteLine("HandleEvent03 호출");
		}
#endif
	}
}
