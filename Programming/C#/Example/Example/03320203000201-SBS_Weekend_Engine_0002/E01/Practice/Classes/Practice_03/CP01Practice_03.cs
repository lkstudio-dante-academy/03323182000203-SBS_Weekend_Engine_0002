//#define P03_01
//#define P03_02
#define P03_03

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 연습 문제 3 - 1
 * - 바위, 가위, 보 게임 제작하기
 * - 사용자 선택은 콘솔로부터 입력, 컴퓨터는 랜덤하게 하나를 선택
 * - 결과를 판정 후 사용자가 이겼거나 비겼으면 게임 계속 진행
 * - 졌을 경우 전적 출력 후 프로그램 종료
 * 
 * Ex)
 * 바위 0, 가위 1, 보 2 선택 : 2
 * 결과 : 이겼습니다. (나 - 보, 컴퓨터 - 바위)
 * 
 * 바위 0, 가위 1, 보 2 선택 : 1
 * 결과 : 졌습니다. (나 - 가위, 컴퓨터 - 바위)
 * 
 * 전적 : 1 승 0 무 1 패
 * 프로그램을 종료했습니다.
 * 
 * 
 * 연습 문제 3 - 2
 * - 1 ~ 15 범위 중 랜덤하게 20 개 숫자를 지니는 배열 생성
 * - 배열의 특정 위치를 입력
 * - 입력한 위치를 포함한 주변 숫자 중 한자리 숫자를 모두 0 으로 치환
 * - 단, 한자리 수가 아닐 경우 치환 종료
 * 
 * Ex)
 * =====> 배열 요소 <=====
 * 1, 10, 5, 8, 9, 11, 14, 5, 2, 3
 * 
 * 시작 위치 입력 : 2
 * 
 * =====> 배열 요소 - 치환 후 <=====
 * 1, 10, 0, 0, 0, 11, 14, 5, 2, 3
 * 
 * 
 * 연습 문제 3 - 3
 * - 미로를 탈출하기 위한 경로 찾기
 * - 고정 된 맵을 생성 후 시작 위치부터 종료 위치까지 이동 할 수 있는 경로 탐색
 * - # 으로 지정 된 부분은 이동 불가
 * - 단, 탈출하기 위한 경로가 없을 경우 프로그램 종료
 * 
 * Ex)
 * =====> 경로 탐색 전 <=====
 * ##S##
 * #   #
 * # # #
 * #   #
 * ##E##
 * 
 * =====> 경로 탐색 후 <=====
 * ##*##
 * # **#
 * # #*#
 * # **#
 * ##*##
 */
namespace Example._03320203000201_SBS_Weekend_Engine_0002.E01.Practice.Classes.Practice_03
{
	class CP01Practice_03
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if P03_01
			int nWinCount = 0;
			int nDrawCount = 0;

			EResult eResult = EResult.NONE;

			do {
				Console.Write("바위 0, 가위 1, 보 2 선택 : ");
				int.TryParse(Console.ReadLine(), out int nUserSel);

				Random oRandom = new Random();

				ESel eUserSel = (ESel)nUserSel;
				ESel eComputerSel = (ESel)oRandom.Next(0, (int)ESel.MAX_VAL);

				eResult = GetResult(eUserSel, eComputerSel);

				nWinCount += (eResult == EResult.WIN) ? 1 : 0;
				nDrawCount += (eResult == EResult.DRAW) ? 1 : 0;

				string oUserSelStr = ToString(eUserSel);
				string oComputerSelStr = ToString(eComputerSel);
				string oResultStr = ToString(eResult);

				Console.WriteLine("결과 : {0} (나 - {1}, 컴퓨터 - {2})\n",
					oResultStr, oUserSelStr, oComputerSelStr);
			} while(eResult != EResult.LOSE);

			Console.WriteLine("전적 : {0} 승 {1} 무 1 패", nWinCount, nDrawCount);
			Console.WriteLine("프로그램을 종료했습니다.");
#elif P03_02
			List<int> oValList = new List<int>();
			SetupVals(oValList);

			Console.WriteLine("=====> 배열 요소 - 치환 전 <=====");
			PrintVals(oValList);

			Console.Write("\n위치 입력 : ");
			int.TryParse(Console.ReadLine(), out int nIdx);

			ReplaceVals(oValList, nIdx);

			Console.WriteLine("\n\n=====> 배열 요소 - 치환 후 <=====");
			PrintVals(oValList);
#elif P03_03
			char[,] oMaps = new char[5, 5]
			{
				{ '#', '#', 'S', '#', '#' },
				{ '#', ' ', ' ', ' ', '#' },
				{ '#', ' ', '#', ' ', '#' },
				{ '#', ' ', ' ', ' ', '#' },
				{ '#', '#', 'E', '#', '#' }
			};

			Console.WriteLine("=====> 경로 탐색 전 <=====");
			PrintMaps(oMaps);

			FindPath(oMaps, 0, 2);

			Console.WriteLine("\n=====> 경로 탐색 후 <=====");
			PrintMaps(oMaps);
#endif
		}

#if P03_01
		/** 선택 */
		public enum ESel
		{
			NONE = -1,
			ROCK,
			SCISSORS,
			PAPER,
			MAX_VAL
		}

		/** 결과 */
		public enum EResult
		{
			NONE = -1,
			WIN,
			LOSE,
			DRAW,
			MAX_VAL
		}

		/** 선택 -> 문자열로 변환한다 */
		public static string ToString(ESel a_eSel) {
			if(a_eSel == ESel.ROCK) {
				return "바위";
			}

			return (a_eSel == ESel.SCISSORS) ? "가위" : "보";
		}

		/** 결과 -> 문자열로 변환한다 */
		public static string ToString(EResult a_eResult) {
			if(a_eResult == EResult.WIN) {
				return "이겼습니다.";
			}

			return (a_eResult == EResult.DRAW) ? "비겼습니다." : "졌습니다.";
		}

		/** 결과를 반환한다 */
		public static EResult GetResult(ESel a_eUser, ESel a_eComputer) {
			var oResults = new EResult[(int)ESel.MAX_VAL, (int)ESel.MAX_VAL]
			{
				{ EResult.DRAW, EResult.WIN, EResult.LOSE },
				{ EResult.LOSE, EResult.DRAW, EResult.WIN },
				{ EResult.WIN, EResult.LOSE, EResult.DRAW }
			};

			return oResults[(int)a_eUser, (int)a_eComputer];
		}
#elif P03_02
		/** 값을 설정한다 */
		public static void SetupVals(List<int> a_oValList) {
			Random oRandom = new Random();

			for(int i = 0; i < 20; ++i) {
				a_oValList.Add(oRandom.Next(1, 16));
			}
		}

		/** 값을 출력한다 */
		public static void PrintVals(List<int> a_oValList) {
			for(int i = 0; i < a_oValList.Count; ++i) {
				Console.Write("{0}, ", a_oValList[i]);
			}

			Console.WriteLine();
		}

		/** 값을 치환한다 */
		public static void ReplaceVals(List<int> a_oValList, int a_nIdx) {
			bool bIsValid = a_nIdx >= 0 && a_nIdx < a_oValList.Count;

			// 값 치환이 불가능 할 경우
			if(!bIsValid || (a_oValList[a_nIdx] <= 0 || a_oValList[a_nIdx] >= 10)) {
				return;
			}

			a_oValList[a_nIdx] = 0;

			ReplaceVals(a_oValList, a_nIdx - 1);
			ReplaceVals(a_oValList, a_nIdx + 1);
		}
#elif P03_03
		/** 맵을 출력한다 */
		public static void PrintMaps(char[,] a_oMaps)
		{
			for(int i = 0; i < a_oMaps.GetLength(0); ++i)
			{
				for(int j = 0; j < a_oMaps.GetLength(1); ++j)
				{
					Console.Write("{0}", a_oMaps[i, j]);
				}

				Console.WriteLine();
			}
		}

		/** 경로를 탐색한다 */
		public static bool FindPath(char[,] a_oMaps, int a_nRow, int a_nCol)
		{
			bool bIsValid01 = a_nRow >= 0 && a_nRow < a_oMaps.GetLength(0);
			bool bIsValid02 = a_nCol >= 0 && a_nCol < a_oMaps.GetLength(1);

			// 탐색이 불가능 할 경우
			if(!bIsValid01 || !bIsValid02 ||
				a_oMaps[a_nRow, a_nCol] == '#' || a_oMaps[a_nRow, a_nCol] == '*')
			{
				return false;
			}

			char chPrevLetter = a_oMaps[a_nRow, a_nCol];
			a_oMaps[a_nRow, a_nCol] = '*';

			// 목적지에 도착했을 경우
			if(chPrevLetter == 'E')
			{
				return true;
			}

			List<(int, int)> oOffsetInfoList = new List<(int, int)>() {
				(0, -1), (0, 1), (-1, 0), (1, 0)
			};

			for(int i = 0; i < oOffsetInfoList.Count; ++i)
			{
				int nNextRow = a_nRow + oOffsetInfoList[i].Item1;
				int nNextCol = a_nCol + oOffsetInfoList[i].Item2;

				// 경로 탐색에 성공했을 경우
				if(FindPath(a_oMaps, nNextRow, nNextCol))
				{
					return true;
				}
			}

			a_oMaps[a_nRow, a_nCol] = chPrevLetter;
			return false;
		}
#endif
	}
}
