#define E02_VAL_VAR
#define E02_REF_VAR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 자료형이란?
 * - 컴퓨터가 특정 데이터를 해석하는 방법을 의미한다. (즉, 동일한 형태의
 * 데이터라고 하더라도 자료형에 따라 처리 되는 방법이 달라 질 수 있다는 것을
 * 의미한다.)
 * 
 * 또한, 자료형은 데이터가 표현 할 수 있는 범위를 제한하는 역할도 수행한다.
 * (즉, 자료형에는 여러 종류가 존재하며 자료형에 따라 데이터를 해석하는 방법과
 * 표현 범위가 달라 진다는 것을 알 수 있다.)
 * 
 * C# 자료형 종류
 * // 정수
 * - byte : 1 byte
 * - sbyte : 1 byte
 * - short : 2 byte
 * - ushort : 2 byte
 * - int : 4 byte
 * - uint : 4 byte
 * - long : 8 byte
 * - ulong : 8 byte
 * 
 * // 실수
 * - float : 4 byte
 * - double : 8 byte
 * - decimal : 16 byte
 * 
 * // 문자
 * - char : 2 byte
 * - string
 * 
 * // 기타
 * - bool : 1 byte
 * - object
 * - 사용자 정의 자료형 (class, struct, record 등등...)
 * 
 * C# 자료형 형식 종류
 * - 값 형식
 * - 참조 형식
 * 
 * 값 형식 자료형이란?
 * - 데이터를 직접적으로 다루는 자료형을 의미한다. (즉, 값 형식 자료형은 실제
 * 데이터를 제어하기 때문에 해당 형식으로 선언 된 변수를 다른 변수에 할당하면
 * 원본과 전혀 상관 없는 사본 데이터가 만들어진다는 특징이 존재한다.)
 * 
 * 또한, 값 형식 자료형은 시스템에 의해서 메모리 관리가 이루어진다.
 * 
 * 참조 형식 자료형이란?
 * - 데이터를 지니고 있는 특정 대상의 참조 값 (메모리 주소) 를 다루는 자료형을
 * 의미한다. (즉, 참조 형식 자료형은 데이터 자체를 다루지 않고 참조 값만을 
 * 다루기 때문에 해당 형식으로 선언 된 변수를 다른 변수에 할당하면 얕은 복사가
 * 이루어진다는 특징이 존재한다.)
 * 
 * 또한, 참조 형식 자료형은 시스템에 의해서 메모리가 관리되는 값 형식과 달리
 * 가비지 컬렉션에 의해서 관리되는 특징이 존재한다. (즉, C# 은 가비지 컬렉션에
 * 의해서 동적 할당 된 메모리가 관리되는 관리형 프로그래밍 언어라는 것을 알 수
 * 있다.)
 */
namespace Example._03020203000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_02
{
	class CE01Example_02
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E02_VAL_VAR
			/*
			 * 변수란?
			 * - 프로그램 실행 중에 필요에 따라 데이터를 저장하거나 읽어들일 수
			 * 있는 공간을 의미한다. (즉, 변수를 활용하면 특정 데이터를 보관 및
			 * 필요에 따라 재사용하는 것이 가능하다는 것을 알 수 있다.)
			 * 
			 * C# 변수 선언 방법
			 * - 자료형 + 변수 이름
			 * 
			 * C# 이름 작성 규칙
			 * - C# 은 다국어를 지원하기 때문에 변수 및 메서드 등의 이름을 명시
			 * 할 때 영어 뿐만 아니라 한국어 등 다양한 언어를 통해서 이름을 명시
			 * 하는 것이 가능하다.
			 * 
			 * 단, 프로그래밍 언어는 전통적으로 영어만을 사용하던 관례가 존재하기
			 * 때문에 변수 이름 등을 명시 할 때도 가능하면 영어를 활용하는 것을
			 * 추천한다.
			 * 
			 * 또한, 영어 (알파벳) 뿐만 아니라 _ (언더 스코어) 와 숫자를 사용하는
			 * 것도 가능하지만 첫번째 문자에는 숫자를 사용하는 것이 불가능하다.
			 * 
			 * Ex)
			 * int nVal01;      <- O
			 * int 01nVal;      <- X
			 * 
			 * 위의 경우 두번째 변수는 첫 문자가 숫자로 시작하기 때문에 컴파일 에러가
			 * 발생하는 것을 알 수 있다.
			 * 
			 * int nVal;
			 * int nval;
			 * 
			 * 위의 경우 단어는 동일하지만 대/소문자가 서로 구분되기 때문에 해당 변수는
			 * 서로 다른 변수로 인식 된다는 것을 알 수 있다.
			 */
			byte nByte01 = byte.MaxValue;
			sbyte nByte02 = sbyte.MaxValue;

			short nShort01 = short.MaxValue;
			ushort nShort02 = ushort.MaxValue;

			int nInt01 = int.MaxValue;
			uint nInt02 = uint.MaxValue;

			long nLong01 = long.MaxValue;
			ulong nLong02 = ulong.MaxValue;

			Console.WriteLine("=====> 정수 <=====");
			Console.WriteLine("byte : {0}, {1}", nByte01, nByte02);
			Console.WriteLine("short : {0}, {1}", nShort01, nShort02);
			Console.WriteLine("int : {0}, {1}", nInt01, nInt02);
			Console.WriteLine("long : {0}, {1}", nLong01, nLong02);

			float fFloat = float.MaxValue;
			double dblDouble = double.MaxValue;
			decimal dmDecimal = decimal.MaxValue;

			Console.WriteLine("=====> 실수 <=====");
			Console.WriteLine("float : {0}", fFloat);
			Console.WriteLine("double : {0}", dblDouble);
			Console.WriteLine("decimal : {0}", dmDecimal);

			char chLetter01 = '한';
			char chLetter02 = '글';

			Console.WriteLine("=====> 문자 <=====");
			Console.WriteLine("char : {0}, {1}", chLetter01, chLetter02);
#elif E02_REF_VAR
			/*
			 * C# 의 모든 자료형은 직/간접적으로 Object 자료형을 상속하기 
			 * 때문에 Object 자료형을 사용하면 C# 이 제공하는 모든 자료형의
			 * 데이터를 보관 및 제어하는 것이 가능하다.
			 * 
			 * 단, Object 자료형은 참조 형식 자료형에 해당하기 때문에 해당
			 * 자료형으로 선언 된 변수에 값 형식 자료형 데이터를 할당 할 경우
			 * 내부적으로 Boxing 이 발생하기 때문에 이는 곧 가비지 컬렉션을
			 * 유발 시킨다는 것을 알 수 있다. (즉, Object 자료형을 남발하면
			 * 프로그램의 성능을 저하 시키는 원인이 될 수 있다.)
			 */
			object oObj = 10;
			string oStr = "Hello, World!";

			Console.WriteLine("=====> 참조 <=====");
			Console.WriteLine("object : {0}", oObj);
			Console.WriteLine("string : {0}", oStr);
#endif
		}
	}
}
