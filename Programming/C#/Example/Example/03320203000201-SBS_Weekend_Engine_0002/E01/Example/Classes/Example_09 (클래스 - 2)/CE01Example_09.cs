//#define E09_CLASS_01
//#define E09_CLASS_02
#define E09_CLASS_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03320203000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_09
{
	class CE01Example_09
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E09_CLASS_01
			CBase oBase = new CBase(10, 3.14f);
			CDerived oDerived = new CDerived(10, 3.14f, "Hello, World!");

			Console.WriteLine("=====> Base <=====");
			oBase.ShowInfo();

			Console.WriteLine("\n=====> Derived <=====");
			oDerived.ShowInfo();
#elif E09_CLASS_02
			CDerived oDerived = new CDerived(10, 3.14f, "Hello, World!");
			
			CBase oBase01 = oDerived;
			CBase oBase02 = new CBase(10, 3.14f);

			Console.WriteLine("=====> Base <=====");
			oBase01.ShowInfo();

			Console.WriteLine("\n=====> Derived <=====");
			oDerived.ShowInfo();

			/*
			 * 업 캐스팅이란?
			 * - 자식 클래스 자료형을 부모 클래스 자료형으로 변환하는 구문을 의미한다.
			 * (즉, 업 캐스팅은 안전성을 보장 할 수 있다는 것을 알 수 있다.)
			 * 
			 * 다운 캐스팅이란?
			 * - 부모 클래스 자료형을 자식 클래스 자료형으로 변환하는 구문을 의미한다.
			 * (즉, 다운 캐스팅은 업 캐스팅과 달리 안전성을 보장 할 수 없다는 것 을
			 * 알 수 있다.)
			 * 
			 * is 키워드란?
			 * - 다운 캐스팅 가능 여부를 검사하기 위한 역할을 수행하는 키워드를
			 * 의미한다. (즉, 해당 키워드를 활용하면 부모 클래스 자료형을 자식
			 * 클래스 자료형으로 변환하는 다운 캐스팅을 좀 더 안전하게 처리하는
			 * 것이 가능하다.)
			 * 
			 * as 키워드란?
			 * - is 키워드와 마찬가지로 다운 캐스팅을 처리해주는 키워드를 의미한다.
			 * is 키워드는 다운 캐스팅의 결과를 참 or 거짓으로 알려주는 반면 as 키워드는 
			 * 다운 캐스팅이 가능하다면 자식 클래스 자료형으로 형 변환을 해주고 
			 * 불가능하다면 null 을 반환하는 차이점이 존재한다.
			 * 
			 * 따라서, as 키워드는 값 형식 자료형에는 사용하는 것이 불가능하다.
			 */
			Console.WriteLine("\n=====> 다운 캐스팅 is 결과 <=====");
			Console.WriteLine("Base 1 -> Derived : {0}", oBase01 is CDerived);
			Console.WriteLine("Base 2 -> Derived : {0}", oBase02 is CDerived);

			Console.WriteLine("\n=====> 다운 캐스팅 as 결과 <=====");
			Console.WriteLine("Base 1 -> Derived : {0}", oBase01 as CDerived);
			Console.WriteLine("Base 2 -> Derived : {0}", oBase02 as CDerived);
#elif E09_CLASS_03
			CData oData01 = CData.GetInst();
			oData01.m_nInstVal = 10;

			CData oData02 = CData.GetInst();
			oData02.m_nInstVal = 20;

			CData.m_nVal = 10;
			CData.m_fVal = 3.14f;

			Console.WriteLine("=====> 데이터 1 <=====");
			oData01.ShowInstInfo();

			Console.WriteLine("\n=====> 데이터 2 <=====");
			oData02.ShowInstInfo();
#endif
		}

#if E09_CLASS_01
		/*
		 * 객체 지향 프로그래밍 3 대 요소
		 * - 캡슐화 (+ or 정보 은닉)
		 * - 상속
		 * - 다형성
		 * 
		 * 상속이란?
		 * - 특정 클래스가 지니고 있는 멤버 (변수, 메서드 등등...) 를 다른 클래스에게
		 * 공유해주는 기능을 의미한다.
		 * 
		 * 상속이란 클래스 간에 부모/자식 관계를 형성하기 위한 방법이며 자식 클래스는
		 * 부모 클래스가 지니고 있는 멤버를 사용 할 수 있다는 특징이 존재한다.
		 * (즉, 상속을 활용하면 클래스마다 중복적으로 명시되는 멤버를 최소화시키는 것이
		 * 가능하다.)
		 * 
		 * C# 클래스 상속 방법
		 * - 클래스 이름 + 부모 클래스 이름
		 * 
		 * Ex)
		 * class CBase {
		 *      // Do Something
		 * }
		 * 
		 * class CDerived : CBase {
		 *      // Do Something
		 * }
		 * 
		 * 단, 무분별한 상속은 오히려 클래스의 구조를 복잡하게 만들기 때문에 상속을
		 * 명시 할 때는 반드시 is a 의 관계가 성립되는 먼저 판단 할 필요가 있다.
		 * (즉, is a 의 관계가 성립되지 않는다면 잘못된 상속이라는 것을 알 수 있다.)
		 */
		/** 부모 클래스 */
		public class CBase {
			private int m_nVal = 0;
			protected float m_fVal = 0.0f;

			/** 기본 생성자 */
			public CBase() {
				// Do Something
			}

			/** 생성자 */
			public CBase(int a_nVal, float a_fVal) {
				m_nVal = a_nVal;
				m_fVal = a_fVal;
			}

			/** 정보를 출력한다 */
			public void ShowInfo() {
				Console.WriteLine("정수 : {0}", m_nVal);
				Console.WriteLine("실수 : {0}", m_fVal);
			}
		}

		/** 자식 클래스 */
		public class CDerived : CBase {
			private string m_oStr = string.Empty;

			/*
			 * 클래스가 특정 클래스를 상속했을 경우 해당 클래스는 반드시 부모 클래스의
			 * 생성자를 호출 해 줄 필요가 있다. 만약, 부모 클래스의 생성자를 호출하지
			 * 않았을 경우 C# 컴파일러에 의해서 자동으로 부모 클래스의 기본 생성자를
			 * 호출하는 명령문을 추가 된다는 특징이 존재한다.
			 * 
			 * 따라서, 부모 클래스에 기본 생성자가 존재하지 않을 경우에는 반드시 자식
			 * 클래스의 생성자에서 부모 클래스의 생성자를 명시적으로 호출해줘야한다.
			 * 
			 * 이는 상속 관계에 있는 클래스의 생성자 호출 순서는 반드시 부모 클래스에서
			 * 자식 클래스 순으로 호출 되어야 하기 때문이다.
			 */
			/** 생성자 */
			public CDerived(int a_nVal, float a_fVal, string a_oStr) : base(a_nVal, a_fVal) {
				m_oStr = a_oStr;
			}

			/*
			 * 부모 클래스와 자식 클래스에 동일한 이름을 지니는 멤버가 존재 할 경우
			 * 컴파일 경고가 발생한다. (즉, 해당 상황이 의도된 것인지 아닌지
			 * 컴파일러는 알 수 없기 때문에 경고를 통해 사용자에게 해당 명령문을
			 * 확인해 보라는 의미라는 것을 알 수 있다.)
			 * 
			 * 이때, 해당 상황이 의도한 상황이라고 한다면 컴파일 경고를 없기 위해서
			 * new 키워드가 활용된다. (즉, new 키워드는 객체를 생성하는 용도 뿐만
			 * 아니라 다양한 상황에서 사용 된다는 것을 알 수 있다.)
			 * 
			 * C# 클래스 관련 키워드
			 * - base
			 * - this
			 * 
			 * base 키워드는 부모 클래스를 지칭하는 역할을 수행하며 this 키워드는
			 * 자기 자신을 지칭하는 역할을 수행한다. 따라서, 자식 클래스에 부모
			 * 클래스에 존재하는 멤버와 동일한 멤버가 존재 할 경우 자식 클래스의
			 * 멤버가 우선순위가 더 높기 때문에 부모 클래스가 지니고 있는 멤버를 
			 * 명시적으로 지칭하기 위해서는 반드시 base 키워드를 명시해 줄 필요가
			 * 있다.
			 */
			/** 정보를 출력한다 */
			public new void ShowInfo() {
				base.ShowInfo();
				Console.WriteLine("문자열 : {0}", m_oStr);
			}
		}
#elif E09_CLASS_02
		/*
		 * 다형성이란?
		 * - 특정 대상 (사물이) 을 바라보는 시선에 따라 다양한 형태를 지니고 있는 것을
		 * 의미한다.
		 * 
		 * 객체 지향 프로그래밍에서는 클래스의 상속을 통해 다형성을 흉내는 것이 가능하며
		 * 이는 곧 하나의 객체의 여러 자료형으로 인지 될 수 있다는 것을 의미한다.
		 * (즉, 자식 클래스를 통해 생성 된 객체는 부모 클래스 자료형으로 참조하는 것이
		 * 가능하다는 것을 알 수 있다.)
		 */
		/** 부모 클래스 */
		public class CBase {
			private int m_nVal = 0;
			protected float m_fVal = 0.0f;

			/** 기본 생성자 */
			public CBase() {
				// Do Something
			}

			/** 생성자 */
			public CBase(int a_nVal, float a_fVal) {
				m_nVal = a_nVal;
				m_fVal = a_fVal;
			}

			/*
			 * 가상 메서드란?
			 * - 부모 클래스와 자식 클래스에 동일한 메서드가 존재 할 경우 부모 클래스에
			 * 있는 메서드 대신에 자식 클래스의 존재하는 메서드를 호출 할 수 있는 기능을
			 * 의미한다. (즉, 가상 메서드를 활용하면 자식 클래스 객체를 부모 클래스
			 * 자료형으로 참조하고 있는 상황에서 자식 클래스에 존재하는 메서드를 호출하는
			 * 것이 가능하다.)
			 * 
			 * 따라서, 자식 클래스에서는 부모 클래스에 존재하는 메서드 대신에 자식 클래스에
			 * 존재하는 메서드를 호출하기 위해서 부모 클래스에 존재하는 가상 메서드를
			 * override 해 줄 필요가 있다. (즉, override 된 메서드는 부모 클래스 메서드
			 * 대신에 자식 클래스 메서드가 호출 된다는 것을 알 수 있다.)
			 */
			/** 정보를 출력한다 */
			public virtual void ShowInfo() {
				Console.WriteLine("정수 : {0}", m_nVal);
				Console.WriteLine("실수 : {0}", m_fVal);
			}
		}

		/** 자식 클래스 */
		public class CDerived : CBase {
			private string m_oStr = string.Empty;

			/** 생성자 */
			public CDerived(int a_nVal, float a_fVal, string a_oStr) : base(a_nVal, a_fVal) {
				m_oStr = a_oStr;
			}

			/** 정보를 출력한다 */
			public override void ShowInfo() {
				base.ShowInfo();
				Console.WriteLine("문자열 : {0}", m_oStr);
			}
		}
#elif E09_CLASS_03
		/*
		 * 정적 멤버란?
		 * - 일반적인 멤버와 달리 객체에 종속되는 것이 아니라 클래스 자체에 종속되는
		 * 멤버를 의미하다. 따라서, 정적 멤버에 접근하기 위해서는 객체가 필요하지 않다는
		 * 특징이 존재한다. (즉, 객체를 통하지 않고도 멤버에 접근하는 것이 가능하다.)
		 * 
		 * 따라서, 정적 멤버는 모든 객체가 공유는 멤버라는 것을 알 수 있다.
		 */
		/** 데이터 */
		public class CData
		{
			public int m_nInstVal = 0;
			public static int m_nVal = 0;
			public static float m_fVal = 0.0f;

			private static CData m_oInst = null;

			/** 생성자 */
			private CData()
			{
				// Do Something
			}

			/** 인스턴스를 반환한다 */
			public static CData GetInst()
			{
				// 생성 된  인스턴스가 없을 경우
				if(m_oInst == null)
				{
					m_oInst = new CData();
				}

				return m_oInst;
			}

			/** 정보를 출력한다 */
			public void ShowInstInfo()
			{
				Console.WriteLine("정수 : {0}", m_nVal);
				Console.WriteLine("실수 : {0}", m_fVal);
				Console.WriteLine("멤버 : {0}", m_nInstVal);
			}

			/** 정보를 출력한다 */
			public static void ShowInfo()
			{
				Console.WriteLine("정수 : {0}", m_nVal);
				Console.WriteLine("실수 : {0}", m_fVal);

				/*
				 * 정적 메서드에서는 멤버 변수에 접근하는 것이 불가능하다.
				 * 이는 멤버 변수에 접근하기 위해서는 반드시 객체가 필요한데 정적 메서드에는
				 * 특정 객체를 지정하기 위한 정보가 없기 때문이다.
				 */
				//Console.WriteLine("멤버 : {0}", m_nInstVal);
			}
		}
#endif
	}
}
