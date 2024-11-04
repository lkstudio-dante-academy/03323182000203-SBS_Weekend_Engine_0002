using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 연산자란?
 * - 프로그램이 동작 중에 특정 역할을 수행하기 위한 특별한 심볼을 의미한다.
 * (즉, 연산자를 활용하면 데이터를 처리하거나 특정 명령문을 실행하는 행위가
 * 가능하다는 것을 알 수 있다.)
 * 
 * C# 주요 연산자 종류
 * - 산술 연산자 (+, -, *, /, %)
 * - 관계 연산자 (<, >, <=, >=, ==, !=)
 * - 논리 연산자 (&&, ||, !)
 * - 할당 연산자 (=, +=, -=, *= 등등...)
 * - 비트 연산자 (&, |, ^, ~, <<, >>)
 * - 증감 연산자 (++, --)
 */
namespace Example._03020203000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_03
{
	class CE01Example_03
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
			Console.Write("정수 (2 개) 입력 : ");
			var oTokens = Console.ReadLine().Split();

			int.TryParse(oTokens[0], out int nLhs);
			int.TryParse(oTokens[1], out int nRhs);

			/*
			 * 산술 연산자에 사용 되는 피 연산자는 기본적으로 모두 동일한 자료형이여야한다.
			 * 만약, 피 연산자의 자료형이 서로 다를 경우 내부적으로 자료형을 일치시키기위한
			 * 형 변환이 발생하며 이를 암시적 (묵시적) 형 변환이라고 한다.
			 * 
			 * 이때, 암시적 형 변환이 발생하는 기준은 최대한 데이터의 손실이 덜 발생하는
			 * 방향으로 형 변환이 발생한다는 특징이 존재한다.
			 * 
			 * 따라서, 정수와 실수를 산술 연산 했을 경우 내부적으로 정수를 실수로 형 변환
			 * 후 연산을 수행한다는 것을 알 수 있다.
			 */
			Console.WriteLine("=====> 산술 연산자 <=====");
			Console.WriteLine("{0} + {1} = {2}", nLhs, nRhs, nLhs + nRhs);
			Console.WriteLine("{0} - {1} = {2}", nLhs, nRhs, nLhs - nRhs);
			Console.WriteLine("{0} * {1} = {2}", nLhs, nRhs, nLhs * nRhs);
			Console.WriteLine("{0} / {1} = {2}", nLhs, nRhs, nLhs / (float)nRhs);
			Console.WriteLine("{0} % {1} = {2}", nLhs, nRhs, nLhs % nRhs);

			Console.WriteLine("\n=====> 관계 연산자 <=====");
			Console.WriteLine("{0} < {1} = {2}", nLhs, nRhs, nLhs < nRhs);
			Console.WriteLine("{0} > {1} = {2}", nLhs, nRhs, nLhs > nRhs);
			Console.WriteLine("{0} <= {1} = {2}", nLhs, nRhs, nLhs <= nRhs);
			Console.WriteLine("{0} >= {1} = {2}", nLhs, nRhs, nLhs >= nRhs);
			Console.WriteLine("{0} == {1} = {2}", nLhs, nRhs, nLhs == nRhs);
			Console.WriteLine("{0} != {1} = {2}", nLhs, nRhs, nLhs != nRhs);

			bool bIsLhs = nLhs != 0;
			bool bIsRhs = nRhs != 0;

			Console.WriteLine("\n=====> 논리 연산자 <=====");
			Console.WriteLine("{0} && {1} = {2}", nLhs, nRhs, bIsLhs && bIsRhs);
			Console.WriteLine("{0} || {1} = {2}", nLhs, nRhs, bIsLhs || bIsRhs);
			Console.WriteLine("!{0} = {1}", nLhs, !bIsLhs);

			/*
			 * 전위 증감 연산자 vs 후위 증감 연산자
			 * - 전위 증감 연산자는 선 증감 후 연산의 순서를 지니고 있기 때문에
			 * 특정 변수가 지니고 있는 데이터를 먼저 변화 시킨 후 연산에 사용하는
			 * 특징이 존재한다.
			 * 
			 * 반면, 후위 증감 연산자는 선 연산 후 증감의 순서를 지니고 있기 때문에
			 * 특정 변수가 지니고 있는 데이터를 연산에 먼저 사용한 후 해당 변수가
			 * 지니고 있는 데이터를 변화 시킨다는 차이점이 존재한다.
			 */
			Console.WriteLine("\n=====> 증감 연산자 <=====");
			Console.WriteLine("++{0}, --{1}", ++nLhs, --nRhs);
			Console.WriteLine("{0}++, {1}--", nLhs++, nRhs--);

			Console.WriteLine("\n=====> 후위 증감 연산자 후 <=====");
			Console.WriteLine("{0}, {1}", nLhs, nRhs);

			Console.WriteLine("\n=====> 비트 연산자 <=====");
			Console.WriteLine("{0} & {1} = {2}", nLhs, nRhs, nLhs & nRhs);
			Console.WriteLine("{0} | {1} = {2}", nLhs, nRhs, nLhs | nRhs);
			Console.WriteLine("{0} ^ {1} = {2}", nLhs, nRhs, nLhs ^ nRhs);
			Console.WriteLine("~{0} = {1}", nLhs, ~nLhs);
			Console.WriteLine("{0} << 1 = {1}", nLhs, nLhs << 1);
			Console.WriteLine("{0} >> 1 = {1}", nRhs, nRhs >> 1);
		}
	}
}
