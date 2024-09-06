#define E04_IF_ELSE
#define E04_SWITCH_CASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 조건문이란?
 * - 특정 조건에 따라 프로그램의 흐름을 변경함으로써 다양한 결과를 만들어낼
 * 수 있는 기능을 의미한다. (즉, 조건문은 프로그램의 흐름을 제어 할 수 있는
 * 기능이라는 것을 알 수 있다.)
 * 
 * C# 조건문 종류
 * - if ~ else
 * - switch ~ case
 * 
 * Ex)
 * if(조건문 A)
 * {
 *     // 조건문 A 를 만족했을 때 실행 할 명령문
 * }
 * else if(조건문 B)
 * {
 *     // 조건문 B 를 만족했을 때 실행 할 명령문
 * }
 * else
 * {
 *     // 조건문 A 와 B 를 모두 만족하지 않았을 경우 실행 할 명령문
 * }
 * 
 * switch(데이터)
 * {
 *     case 조건 A:
 *         // 조건 A 를 만족했을 때 실행 할 명령문
 *         break;
 *         
 *     case 조건 B:
 *         // 조건 A 를 만족했을 때 실행 할 명령문
 *         break;
 *         
 *     default:
 *         // 조건 A 와 B 를 모두 만족하지 않았을 경우 실행 할 명령문
 *         break;
 * }
 * 
 * if ~ else vs switch ~ case
 * - if ~ else 조건문은 동등 비교를 포함한 복잡한 조건문을 작성 할 수 있기
 * 때문에 프로그램에서 사용되는 여러 데이터를 비교하는 연산에 활용하는 것이
 * 가능하다.
 * 
 * 반면, switch ~ case 조건문은 동등 비교만을 지원하기 때문에 범용적으로
 * 사용되는 if ~ else 에 비해 사용 할 수 있는 경우가 한정이 되는 단점이 있지만
 * switch ~ case 조건문은 내부적으로 기계어로 변환 될 때 명령문을 좀 더 빠르게
 * 동작 시킬 수 있는 점프 테이블을 만들기 위한 조건이 if ~ else 조건문 보다
 * 단순하기 때문에 컴파일러에 의해서 좀 더 빠른 명령문을 작성하는 것이 가능하다.
 * 
 * 따라서, 두 조건문을 모두 사용 할 수 있는 경우라면 switch ~ case 조건문을
 * 사용하는 것이 좀 더 빠르게 동작하는 명령문을 작성 할 수 있다.
 */
namespace Example._03010201000201_S_W_Engine_0002.E01.Example.Classes.Example_04
{
	class CE01Example_04
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E04_IF_ELSE
			Console.Write("점수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nScore);

			// F 학점 일 경우
			if(nScore < 60)
			{
				Console.WriteLine("F 학점입니다.");
			}
			else
			{
				// A 학점 일 경우
				if(nScore >= 90)
				{
					Console.WriteLine("A 학점입니다.");
				}
				// B 학점 일 경우
				else if(nScore >= 80)
				{
					Console.WriteLine("B 학점입니다.");
				}
				// C 학점 일 경우
				else if(nScore >= 70)
				{
					Console.WriteLine("C 학점입니다.");
				}
				// D 학점 일 경우
				else
				{
					Console.WriteLine("D 학점입니다.");
				}
			}
#elif E04_SWITCH_CASE
			Console.Write("점수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nScore);

			switch(nScore / 10)
			{
				case 9:
				case 10:
					Console.WriteLine("A 학점입니다.");
					break;
				case 8:
					Console.WriteLine("B 학점입니다.");
					break;
				case 7:
					Console.WriteLine("C 학점입니다.");
					break;
				case 6:
					Console.WriteLine("D 학점입니다.");
					break;
				default:
					Console.WriteLine("F 학점입니다.");
					break;
			}
#endif
		}
	}
}
