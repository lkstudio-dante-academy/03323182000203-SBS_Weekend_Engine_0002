using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 네임 스페이스란?
 * - 클래스를 비롯한 여러 기능을 하나로 묶어주는 논리적인 단위를 의미한다.
 * (즉, C# 에서 특정 역할을 수행하는 클래스 등에 접근하기 위해서는 해당 
 * 클래스가 속해 있는 네임 스페이스 경로를 명시해줘야한다는 것을 알 수 있다.)
 */
namespace Example {
	/*
	 * 클래스란?
	 * - 데이터와 기능을 하나로 묶어서 관리 할 수 있는 기능을 의미한다.
	 */
	class Program {
		/* 
		 * 메인 함수 (메서드) 란?
		 * - C# 으로 제작 된 프로그램이 실행 될 때 가장 먼저 실행되는 함수를
		 * 의미한다. (즉, 프로그램을 제작 할 때는 필요에 여러 파일을 생성하는
		 * 것이 가능하기 때문에 해당 파일 중 가장 먼저 실행 되어야 할 위치가
		 * 필요하며 해당 위치 역할을 하는 것이 메인 함수라는 것을 알 수 있다.)
		 * 
		 * 또한, 메인 함수가 종료되면 프로그램도 같이 종료되는 특징이 존재하기
		 * 때문에 메인 함수가 실행 되었다는 것은 프로그램이 실행 되었다는 것을
		 * 의미하며 메인 함수가 종료 되었다는 것은 프로그램이 종료 되었다는
		 * 것을 의미한다.
		 * 
		 * 함수 (메서드) 란?
		 * - 특정 역할을 수행하는 기능을 의미한다. (즉, 함수는 특정 역할을
		 * 수행하기 위한 명령문이 작성 되어있으며 해당 함수를 호출한다는 것은 
		 * 해당 함수에 작성 된 명령문을 실행한다는 것을 의미한다.)
		 */
		static void Main(string[] args) {
			//Classes.Example_01.CExample_01.Start(args);
			//Classes.Example_02.CExample_02.Start(args);
			//Classes.Example_03.CExample_03.Start(args);
			//Classes.Example_04.CExample_04.Start(args);
			Classes.Example_05.CExample_05.Start(args);

			/*
			 * Console.Read 계열 메서드는 콘솔 창으로부터 특정 문자를 입력 
			 * 받는 역할을 수행한다. (즉, 해당 메서드를 실행하면 콘솔에 
			 * 특정 문자를 입력 받기 전까지 프로그램이 계속 정지 되어있는 
			 * 특징이 있다.)
			 */
			Console.ReadKey();
		}
	}
}
