//#define E12_DATA_TYPE_01
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
 * 
 * 열거형이란?
 * - 심볼릭 상수를 선언 할 수 있는 기능을 의미한다. (즉, 열거형을 활용하면
 * 프로그램에서 사용되는 특정 데이터를 식별하기 위한 식별자로 사용하는 것이
 * 가능하다.)
 * 
 * 상수란?
 * - 변수와 같이 특정 데이터를 저장하거나 읽어들일 수 있는 공간을 의미한다.
 * 
 * 단, 상수는 변수와 달리 한번 값이 저장되면 더이상 해당 공간의 값을 변경
 * 하는 것은 불가능하고 읽어만 들일 수 있는 차이점이 존재한다. (즉, 상수에
 * 값을 저장 할 수 있는 시점은 상수가 선언되는 초기화 시점에만 가능하다는
 * 것을 알 수 있다.)
 * 
 * C# 상수 종류
 * - 리터널 상수
 * - 심볼릭 상수
 * 
 * 리터널 상수란?
 * - 이름이 존재하지 않는 상수를 의미한다. (즉, 리터널 상수는 이름이 존재하지
 * 않기 때문에 해당 상수는 재활용하는 것이 불가능하다.)
 * 
 * 심볼릭 상수란?
 * - 이름이 존재하는 상수를 의미한다. (즉, 심볼릭 상수는 이름이 존재하기
 * 때문에 필요에 언제든지 재활용하는 것이 가능하다.)
 * 
 * C# 열거형 선언 방법
 * - enum + 열거형 이름 + 열거형 상수
 * 
 * Ex)
 * enum EItemType {
 *     NONE = -1,
 *     GOLD,
 *     POTION,
 *     EQUIPMENT,
 *     MAX_VAL
 * }
 * 
 * EItemType eItemType = EItemType.NONE;
 */
namespace Example._03320282000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_12
{
	class CE01Example_12
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
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
			var oRandom = new Random();
			var eItemType = (EItemType)oRandom.Next(0, (int)EItemType.MAX_VAL);

			switch(eItemType)
			{
				case EItemType.GOLD:
					Console.WriteLine("골드를 획득했습니다.");
					break;
				case EItemType.POTION:
					Console.WriteLine("물약을 획득했습니다.");
					break;
				case EItemType.EQUIPMENTS:
					Console.WriteLine("장비를 획득했습니다.");
					break;
			}
#endif
		}

#if E12_DATA_TYPE_01
		/** 데이터 */
		public struct STData {
			public int m_nVal;
			public float m_fVal;

			/*
			 * 구조체는 클래스와 달리 항상 기본 생성자가 자동으로 구현 되기 때문에
			 * 사용자 (프로그래머) 가 명시적으로 기본 생성자를 구현하는 것이
			 * 불가능하다. (즉, 구조체는 기본 생성자 이외의 생성자를 구현해도 항상
			 * 기본 생성자가 자동으로 구현 된다는 것을 알 수 있다.)
			 * 
			 * 또한, 구조체의 생성자는 클래스와 달리 생성자 내부에서 구조체에
			 * 존재하는 모든 멤버를 초기화 시켜줘야한다. (즉, 하나라도 초기화
			 * 시키지 않았을 경우 컴파일 에러가 발생한다는 것을 알 수 있다.)
			 */
			/** 생성자 */
			public STData(int a_nVal, float a_fVal) {
				m_nVal = a_nVal;
				m_fVal = a_fVal;
			}
		}
#elif E12_DATA_TYPE_02
		/** 아이템 타입 */
		public enum EItemType
		{
			NONE = -1,
			GOLD,
			POTION,
			EQUIPMENTS,
			MAX_VAL
		}
#endif
	}
}
