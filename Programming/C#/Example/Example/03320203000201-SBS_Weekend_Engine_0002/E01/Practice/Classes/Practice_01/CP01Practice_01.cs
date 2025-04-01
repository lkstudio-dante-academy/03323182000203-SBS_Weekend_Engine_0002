#define P01_01
#define P01_02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 연습 문제 1 - 1
 * - 세부 학점 계산하기
 * - 점수를 입력 받은 후 점수에 해당하는 학점 출력
 * 
 * 세부 학점 범위
 * - 0 ~ 3 : -
 * - 4 ~ 6 : 0
 * - 7 ~ 9 : +
 * 
 * Ex)
 * 점수 입력 : 94
 * A0 학점입니다.
 * 
 * 
 * 연습 문제 1 - 2
 * - 바위, 가위, 보 결과 출력하기
 * - 사용자로부터 바위, 가위, 보 중 하나를 입력
 * - 컴퓨터는 랜덤하게 선택
 * - 사용자와 컴퓨터의 선택을 비교해서 결과 출력
 * 
 * Ex)
 * 바위 (0), 가위 (1), 보 (2) 선택 : 2
 * 결과 : 졌습니다. (나 - 보, 컴퓨터 - 가위)
 */
namespace Example._03320203000201_SBS_Weekend_Engine_0002.E01.Practice.Classes.Practice_01
{
	class CP01Practice_01
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if P01_01
			Console.Write("점수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nScore);

			// F 학점 일 경우
			if(nScore < 60)
			{
				Console.Write("F");
			}
			else
			{
				// A 학점 일 경우
				if(nScore >= 90)
				{
					Console.Write("A");
				}
				// B 학점 일 경우
				else if(nScore >= 80)
				{
					Console.Write("B");
				}
				// C 학점 일 경우
				else if(nScore >= 70)
				{
					Console.Write("C");
				}
				// D 학점 일 경우
				else
				{
					Console.Write("D");
				}

				// + 학점 일 경우
				if(nScore >= 100 || (nScore % 10) >= 7)
				{
					Console.Write("+");
				}
				else
				{
					Console.Write("{0}", (nScore % 10 <= 3) ? '-' : '0');
				}
			}

			Console.WriteLine(" 학점입니다.");
#elif P01_02
			Random oRandom = new Random();
			int nSelComputer = oRandom.Next(0, 3);

			Console.Write("바위 (0), 가위 (1), 보 (2) 선택 : ");
			int.TryParse(Console.ReadLine(), out int nSelUser);

			Console.WriteLine("컴퓨터 선택 : {0}\n", nSelComputer);

			// 비겼을 경우
			if(nSelUser == nSelComputer) {
				Console.WriteLine("결과 : 비겼습니다.");
			} else {
				bool bIsWin = (nSelUser + 1) % 3 == nSelComputer;
				Console.WriteLine("결과 : {0}", bIsWin ? "이겼습니다." : "졌습니다.");
			}
#endif
		}
	}
}
