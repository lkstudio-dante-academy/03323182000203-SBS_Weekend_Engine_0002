#define E06_ARRAY
#define E06_LINEAR
#define E06_NON_LINEAR

#if E06_ARRAY
//#define E06_ARRAY_01
#define E06_ARRAY_02
#elif E06_LINEAR
//#define E06_LINEAR_01
#define E06_LINEAR_02
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 컬렉션이란?
 * - 1 개 이상의 데이터를 관리 및 제어 할 수 있는 기능을 의미한다.
 * (즉, 컬렉션을 활용하면 다수의 데이터를 좀 더 효율적으로 관리하는 것이
 * 가능하다는 것을 알 수 있다.)
 * 
 * C# 컬렉션 분류
 * - 선형 컬렉션 (배열, 리스트, 스택, 큐)
 * - 비선형 컬렉션 (딕셔너리, 셋)
 * 
 * 선형 컬렉션 vs 비선형 컬렉션
 * - 선형 컬렉션은 관리되는 데이터의 순서가 존재하기 때문에 특정 데이터를 
 * 가져오거나 특정 데이터를 변경하기 위해서는 특정 데이터의 순서 (번호) 에
 * 맞춰서 알고리즘을 구성 할 필요가 있다.
 * 
 * 반면, 비선형 컬렉션은 관리되는 데이터의 순서가 따로 존재하지 않기 때문에
 * 특정 데이터에 접근하기 위해서는 선형 컬렉션보다 내부적으로 복잡한 연산을 
 * 필요로하는 차이점 존재한다.
 * 
 * 단, 일반적으로 비선형 컬렉션은 선형 컬렉션에 비해 관리되는 데이터의 개수가
 * 많아질 수록 좋은 성능을 발휘하기 때문에 대량의 데이터를 관리 할 필요가
 * 있다면 비선형 컬렉션을 활용하는 것이 일반적이다.
 */
namespace Example._03010201000201_S_W_Engine_0002.E01.Example.Classes.Example_06
{
	class CExample_06
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
#if E06_ARRAY
#if E06_ARRAY_01
			/*
			 * 배열의 특정 위치에 데이터를 저장하거나 읽어들이기 위해서는
			 * [] (인덱스 연산자) 와 인덱스 번호를 활용하면 된다.
			 * 
			 * 인덱스 번호의 범위는 0 ~ 배열 길이 - 1 이며 해당 번호를 이용하면
			 * 배열의 특정 위치에 데이터를 저장하거나 읽어들이는 것이 가능하다.
			 * 
			 * 단, 잘못된 인덱스 번호를 명시 할 경우 내부적으로 인덱스 번호와
			 * 관련 된 예외가 발생하기 때문에 배열의 특정 데이터에 접근 할 때는
			 * 항상 올바른 인덱스 번호를 사용하고 있는지 주시 할 필요가 있다.
			 * 
			 * 또한, 특정 배열을 생성 후 해당 배열의 초기화 값을 따로 명시하지
			 * 않으면 내부적으로 자동으로 기본 값으로 초기화 되는 장점이 있다.
			 * (즉, default 키워드로 모든 데이터의 값이 지정 된다는 것을 알 수
			 * 있다.)
			 */
			int[] aoVals01 = new int[5];
			aoVals01[0] = 1;
			aoVals01[1] = 2;
			aoVals01[2] = 3;
			aoVals01[3] = 4;
			aoVals01[4] = 5;

			/*
			 * 배열은 생성과 동시에 특정 데이터를 설정 할 수 있는 초기화 기능을
			 * 지원하며 초기화 값을 명시하는 방법은 { } 기호를 통해서 명시하는
			 * 것이 가능하다.
			 * 
			 * 단, C# 배열 초기화는 다른 언어와 달리 조금 까다로운 면이 있기
			 * 때문에 특정 배열의 초기화 값을 명시 할 경우 해당 배열의 길이만큼
			 * 초기화 값을 지정하도록 강제가 되는 특징이 존재한다.
			 */
			int[] aoVals02 = new int[5]
			{
				1, 2, 3, 4, 5
			};

			/*
			 * 기본적으로 배열을 생성 할 때는 배열의 길이를 명시해줘야하지만 초기화
			 * 값을 이용하면 배열의 길이를 명시하는 것을 생략하는 것이 가능하다.
			 * 
			 * 단, 해당 경우 C# 컴파일러가 배열의 길이를 계산 할 수 있도록 반드시
			 * 초기화 값을 명시해줘야한다. (즉, C# 컴파일러는 배열의 길이를 생략했을
			 * 경우 명시 된 초기화 값의 개수를 기반으로 배열의 길이를 자동으로 설정
			 * 한다는 것을 알 수 있다.)
			 */
			int[] aoVals03 = new int[]
			{
				1, 2, 3
			};

			Console.WriteLine("=====> 1 차원 배열 <=====");

			for(int i = 0; i < aoVals01.Length; ++i)
			{
				Console.Write("{0}, ", aoVals01[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < aoVals02.Length; ++i)
			{
				Console.Write("{0}, ", aoVals02[i]);
			}

			Console.WriteLine();

			for(int i = 0; i < aoVals03.Length; ++i)
			{
				Console.Write("{0}, ", aoVals03[i]);
			}

			/*
			 * 배열은 필요에 따라 차원을 올리는 것이 가능하며 2 차원 이상의
			 * 배열을 다차원 배열이라고 지칭한다.
			 * 
			 * 다차원 배열은 하위 차원 배열을 요소로 하는 배열을 의미하며
			 * 다차원 배열에서 특정 위치에 데이터를 저장하거나 읽어들이기 위해서는
			 * 차원의 깊이만큼 인덱스 번호를 명시 할 필요가 있다. (즉, 2 차원 배열
			 * 일 경우 인덱스 번호를 2 번 명시해야 특정 데이터에 접근하는 것이
			 * 가능하다는 것을 알 수 있다.)
			 */
			int[,] aoMatrix01 = new int[2, 2];
			aoMatrix01[0, 0] = 1;
			aoMatrix01[0, 1] = 2;
			aoMatrix01[1, 0] = 3;
			aoMatrix01[1, 1] = 4;

			/*
			 * 다차원 배열 또한 생성과 동시에 초기화 값을 명시하는 것이 가능하며
			 * 이때, 특정 차원을 구분하기 위해서 반드시 { } 기호를 명시해 줄 필요가
			 * 있다. (즉, 차원을 명시적으로 지정하지 않으면 컴파일 에러가 발생한다는
			 * 것을 알 수 있다.)
			 */
			int[,] aoMatrix02 = new int[2, 2]
			{
				{ 1, 2 },
				{ 3, 4 }
			};

			int[,] aoMatrix03 = new int[,]
			{
				{ 1 },
				{ 3 }
			};

			Console.WriteLine("\n\n=====> 2 차원 배열 <=====");

			/*
			 * 2 차원 이상의 배열에 반복문을 구성하기 위해서는 해당 배열의
			 * 차원만큼 반복문 중첩 시키는게 일반적은 구조이다.
			 * 
			 * 이때, 배열의 특정 차원의 길이를 가져오기 위해서는 GetLength
			 * 메서드를 사용 할 필요가 있으며 해당 메서드에 0 이상의 값을
			 * 명시함으로서 특정 차원에 해당하는 배열의 길이를 가져오는 것이
			 * 가능하다. (즉, 차원의 번호가 0 에 가까울수록 상위 차원을 의미하며
			 * 반대 일 경우에는 하위 차원을 의미한다는 것을 알 수 있다.)
			 */
			for(int i = 0; i < aoMatrix01.GetLength(0); ++i)
			{
				for(int j = 0; j < aoMatrix01.GetLength(1); ++j)
				{
					Console.Write("{0}, ", aoMatrix01[i, j]);
				}

				Console.WriteLine();
			}

			Console.WriteLine();

			for(int i = 0; i < aoMatrix02.GetLength(0); ++i)
			{
				for(int j = 0; j < aoMatrix02.GetLength(1); ++j)
				{
					Console.Write("{0}, ", aoMatrix02[i, j]);
				}

				Console.WriteLine();
			}

			Console.WriteLine();

			for(int i = 0; i < aoMatrix03.GetLength(0); ++i)
			{
				for(int j = 0; j < aoMatrix03.GetLength(1); ++j)
				{
					Console.Write("{0}, ", aoMatrix03[i, j]);
				}

				Console.WriteLine();
			}

			Console.Write("\n개수 입력 : ");
			int.TryParse(Console.ReadLine(), out int nNum);

			int[] aoVals = new int[nNum];

			for(int i = 0; i < nNum; ++i)
			{
				Console.Write("{0} 번째 정수 입력 : ", i + 1);
				int.TryParse(Console.ReadLine(), out aoVals[i]);
			}

			int nSumVal = 0;

			for(int i = 0; i < nNum; ++i)
			{
				nSumVal += aoVals[i];
			}

			Console.WriteLine("\n합계 : {0}", nSumVal);
#elif E06_ARRAY_02
			int nVal = 0;
			int nNumVals = 0;

			int[] aoVals = new int[5];

			do
			{
				Console.Write("{0} 번째 정수 입력 : ", nNumVals + 1);
				int.TryParse(Console.ReadLine(), out nVal);

				// 입력을 중단했을 경우
				if(nVal <= 0)
				{
					continue;
				}

				// 배열에 공간이 부족 할 경우
				if(nNumVals >= aoVals.Length)
				{
					/*
					 * Array.Resize 메서드는 기존 배열의 길이를 변경하는
					 * 역할을 수행한다. (즉, 해당 메서드를 활용하면 배열의
					 * 길이를 늘리거나 줄이는 것이 가능하다는 것을 알 수
					 * 있다.)
					 */
					Array.Resize(ref aoVals, aoVals.Length * 2);
				}

				aoVals[nNumVals++] = nVal;
			} while(nVal > 0);

			int nSumVal = 0;

			for(int i = 0; i < nNumVals; ++i)
			{
				nSumVal += aoVals[i];
			}

			Console.WriteLine("\n합계 : {0}", nSumVal);
#endif
#elif E06_LINEAR
#if E06_LINEAR_01
			/*
			 * 배열 vs 리스트
			 * - 배열은 한번 정해진 크기를 변경하는 것이 불가능하기 때문에
			 * 기존에 정해진 길이보다 더 많은 데이터를 처리하기 위해서는
			 * 배열을 길이를 변경해주는 작업을 직접 만들어 줄 필요가 있다.
			 * 
			 * 반면, 리스트는 내부적으로 입력 된 데이터의 개수에 맞춰서 자동으로
			 * 데이터를 저장 할 수 있는 공간을 늘려주기 때문에 배열에 비해
			 * 좀 더 많은 데이터를 손쉽게 처리하는 것이 가능하다.
			 * 
			 * C# 리스트 종류
			 * - 배열 리스트
			 * - 연결 리스트
			 * 
			 * 배열 리스트란?
			 * - 배열을 기반으로 데이터의 순서를 보장하는 컬렉션을 의미한다.
			 * (즉, 내부적으로 배열로 데이터를 보관하고 제어하기 때문에 빠른 속도로
			 * 데이터에 접근하는 것이 가능하다.)
			 * 
			 * 단, 배열을 기반으로 데이터를 처리하기 때문에 기존에 존재하는 배열의
			 * 길이보다 더 많은 데이터가 추가 될 경우 내부적으로 배열을 새롭게 할당하는
			 * 추가적인 연산이 필요하다는 단점이 존재한다.
			 * 
			 * 연결 리스트란?
			 * - 노드를 기반으로 데이터의 순서를 보장하는 컬렉션을 의미한다.
			 * (즉, 내부적으로 데이터의 참조를 통해 여러 데이터를 관리하기 때문에 특정
			 * 데이터에 접근하기 위한 성능이 배열 리스트에 비해 상대적으로 떨어지는
			 * 단점이 존재한다.)
			 * 
			 * 단, 기존의 데이터보다 많은 데이터가 추가 된다하더라도 배열 리스트에 비해
			 * 내부적으로 발생하는 추가적인 연산이 적다는 장점이 존재한다.
			 */
			List<int> oValList01 = new List<int>();
			LinkedList<int> oValList02 = new LinkedList<int>();

			/*
			 * 리스트는 배열과 달리 처음부터 데이터를 저장하기 위한 공간을
			 * 생성하지 않기 때문에 처음 생성 된 리스트에 [ ] (인덱스 연산자)
			 * 를 통해 임의 접근을 시도하면 잘못된 인덱스 참조에 의해서
			 * 프로그램 종료되는 문제가 발생한다.
			 * 
			 * 따라서, 처음 리스트에 데이터를 저장하기 위해서는 Add 및 Insert
			 * 계열 메서드를 사용해야하며 해당 메서드를 통해 데이터를 추가하고
			 * 나서야 내부적으로 데이터를 저장하는 공간이 생성 된다는 것을
			 * 알 수 있다. (즉, Add 및 Insert 계열 메서드는 데이터를 추가함과
			 * 동시에 데이터를 저장하기 위한 공간도 생성하는 역할을 수행한다는
			 * 것을 알 수 있다.)
			 */
			for(int i = 0; i < 10; ++i)
			{
				oValList01.Add(i + 1);
				oValList02.AddLast(i + 1);
			}

			Console.WriteLine("=====> 배열 리스트 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			oValList01.Remove(10);
			oValList01.RemoveAt(0);

			Console.WriteLine("\n\n=====> 배열 리스트 - 삭제 후 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			oValList01.Sort((a_nLhs, a_nRhs) => a_nRhs.CompareTo(a_nLhs));
			Console.WriteLine("\n\n=====> 배열 리스트 - 내림차순 정렬 후 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			oValList01.Sort((a_nLhs, a_nRhs) => a_nLhs.CompareTo(a_nRhs));
			Console.WriteLine("\n\n=====> 배열 리스트 - 오름차순 정렬 후 <=====");

			for(int i = 0; i < oValList01.Count; ++i)
			{
				Console.Write("{0}, ", oValList01[i]);
			}

			Console.WriteLine("\n\n=====> 연결 리스트 <=====");
			LinkedListNode<int> oNode = oValList02.First;

			while(oNode != null)
			{
				Console.Write("{0}, ", oNode.Value);
				oNode = oNode.Next;
			}

			oValList02.Remove(10);
			oValList02.Remove(oValList02.Find(3));

			Console.WriteLine("\n\n=====> 연결 리스트 - 삭제 후 <=====");
			oNode = oValList02.First;

			while(oNode != null)
			{
				Console.Write("{0}, ", oNode.Value);
				oNode = oNode.Next;
			}

			Console.WriteLine();
#elif E06_LINEAR_02
			/*
			 * 스택이란?
			 * - LIFO (Last In First Out) 구조를 지니는 컬렉션을 의미한다.
			 * (즉, 마지막에 추가 된 데이터가 가장 처음 출력 된다는 것을 
			 * 알 수 있다.)
			 * 
			 * 큐란?
			 * - FIFO (First In First Out) 구조를 지니는 컬렉션을 의미한다.
			 * (즉, 처음 추가 된 데이터가 가장 처음 출력 된다는 것을 알 수
			 * 있다.)
			 * 
			 * 따라서, 스택과 큐는 일반적인 다른 컬렉션과 달리 데이터를 입력하고
			 * 출력하는 순서가 제한 된다는 특징이 있다. (즉, 자유롭게 특정 위치에
			 * 존재하는 데이터를 가져오는 기능을 제공하지 않는다.)
			 */
			Stack<int> oValStack = new Stack<int>();
			Queue<int> oValQueue = new Queue<int>();

			Console.WriteLine("=====> 데이터 입력 순서 <=====");

			for(int i = 0; i < 15; ++i)
			{
				oValStack.Push(i + 1);
				oValQueue.Enqueue(i + 1);

				Console.Write("{0}, ", i + 1);
			}

			Console.WriteLine("\n\n=====> 스택 데이터 출력 순서 <=====");

			while(oValStack.Count >= 1)
			{
				Console.Write("{0}, ", oValStack.Pop());
			}

			Console.WriteLine("\n\n=====> 큐 데이터 출력 순서 <=====");

			while(oValQueue.Count >= 1)
			{
				Console.Write("{0}, ", oValQueue.Dequeue());
			}

			Console.WriteLine();
#endif
#elif E06_NON_LINEAR
			/*
			 * 셋이란?
			 * - 데이터의 중복을 허용하지 않는 컬렉션을 의미한다. (즉, 셋을
			 * 활용하면 중복적으로 존재하는 특정 데이터 컬렉션으로부터 순열
			 * 데이터를 필터링하는 것이 가능하다.)
			 * 
			 * 따라서, 셋은 다른 컬렉션과 달리 범용적으로 사용되는 컬렉션이
			 * 아니라 특정 상황에서만 사용되는 특징이 존재한다.
			 * 
			 * 딕셔너리란?
			 * - 데이터를 찾기 위한 탐색에 특화 된 컬렉션을 의미한다. (즉,
			 * 딕셔너리 컬렉션은 저장 된 데이터가 많을수록 다른 컬렉션에
			 * 비해 빠르게 특정 데이터를 가져오는 것이 가능하다.)
			 * 
			 * 또한, 딕셔너리 컬렉션은 키 (Key) 와 벨류 (Value) 를 하나의
			 * 쌍으로 관리하는 컬렉션이기 때문에 키는 중복을 허용하지 않는
			 * 반면 벨류는 실제 관리 할 데이터를 의미하기 때문에 중복을
			 * 허용하는 특징이 존재한다. (즉, 키 데이터는 벨류를 탐색하기
			 * 위한 식별자를 의미한다.)
			 */
			Random oRandom = new Random();
			HashSet<int> oValSet = new HashSet<int>();
			Dictionary<string, int> oValDict = new Dictionary<string, int>();

			for(int i = 0; i < 10; ++i)
			{
				string oKey = $"Key_{i + 1:00}";
				oValDict.Add(oKey, i + 1);
			}

			for(int i = 0; i < 20; ++i)
			{
				oValSet.Add(oRandom.Next(0, 10));
			}

			Console.WriteLine("=====> 셋 <=====");

			/*
			 * foreach 반복문은 특정 컬렉션을 기반으로 해당 컬렉션이 관리하는
			 * 데이터를 순회하는 역할을 수행한다. (즉, foreach 반복문은 다른
			 * 반복문과 달리 컬렉션을 필요로 한다는 것을 알 수 있다.)
			 * 
			 * 따라서, 특정 컬렉션이 관리하는 데이터에 모두 접근하고 싶을 경우
			 * foreach 반복문을 활용하면 된다.
			 */
			foreach(int nVal in oValSet)
			{
				Console.Write("{0}, ", nVal);
			}

			Console.WriteLine("\n\n=====> 딕셔너리 <=====");

			foreach(var stKeyVal in oValDict)
			{
				Console.Write("{0}:{1}, ", stKeyVal.Key, stKeyVal.Value);
			}

			oValSet.Remove(1);
			Console.WriteLine("\n\n=====> 셋 - 제거 후 <=====");

			foreach(int nVal in oValSet)
			{
				Console.Write("{0}, ", nVal);
			}

			oValDict.Remove("Key_01");
			Console.WriteLine("\n\n=====> 딕셔너리 - 제거 후 <=====");

			foreach(var stKeyVal in oValDict)
			{
				Console.Write("{0}:{1}, ", stKeyVal.Key, stKeyVal.Value);
			}

			/*
			 * 딕셔너리 컬렉션에 존재하는 특정 데이터를 변경하거나 가져오고
			 * 싶을 때는 [ ] (인덱스 연산자) 와 키 데이터를 명시하면 된다.
			 * (즉, 인덱스 연산자에 키 데이터를 명시 할 경우 해당 키와 쌍을
			 * 이루는 벨류를 제어 할 수 있다는 것을 알 수 있다.)
			 */
			oValDict["Key_02"] = 200;
			Console.WriteLine("\n\n=====> 딕셔너리 - 변경 후 <=====");

			foreach(var stKeyVal in oValDict)
			{
				Console.Write("{0}:{1}, ", stKeyVal.Key, stKeyVal.Value);
			}

			Console.WriteLine();
#endif
		}
	}
}
