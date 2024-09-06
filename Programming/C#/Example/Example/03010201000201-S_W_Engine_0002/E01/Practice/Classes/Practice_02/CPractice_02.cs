//#define P02_01
//#define P02_02
//#define P02_03
//#define P02_04
//#define P02_05
#define P02_06

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 연습 문제 2 - 1
 * - 2 ~ 9 사이 숫자를 입력 받은 후 해당 숫자에 해당하는 구구단 출력
 * - 단, 범위를 벗어나는 숫자를 입력했을 경우 예외 처리
 * 
 * Ex)
 * 2 ~ 9 범위 숫자 입력 : 2
 * =====> 2 단 <=====
 * 2 * 1 = 2
 * 2 * 2 = 4
 * 2 * 3 = 6
 * ... 이하 생략
 * 
 * 2 ~ 9 범위 숫자 입력 : 0
 * 2 ~ 9 범위 숫자를 입력해주세요.
 * 
 * 
 * 연습 문제 2 - 2
 * - 초기 금액을 입력 받은 후 해당 금액으로 구입 가능한 모든 경우의 수 출력
 * - 플스 (1000 원), Xbox (950 원), 스위치 (500 원)
 * 
 * Ex)
 * 금액 입력 : 2000
 * 플스 (2 대), Xbox (0 대), 스위치 (0 대)
 * 플스 (1 대), Xbox (1 대), 스위치 (0 대)
 * 
 * 
 * 연습 문제 2 - 3
 * - 1 ~ 100 범위에 해당하는 랜덤한 정답 숫자 하나를 선택
 * - 숫자를 입력 받아 정답과 비교해서 결과 출력
 * - 단, 정답을 맞추지 못했을 경우 정답을 유추 할 수 있도록 힌트 출력
 * 
 * Ex)
 * 정답 : 50
 * 
 * 1 ~ 100 범위 숫자 입력 : 45
 * 정답은 45 보다 큽니다.
 * 
 * 1 ~ 100 범위 숫자 입력 : 55
 * 정답은 55 보다 작습니다.
 * 
 * 1 ~ 100 범위 숫자 입력 : 50
 * 정답입니다.
 * 
 * 
 * 연습 문제 2 - 4
 * - 단어 맞추기 게임 제작
 * - 미리 정해져 있는 특정 단어 중 하나를 랜덤하게 선택
 * - 문자를 입력 받아 단어에 포함 되어있으면 매칭 된 문자를 공개
 * - 모든 문자를 맞추면 게임 종료
 * - 단, 대/소문자는 구별하지 않는다
 * 
 * Ex)
 * 정답 : Microsoft
 * 
 * _ _ _ _ _ _ _ _ _
 * 문자 입력 : m
 * 
 * M _ _ _ _ _ _ _ _
 * 문자 입력 : O
 * 
 * M _ _ _ o _ o _ _
 * ... 이하 생략
 * 
 * 
 * 연습 문제 2 - 5
 * - 0 ~ 9 범위 숫자 중 랜덤하게 5 개를 선택
 * - 0, 1, 2 숫자 중 하나를 입력 받아 숫자를 지정 방향으로 이동하는 프로그램 제작
 * 
 * Ex)
 * 3, 0, 1, 5, 4
 * 숫자 (0:종료, 1:왼쪽, 2:오른쪽) 입력 : 1
 * 
 * 0, 1, 5, 4, 3
 * 숫자 (0:종료, 1:왼쪽, 2:오른쪽) 입력 : 2
 * 
 * 3, 0, 1, 5, 4
 * 숫자 (0:종료, 1:왼쪽, 2:오른쪽) 입력 : 0
 * 
 * 프로그램을 종료했습니다.
 * 
 * 
 * 연습 문제 2 - 6
 * - 숫자 야구 게임 제작
 * - 1 ~ 9 범위 숫자 중 중복되지 않는 4 개 숫자 (정답) 선택
 * - 숫자 4 개를 입력 받은 후 정답과 비교해서 Strike 또는 Ball 여부를 판단
 * - 4 Strike 달성 되면 게임 종료
 * 
 * Ex)
 * 정답 : 4 3 6 9
 * 
 * 숫자 (4 개) 입력 : 3 4 6 9
 * 결과 : 2 Strike, 2 Ball
 * 
 * 숫자 (4 개) 입력 : 4 3 6 9
 * 결과 : 4 Strike, 0 Ball
 * 
 * 게임을 종료했습니다.
 */
namespace Example._03010201000201_S_W_Engine_0002.E01.Practice.Classes.Practice_02
{
	class CPractice_02
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if P02_01
			Console.Write("2 ~ 9 범위 숫자 입력 : ");
			int.TryParse(Console.ReadLine(), out int nVal);

			// 범위를 벗어났을 경우
			if(nVal < 2 || nVal > 9) {
				Console.WriteLine("2 ~ 9 범위 숫자를 입력해주세요.");
			} else {
				Console.WriteLine("=====> {0} 단 <=====", nVal);

				for(int i = 1; i < 10; ++i) {
					Console.WriteLine("{0} * {1} = {2}",
						nVal, i, nVal * i);
				}
			}
#elif P02_02
			const int nPS = 1000;
			const int nXbox = 950;
			const int nSwitch = 500;

			Console.Write("금액 입력 : ");
			int.TryParse(Console.ReadLine(), out int nPrice);

			for(int i = 0; i <= nPrice; i += nPS) {
				for(int j = 0; j <= nPrice; j += nXbox) {
					for(int k = 0; k <= nPrice; k += nSwitch) {
						int nTotalPrice = i + j + k;

						// 구입 가능 할 경우
						if(nTotalPrice > 0 && nTotalPrice == nPrice) {
							Console.WriteLine("플스 ({0} 대), Xbox ({1} 대), 스위치 ({2} 대)",
								i / nPS, j / nXbox, k / nSwitch);
						}
					}
				}
			}
#elif P02_03
			Random oRandom = new Random();

			int nVal = 0;
			int nAnswer = oRandom.Next(1, 101);

			Console.WriteLine("정답 : {0}\n", nAnswer);

			do {
				Console.Write("1 ~ 100 범위 숫자 입력 : ");
				int.TryParse(Console.ReadLine(), out nVal);

				// 정답 일 경우
				if(nVal == nAnswer) {
					Console.WriteLine("정답입니다.");
				} else {
					Console.WriteLine("정답은 {0} 보다 {1}",
						nVal, (nVal < nAnswer) ? "큽니다." : "작습니다.");
				}

				Console.WriteLine();
			} while(nVal != nAnswer);
#elif P02_04
			string[] oWords = new string[]
			{
				"Apple", "Google", "Samsung", "Microsoft"
			};

			Random oRandom = new Random();
			string oAnswer = oWords[oRandom.Next(0, oWords.Length)];

			Console.WriteLine("정답 : {0}\n", oAnswer);
			char[] oLetters = new char[oAnswer.Length];

			for(int i = 0; i < oAnswer.Length; ++i) {
				oLetters[i] = '_';
			}

			bool bIsAnswer = false;

			do {
				for(int i = 0; i < oLetters.Length; ++i) {
					Console.Write("{0} ", oLetters[i]);
				}

				Console.Write("\n문자 입력 : ");
				char.TryParse(Console.ReadLine(), out char chLetter);

				for(int i = 0; i < oAnswer.Length; ++i) {
					// 문자가 존재 할 경우
					if(Char.ToUpper(chLetter) == Char.ToUpper(oAnswer[i])) {
						oLetters[i] = oAnswer[i];
					}
				}

				bIsAnswer = true;

				for(int i = 0; i < oLetters.Length; ++i) {
					// 비활성 문자가 존재 할 경우
					if(oLetters[i] == '_') {
						bIsAnswer = false;
					}
				}

				Console.WriteLine();
			} while(!bIsAnswer);

			for(int i = 0; i < oLetters.Length; ++i) {
				Console.Write("{0} ", oLetters[i]);
			}

			Console.WriteLine();
#elif P02_05
			Random oRandom = new Random();
			List<int> oValList = new List<int>();

			for(int i = 0; i < 5; ++i) {
				oValList.Add(oRandom.Next(0, 10));
			}

			int nInput = 0;

			const int nExit = 0;
			const int nLeft = 1;
			const int nRight = 2;

			do {
				for(int i = 0; i < oValList.Count; ++i) {
					Console.Write("{0}, ", oValList[i]);
				}

				Console.Write("\n숫자 입력 : ");
				int.TryParse(Console.ReadLine(), out nInput);

				switch(nInput) {
					case nLeft: {
						int nFirst = oValList[0];

						for(int i = 0; i < oValList.Count - 1; ++i) {
							oValList[i] = oValList[i + 1];
						}

						oValList[oValList.Count - 1] = nFirst;
						break;
					}
					case nRight: {
						int nLast = oValList[oValList.Count - 1];

						for(int i = oValList.Count - 1; i > 0; --i) {
							oValList[i] = oValList[i - 1];
						}

						oValList[0] = nLast;
						break;
					}
				}

				Console.WriteLine();
			} while(nInput != nExit);
#elif P02_06
			Random oRandom = new Random();
			List<int> oAnswer = new List<int>();

			while(oAnswer.Count < 4)
			{
				int nVal = oRandom.Next(1, 10);

				// 중복 된 값이 없을 경우
				if(!oAnswer.Contains(nVal))
				{
					oAnswer.Add(nVal);
				}
			}

			Console.Write("정답 : ");

			for(int i = 0; i < oAnswer.Count; ++i)
			{
				Console.Write("{0}, ", oAnswer[i]);
			}

			Console.WriteLine("\n");

			int nNumBalls = 0;
			int nNumStrikes = 0;

			do
			{
				Console.Write("숫자 (4 개) 입력 : ");
				string[] oTokens = Console.ReadLine().Split();

				nNumBalls = 0;
				nNumStrikes = 0;

				for(int i = 0; i < oTokens.Length; ++i)
				{
					int.TryParse(oTokens[i], out int nNum);

					// 숫자가 정답에 포함 되어있을 경우
					if(oAnswer.Contains(nNum))
					{
						int nIdx = oAnswer.IndexOf(nNum);

						nNumBalls += (i != nIdx) ? 1 : 0;
						nNumStrikes += (i == nIdx) ? 1 : 0;
					}
				}

				Console.WriteLine("결과 : {0} Strike, {1} Ball\n",
					nNumStrikes, nNumBalls);
			} while(nNumStrikes < oAnswer.Count);
#endif
		}
	}
}
