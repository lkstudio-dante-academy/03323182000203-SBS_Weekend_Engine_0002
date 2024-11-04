//#define E10_CLASS_01
#define E10_CLASS_02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if E10_CLASS_01
/*
 * 정적 클래스란?
 * - 일반적인 클래스와 달리 정적 멤버만 명시 할 수 있는 클래스를 의미한다.
 * (즉, 정적 클래스는 정적 멤버만 포함 할 수 있기 때문에 해당 클래스 통해
 * 객체를 생성하는 것은 불가능하다는 것을 알 수 있다.)
 * 
 * 또한, 정적 클래스는 상속을 지원하지 않기 때문에 일반적으로 상수 등을 선언
 * 할 때 주로 활용된다.
 */
/** 정적 클래스 */
public static class CExtension {
	/*
	 * 확장 메서드란?
	 * - 기존 클래스가 지니고 있는 기능을 상속이 아닌 메서드를 이용해서 확장 
	 * 시킬 수 있는 기능을 의미한다. (즉, 확장 메서드를 활용하면 제 3 자가 
	 * 작성해서 라이브러리 형태로 제공되는 클래스에 새로운 기능을 추가하는 것이 
	 * 가능하다.)
	 * 
	 * 따라서, 확장 메서드는 기존에 클래스를 수정하지 않고 새로운 기능을 추가 
	 * 시킬 수 있는 장점이 존재한다.
	 * 
	 * 단, 메서드 형태로 기능을 확장하는 것이기 때문에 기존 클래스에서 외부로 공개 
	 * 되지 않은 기능은 사용하는 것이 불가능하다.
	 * 
	 * 확장 메서드는 오직 정적 클래스에만 명시하는 것이 가능하며 확장 메서드가 포함
	 * 된 정적 클래스는 반드시 아무런 영역에도 포함되지 않아야한다. (즉, 특정
	 * 클래스 내부에 존재하는 정적 클래스에는 확장 메서드를 구현하는 것이 불가능하다는
	 * 것을 알 수 있다.)
	 */
	/** 합계를 반환한다 */
	public static int GetSumVal(this List<int> a_oValList) {
		int nSumVal = 0;

		for(int i = 0; i < a_oValList.Count; ++i) {
			nSumVal += a_oValList[i];
		}

		return nSumVal;
	}

	/** 값을 출력한다 */
	public static void PrintVals(this List<int> a_oValList) {
		for(int i = 0; i < a_oValList.Count; ++i) {
			Console.Write("{0}, ", a_oValList[i]);
		}

		Console.WriteLine();
	}
}
#endif

namespace Example._03020203000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_10
{
	class CE01Example_10
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E10_CLASS_01
			var oRandom = new Random();
			var oValList = new List<int>();

			for(int i = 0; i < 10; ++i) {
				oValList.Add(oRandom.Next(0, 100));
			}

			Console.WriteLine("=====> 리스트 요소 <=====");
			oValList.PrintVals();

			int nSumVal = oValList.GetSumVal();
			Console.WriteLine("\n합계 : {0}", nSumVal);
#elif E10_CLASS_02
			/*
			 * 클래스 특정 인터페이스를 따르고 있을 경우 해당 인터페이스로 객체를 참조하는
			 * 것이 가능하다. (즉, 인터페이스를 따르고 있는 객체가 정확하게 어떤 객체인지
			 * 모른다고 하더라도 해당 객체를 제어하는 것이 가능하다.)
			 */
			IPlayable oPlayable = new CCharacter();
			oPlayable.Attack();
			oPlayable.Move(10, 10);
#endif
		}

#if E10_CLASS_02
		/*
		 * 인터페이스란?
		 * - 특정 사물 간에 상호 작용을 일으킬 수 있는 수단을 의미한다. (즉, 프로그래밍
		 * 언어에서 인터페이스란 특정 객체 간에 상호 작용을 할 수 있는 메서드를
		 * 의미한다는 것을 알 수 있다.)
		 * 
		 * C# 인터페이스 선언 방법
		 * - interface + 인터페이스 이름 + 메서드 목록
		 * 
		 * Ex)
		 * interface IPlayable {
		 *     void Attack();
		 *     void Move();
		 *     void Jump();
		 * }
		 */
		/** 플레이 가능 인터페이스 */
		public interface IPlayable
		{
			/** 공격한다 */
			void Attack();

			/** 이동한다 */
			void Move(int a_nX, int a_nY);
		}

		/** 캐릭터 */
		public class CCharacter : IPlayable
		{
			private int m_nPosX = 0;
			private int m_nPosY = 0;

			/** 공격한다 */
			public void Attack()
			{
				Console.WriteLine("플레이어가 공격을 했습니다.");
			}

			/** 이동한다 */
			public void Move(int a_nX, int a_nY)
			{
				m_nPosX += a_nX;
				m_nPosY += a_nY;

				Console.WriteLine("이동 후 위치 : {0}, {1}",
					m_nPosX, m_nPosY);
			}
		}
#endif
	}
}
