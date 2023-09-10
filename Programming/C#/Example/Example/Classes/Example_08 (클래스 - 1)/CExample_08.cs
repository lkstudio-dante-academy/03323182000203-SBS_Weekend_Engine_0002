//#define E08_CLASS_01
//#define E08_CLASS_02
#define E08_CLASS_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 클래스란?
 * - C# 이 제공하는 사용자 정의 자료형 중 하나로서 변수와 메서드를 하나의
 * 그룹으로 관리 할 수 있는 기능을 의미한다. (즉, 클래스를 활용하면 관련 된
 * 변수와 메서드를 하나의 대상으로 제어하는 것이 가능하다는 것을 알 수 있다.)
 * 
 * 또한, 클래스를 활용하면 특정 사물을 표현하는 것이 가능하기 때문에 클래스는
 * 객체 지향 프로그래밍의 가장 핵심 기능이라는 것을 알 수 있다. (즉, 사물의
 * 속성은 변수, 사물의 행위는 메서드로 표현함으로서 하나의 사물을 정의하는
 * 것이 가능하다.)
 * 
 * C# 클래스 구현 방법
 * - class + 클래스 이름 + 클래스 멤버 (변수, 메서드 등등...)
 * 
 * Ex)
 * class CCharacter {
 *      public int m_nLV = 0;
 *      public int m_nHP = 0;
 *      public int m_nATK = 0;
 *      
 *      public void ShowInfo() {
 *           // Do Something
 *      }
 * }
 */
namespace Example.Classes.Example_08 {
	class CExample_08 {
		/** 초기화 */
		public static void Start(string[] args) {
#if E08_CLASS_01
			CCharacter oCharacter01 = new CCharacter();
			oCharacter01.m_nLV = 1;
			oCharacter01.m_nHP = 10;
			oCharacter01.m_nATK = 15;

			CCharacter oCharacter02 = new CCharacter();
			oCharacter02.m_nLV = 15;
			oCharacter02.m_nHP = 150;
			oCharacter02.m_nATK = 100;

			Console.WriteLine("=====> 캐릭터 1 정보 <=====");
			oCharacter01.ShowInfo();

			Console.WriteLine("\n=====> 캐릭터 2 정보 <=====");
			oCharacter02.ShowInfo();
#elif E08_CLASS_02
			CCharacter oCharacter01 = new CCharacter(1, 10, 15);

			CCharacter oCharacter02 = new CCharacter();
			oCharacter02.SetLV(15);
			oCharacter02.SetHP(150);
			oCharacter02.SetATK(100);

			Console.WriteLine("=====> 캐릭터 1 정보 <=====");
			oCharacter01.ShowInfo();

			Console.WriteLine("\n=====> 캐릭터 2 정보 <=====");
			oCharacter02.ShowInfo();
#elif E08_CLASS_03
			CCharacter oCharacter = new CCharacter();
			oCharacter.LV = 1;
			oCharacter.HP = 10;
			oCharacter.ATK = 15;
			oCharacter.DEF = 20;

			Console.WriteLine("=====> 캐릭터 정보 <=====");
			oCharacter.ShowInfo();
#endif
		}

#if E08_CLASS_01
		/** 캐릭터 */
		class CCharacter {
			public int m_nLV = 0;
			public int m_nHP = 0;
			public int m_nATK = 0;

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("레벨 : {0}", m_nLV);
				Console.WriteLine("체력 : {0}", m_nHP);
				Console.WriteLine("공격력 : {0}", m_nATK);
			}
		}
#elif E08_CLASS_02
		/*
		 * 접근 제어 지시자 (한정자) 란?
		 * - 클래스에 존재하는 멤버 (변수, 메서드 등등...) 을 외부로 보호하기
		 * 위한 보호 수준을 명시할 수 있는 키워드를 의미한다.
		 * 
		 * 클래스는 사물을 표현하기 위한 수단이기 때문에 사물이 지니고 있는
		 * 여러 정보 중 외부에서 함부로 조작되면 안되는 민감한 정보는 외부로부터
		 * 보호 할 필요가 있다. (즉, 민감한 데이터를 보호함으로써 객체가 동작하는데 
		 * 발생하는 여러 문제를 줄일 수 있다는 것을 알 수 있다.)
		 * 
		 * C# 접근 제어 지시자 (한정자) 종류
		 * - public			<- 클래스 내/외부에서 모두 접근 가능
		 * - protected		<- 자식 클래스에서 접근 가능
		 * - private		<- 클래스 내부에서만 접근 가능
		 * 
		 * 일반적으로 변수를 선언 할 때는 private 보호 수준을 명시하며
		 * 메서드를 구현 할 때는 public 보호 수준을 명시하는 것이 일반적인
		 * 관례이다. (즉, 특별한 경우가 아니라고 한다면 데이터에 해당하는
		 * 변수는 항상 외부로부터 보호하는 습관을 들이는 것을 추천한다.)
		 */
		/** 캐릭터 */
		class CCharacter {
			private int m_nLV = 0;
			private int m_nHP = 0;
			private int m_nATK = 0;

			/*
			 * 생성자란?
			 * - 객체가 생성 될 때 호출 되는 메서드를 의미하며 모든 클래스는
			 * 특정 객체를 생성하기 위해서 반드시 생성자를 구현 할 필요가
			 * 있다. (즉, 객체가 생성되는 과정에서 해당 객체를 초기화하기
			 * 위해서 생성자가 반드시 호출 된다는 것을 알 수 있다.)
			 * 
			 * 생성자는 클래스 이름과 동일한 이름을 지니는 특별한 메서드이며
			 * 다른 메서드와 달리 반환 형이 존재하지 않는 차이점이 있다.
			 * 
			 * 만약, 특정 클래스가 아무런 생성자도 구현하지 않았을 경우 C#
			 * 컴파일러는 자동으로 기본 생성자를 추가시켜주는 특징이 존재한다.
			 * 
			 * 단, 컴파일러가 기본 생성자를 자동으로 추가시켜주는 경우는
			 * 아무런 생성자도 없을 경우이며 만약 특정 클래스가 하나라도 생성자를
			 * 구현했을 경우 C# 컴파일러는 더이상 자동으로 기본 생성자를 추가시켜주지
			 * 않는다는 것을 알 수 있다.
			 * 
			 * 따라서, 객체가 생성되기 위해서는 반드시 생성자 호출이 필요하며 만약
			 * 생성자가 호출되지 않았다면 이는 곧 객체 생성이 완료되지 않았다는 것을
			 * 의미한다.
			 */
			/** 생성자 */
			public CCharacter() {
				// Do Something
			}

			/** 생성자 */
			public CCharacter(int a_nLV, int a_nHP, int a_nATK) {
				m_nLV = a_nLV;
				m_nHP = a_nHP;
				m_nATK = a_nATK;
			}

			/** 레벨을 변경한다 */
			public void SetLV(int a_nLV) {
				m_nLV = a_nLV;
			}

			/** 체력을 변경한다 */
			public void SetHP(int a_nHP) {
				m_nHP = a_nHP;
			}

			/** 공격력을 변경한다 */
			public void SetATK(int a_nATK) {
				m_nATK = a_nATK;
			}

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("레벨 : {0}", m_nLV);
				Console.WriteLine("체력 : {0}", m_nHP);
				Console.WriteLine("공격력 : {0}", m_nATK);
			}
		}
#elif E08_CLASS_03
		/** 캐릭터 */
		class CCharacter {
			private int m_nLV = 0;
			private int m_nHP = 0;
			private int m_nATK = 0;

			/*
			 * 프로퍼티란?
			 * - 클래스 외부에서 멤버 변수에 접근할 때 전통적으로 사용하는
			 * Getter/Setter 방식을 좀 더 편하게 작성하기 위한 기능을 의미
			 * 한다. (즉, 프로퍼티를 활용하면 접근 메서드를 좀 더 수월하게
			 * 작성하는 것이 가능하다.)
			 * 
			 * 또한, 프로퍼티는 사용 방식이 변수와 유사하기 때문에 기존의
			 * 접근 메서드를 사용하는 방식보다 코드가 좀 더 친숙하다는 장점이
			 * 존재한다.
			 * 
			 * 따라서, C# 에서는 특별한 이유가 없다면 기존의 접근 메서드보다
			 * 프로퍼티를 활용하는 것을 추천한다.
			 */
			public int LV {
				get {
					return m_nLV;
				} set {
					m_nLV = value;
				}
			}

			public int HP {
				get {
					return m_nHP;
				}
				set {
					m_nHP = value;
				}
			}

			public int ATK {
				get {
					return m_nATK;
				}
				set {
					m_nATK = value;
				}
			}

			/*
			 * 자동 구현 프로퍼티란?
			 * - 일반적인 프로퍼티는 데이터를 저장하기 위한 멤버 변수가 별도로
			 * 필요하지만 자동 구현 프로퍼티는 프로퍼티만으로 데이터를 저장하거나
			 * 읽어들이는 것이 가능하다는 차이점이 존재한다.
			 * 
			 * 자동 구현 프로퍼티는 C# 컴파일러가 내부적으로 데이터를 저장하기위한
			 * 멤버 변수를 자동으로 만들어주는 특징 존재한다.
			 * 
			 * 단, 자동 구현 프로퍼티는 Getter/Setter 동작을 컴파일러가 자동으로 
			 * 구현하기 때문에 해당 프로퍼티를 통해서는 원하는 동작을 별도로 구현하는 
			 * 것이 불가능하다는 단점이 존재한다.
			 */
			public int DEF { get; set; } = 0;

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("레벨 : {0}", m_nLV);
				Console.WriteLine("체력 : {0}", m_nHP);
				Console.WriteLine("공격력 : {0}", m_nATK);
				Console.WriteLine("방어력 : {0}", DEF);
			}
		}
#endif
	}
}
