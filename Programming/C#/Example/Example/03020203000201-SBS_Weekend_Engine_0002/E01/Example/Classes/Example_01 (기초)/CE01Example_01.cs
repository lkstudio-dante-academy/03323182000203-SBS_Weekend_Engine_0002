using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 프로그래밍 언어란?
 * - 컴퓨터가 특정 역할을 수행할 수 있게 명령문을 작성하기 위한 언어를 
 * 의미한다. (즉, 프로그래밍 언어는 사람과 컴퓨터 간에 대화를 하기 위한 
 * 수단이라는 것을 알 수 있다.)
 * 
 * 단, 컴퓨터는 오직 0 과 1 로 이루어진 기계어 (네이티브 코드) 만을 이해 할 
 * 수 있기 때문에 프로그래밍 언어로 작성 된 명령문을 컴퓨터가 이해하기 
 * 위해서는 반드시 해당 명령문을 기계어로 변경 해 줄 필요가 있다.
 * 
 * 따라서, 고급 언어 (프로그래밍 언어) 를 저급 언어 (기계어) 로 변환하기 
 * 위한 프로그램이 필요하며 해당 프로그램을 컴파일러 or 인터프리터고 한다.
 */
namespace Example._03020203000201_SBS_Weekend_Engine_0002.E01.Example.Classes.Example_01
{
	class CE01Example_01
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
			/*
			 * Console.Write 계열 메서드는 콘솔 창에 특정 문장을 출력하는
			 * 역할을 수행한다. (즉, 해당 메서드를 활용하면 한글을 비롯한
			 * 다양한 문장을 화면 상에 출력하는 것이 가능하다.)
			 */
			Console.WriteLine("Hello, World!");
		}
	}
}
