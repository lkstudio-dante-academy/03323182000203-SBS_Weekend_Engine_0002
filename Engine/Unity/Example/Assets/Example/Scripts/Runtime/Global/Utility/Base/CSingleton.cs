using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 싱글턴 패턴이란?
 * - 특정 클래스를 통해 생성되는 객체의 개수를 하나로 제한하는 디자인 패턴을
 * 의미한다. (즉, 싱글턴 패턴을 활용하면 프로그램 전체에서 공유하는 객체를
 * 생성하는 것이 가능하다.)
 */
/** 싱글턴 */
public class CSingleton<T> : CComponent where T : CSingleton<T> {
	#region 클래스 변수
	private static T m_tInst = null;
	#endregion // 클래스 변수

	#region 클래스 프로퍼티
	public static T Inst {
		get {
			// 인스턴스가 존재하지 않을 경우
			if(CSingleton<T>.m_tInst == null) {
				var oGameObj = new GameObject(typeof(T).ToString());

				/*
				 * AddComponent 메서드는 특정 게임 객체에 새로운 컴포넌트를 추가하는
				 * 역할을 수행한다.
				 * 
				 * 또한, 해당 메서드는 정상적으로 컴포넌트가 추가 되었을 경우 해당
				 * 컴포넌트의 참조가 반환된다.
				 * 
				 * 따라서, 게임 객체를 Instantiate 메서드를 통하지 않고 직접 생성했을
				 * 경우 해당 메서드를 사용해서 원하는 컴포넌트를 추가시키면 된다는
				 * 것을 알 수 있다.
				 */
				CSingleton<T>.m_tInst = oGameObj.ExAddComponent<T>();
			}

			return CSingleton<T>.m_tInst;
		}
	}
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		/*
		 * DontDestroyOnLoad 메서드는 입력으로 전달 된 게임 객체를 씬이 전환되어도
		 * 제거 되지 않도록 하는 역할을 수행한다. (즉, 해당 메서드를 활용하면 씬 간에
		 * 공통적으로 사용되는 공유 객체를 생성하는 것이 가능하다.)
		 * 
		 * Unity 씬은 일반적으로 다른 씬으로 전환 될 때 기존 씬에 존재하는 모든 게임
		 * 객체를 제거하는 특징이 존재하기 때문에 씬 간에 공통으로 필요한 데이터를
		 * 공유하기 위해서는 해당 메서드를 활용 할 수 밖에 없다. (즉, 씬이 전환되어도
		 * 제거 되지 않는 객체를 생성함으로서 해당 객체를 통해 씬 간에 데이터를 공유
		 * 할 수 있다는 것을 알 수 있다.)
		 * 
		 * 단, 해당 메서드에 입력으로 전달되는 게임 객체를 반드시 루트 객체여야한다.
		 * (즉, 자식 객체는 해당 메서드를 통해서 게임 객체를 유지하는 것이 불가능하다는
		 * 것을 알 수 있다.)
		 */
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion // 함수

	#region 클래스 함수
	/** 인스턴스를 생성한다 */
	public static T Create() {
		return CSingleton<T>.Inst;
	}
	#endregion // 클래스 함수
}
