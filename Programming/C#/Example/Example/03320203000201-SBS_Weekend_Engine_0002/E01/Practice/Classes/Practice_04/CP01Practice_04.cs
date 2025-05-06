using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example._03320282000201_SBS_Weekend_Engine_0002.E01.Practice.Classes.Practice_04
{
	class CP01Practice_04
	{
		/** 초기화 */
		public static void Start(string[] args)
		{
			EMenu eMenu = EMenu.NONE;
			CCanvas oCanvas = new CCanvas();

			do
			{
				PrintMenus();
				Console.Write("\n메뉴 선택 : ");

				int.TryParse(Console.ReadLine(), out int nMenu);
				eMenu = (EMenu)(nMenu - 1);

				// 종료를 선택했을 경우
				if(eMenu == EMenu.EXIT)
				{
					continue;
				}

				// 모든 도형 그리기를 선택했을 경우
				if(eMenu == EMenu.DRAW_ALL_SHAPES)
				{
					oCanvas.DrawAllShapes();
				}
				else
				{
					CShape oShape = CreateShape(eMenu);
					oCanvas.AddShape(oShape);
				}

				Console.WriteLine();
			} while(eMenu != EMenu.EXIT);
		}

		/** 메뉴를 출력한다 */
		public static void PrintMenus()
		{
			Console.WriteLine("=====> 메뉴 <=====");
			Console.WriteLine("1. 삼각형 추가");
			Console.WriteLine("2. 사각형 추가");
			Console.WriteLine("3. 모든 도형 그리기");
			Console.WriteLine("4. 종료");
		}

		/** 도형을 생성한다 */
		public static CShape CreateShape(EMenu a_eMenu)
		{
			Random oRandom = new Random();
			EColor eColor = (EColor)oRandom.Next(0, (int)EColor.MAX_VAL);

			switch(a_eMenu)
			{
				case EMenu.ADD_TRIANGLE:
					return new CTriangle(eColor);
				case EMenu.ADD_RECTANGLE:
					return new CRectangle(eColor);
			}

			return null;
		}

		/** 메뉴 */
		public enum EMenu
		{
			NONE = -1,
			ADD_TRIANGLE,
			ADD_RECTANGLE,
			DRAW_ALL_SHAPES,
			EXIT,
			MAX_VAL
		}

		/** 색상 */
		public enum EColor
		{
			NONE = -1,
			RED,
			GREEN,
			BLUE,
			MAX_VAL
		}

		/** 도형 */
		public abstract class CShape
		{
			public EColor Color { get; private set; } = EColor.NONE;

			/** 생성자 */
			public CShape(EColor a_eColor)
			{
				this.Color = a_eColor;
			}

			/** 색상 문자열을 반환한다 */
			public string GetColorStr()
			{
				switch(this.Color)
				{
					case EColor.RED:
						return "빨간색";
					case EColor.GREEN:
						return "녹색";
					case EColor.BLUE:
						return "파란색";
				}

				return string.Empty;
			}

			/*
			 * 추상 (순수 가상) 메서드란?
			 * - 메서드 몸체가 존재하지 않는 가상 메서드를 의미한다. (즉, 메서드 몸체가
			 * 존재하지 않기 때문에 해당 메서드를 호출했을때 어떤 명령문이 실행 된다는
			 * 것을 알 수 없다.)
			 * 
			 * C# 클래스가 추상 메서드를 하나 이상 포함하고 있을 경우 해당 클래스는
			 * 객체화 시킬 수 없는 추상 클래스가 된다.
			 * 
			 * 따라서, 추상 클래스를 객체화 시키기 위해서는 자식 클래스에서 부모 클래스에
			 * 존재하는 추상 메서드를 override 해줘야한다. (즉, 추상 클래스를 직접적으로
			 * 객체화 시키는 것은 불가능하지만 자식 클래스를 통해 간접적으로 객체화 시킬 수
			 * 있다는 것을 알 수 있다.)
			 * 
			 * 만약, 자식 클래스가 부모 클래스에 존재하는 추상 메서드를 하나라도 override
			 * 하지 않았을 경우 자식 클래스 또한 객체화 시킬 수 없는 추상 클래스가 되는
			 * 특징이 존재한다.
			 * 
			 * C# 추상 클래스 구현 방법
			 * - abstract + class + 클래스 이름 + 클래스 멤버
			 * 
			 * C# 추상 메서드 선언 방법
			 * - abstract + 반환 형 + 메서드 이름 + 매개 변수
			 */
			/** 도형을 그린다 */
			public abstract void Draw();
		}

		/** 삼각형 */
		public class CTriangle : CShape
		{
			/** 생성자 */
			public CTriangle(EColor a_eColor) : base(a_eColor)
			{
				// Do Something
			}

			/** 도형을 그린다 */
			public override void Draw()
			{
				Console.WriteLine("{0} 삼각형을 그렸습니다.", this.GetColorStr());
			}
		}

		/** 사각형 */
		public class CRectangle : CShape
		{
			/** 생성자 */
			public CRectangle(EColor a_eColor) : base(a_eColor)
			{
				// Do Something
			}

			/** 도형을 그린다 */
			public override void Draw()
			{
				Console.WriteLine("{0} 사각형을 그렸습니다.", this.GetColorStr());
			}
		}

		/** 캔버스 */
		public class CCanvas
		{
			private List<CShape> m_oShapeList = new List<CShape>();

			/** 도형을 추가한다 */
			public void AddShape(CShape a_oShape)
			{
				m_oShapeList.Add(a_oShape);
			}

			/** 모든 도형을 그린다 */
			public void DrawAllShapes()
			{
				for(int i = 0; i < m_oShapeList.Count; ++i)
				{
					m_oShapeList[i].Draw();
				}
			}
		}
	}
}
