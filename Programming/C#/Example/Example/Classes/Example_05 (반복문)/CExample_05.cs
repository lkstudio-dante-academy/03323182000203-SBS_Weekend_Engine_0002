#define E05_WHILE
#define E05_FOR
#define E05_DO_WHILE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 반복문이란?
 * - 특정 조건문을 만족 할 동안 명령문의 전체 또는 일부분을 반복적으로 실행 
 * 할 수 있는 기능을 의미한다. (즉, 반복문을 활용하면 특정 조건을 만족 할 
 * 동안 지속적으로 구동되는 프로그램을 제작하는 것이 가능하다.)
 * 
 * 따라서, 반복문을 활용 할 때는 의도치 않게 반복 조건이 만족 되지 않음으로서
 * 발생하는 무한 루프를 주의 할 필요가 있다. (즉, 무한 루프가 발생하면 반복문이
 * 영원히 실행되기 때문에 프로그램이 끝나지 않는 문제가 발생한다는 것을 알 수
 * 있다.)
 * 
 * C# 반복문 종류
 * - while
 * - for
 * - do ~ while
 */
namespace Example.Classes.Example_05
{
	class CExample_05
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E05_WHILE
			Console.Write("반복 횟수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nTimes);

			int i = 0;

			while(i < nTimes)
			{
				// 홀수 일 경우
				if((i + 1) % 2 != 0)
				{
					i += 1;

					/*
					 * break 키워드란?
					 * - 반복문과 switch ~ case 조건문 내부에서 사용 가능하며 해당
					 * 키워드는 프로그램의 흐름을 반복문 또는 조건문 밖으로 이동시키는
					 * 역할을 수행한다.
					 * 
					 * 따라서, 반복문 내부에서 해당 키워드를 사용하면 즉시 반복문을
					 * 종료시키는 것이 가능하다는 것을 알 수 있다.
					 * 
					 * continue 키워드란?
					 * - 프로그램의 흐름을 제어하는 점프문에 해당하는 키워드 중
					 * 하나로서 반복문 내부에서만 사용가능하며 해당 키워드의
					 * 역할을 현재 흐름을 생략하고 다음 흐름으로 이동 시키는
					 * 것이다.
					 * 
					 * 따라서, while 반복문 내부에 해당 키워드를 사용했을 경우
					 * 원치 않는 무한 루프가 발생 할 수 있기 때문에 해당 키워드를
					 * 사용하기 전에 항상 반복을 끝내기 위한 명령문이 실행 되었는지
					 * 주시 할 필요가 있다.
					 * 
					 * break 와 continue 키워드는 가장 가까운 제어문에만 영향을
					 * 미치기 때문에 중첩으로 동작하는 반복문을 종료하고 싶은
					 * 경우에는 break 키워드와 더불에 외부에 있는 반복문 또한
					 * 종료하기 위한 구조로 반복문을 구성 할 필요가 있다.
					 * 
					 * 만약, 중첩으로 된 반복문을 끝내기 위한 명령문을 간단한 구조로
					 * 작성하고 싶다면 goto 점프문을 활용하면 된다.
					 * 
					 * 단, goto 점프문은 논란의 여지가 굉장히 강한 키워드이기 때문에
					 * 해당 키워드는 반드시 적절한 위치에만 사용 할 필요가 있다.
					 */
					continue;
				}

				Console.Write("{0}, ", i + 1);
				i += 1;
			}

			Console.WriteLine();
#elif E05_FOR
			Console.Write("반복 횟수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nTimes);

			for(int i = 0; i < nTimes; ++i)
			{
				// 홀수 일 경우
				if((i + 1) % 2 != 0)
				{
					/*
					 * for 반복문 내부에서 continue 키워드를 명시 할 경우
					 * while 반복문과 달리 조건을 끝내기 위한 반복절이 항상
					 * 실행 되기 때문에 while 반복문에 비해 좀 더 안전하게
					 * 명령문을 작성하는 것이 가능하다. (즉, for 반복문은
					 * while 반복문에 비해 의도치 않은 무한 루프에 빠지는
					 * 현상을 줄이는 것이 가능하다.)
					 */
					continue;
				}

				Console.Write("{0}, ", i + 1);
			}

			Console.WriteLine("\n\n=====> 구구단 <=====");

			for(int i = 2; i < 10; ++i)
			{
				Console.WriteLine("----- {0} 단", i);

				for(int j = 1; j < 10; ++j)
				{
					Console.WriteLine("{0} * {1} = {2}",
						i, j, i * j);
				}

				Console.WriteLine();
			}
#elif E05_DO_WHILE
			/*
			 * 사전 판단 반복문 vs 사후 판단 반복문
			 * - 사전 판단 반복문은 반복 여부를 반복 할 명령문이 실행 전에
			 * 먼저 검사하기 때문에 처음부터 조건이 거짓이라면 반복 할 명령문이
			 * 실행 되지 않는다는 것을 알 수 있다.
			 * 
			 * 반면, 사후 판단 반복문은 반복 여부를 반복 할 명령문을 실행 한 후
			 * 검사하기 때문에 처음부터 조건이 거짓이라 하더라도 반드시 1 번 이상
			 * 반복 할 명령문이 실행 된다는 차이점이 존재한다.
			 */
			while(false)
			{
				Console.WriteLine("While 반복문");
			}

			do
			{
				Console.WriteLine("Do ~ While 반복문");
			} while(false);
#endif
		}
	}
}
