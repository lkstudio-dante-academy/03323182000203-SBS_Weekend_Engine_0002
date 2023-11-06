using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Collision vs Trigger
 * - 두 방식 모두 Rigidbody 컴포넌트를 지니고 있는 물체가 다른 물체와 충돌
 * 했을때 해당 사실을 전달 받을 수 있는 기능을 제공한다.
 * 
 * Collision 는 충돌에 대한 사실을 전달해줌과 동시에 물리적인 상호작용이
 * 발생하는 반면, Trigger 는 충돌에 대한 사실만 전달해주고 물리적인 상호작용은
 * 발생하지 않는 차이점 존재한다. (즉, 단순한 충돌 사실을 검사하고 싶다면 
 * Trigger 를 사용하는 것이 좀 더 빠르게 연산을 처리 할 수 있다.)
 * 
 * 단, Unity 가 충돌 사실을 알려주는 것은 충돌한 대상에게만 해당 사실을 알려주기
 * 때문에 만약 충돌을 일으킨 대상이 아닌 다른 물체에서 충돌 사실을 검사하고
 * 싶을 경우 충돌에 대한 이벤트를 전파 시킬 수 있는 별도의 구조를 제작해줘야한다.
 * (즉, Unity 로부터 충돌에 대한 사실을 전달 받을 수 있는 대상은 Rigidbody
 * 컴포넌트를 지니고 있는 대상만 가능하다는 것을 알 수 있다.)
 */
/** 충돌 전달자 */
public class CCollisionDispatcher : CComponent {
	#region 프로퍼티
	/*
	 * System.Func 과 System.Action 는 C# 에서 제공하는 델리게이트를 의미한다.
	 * (즉, 해당 델리게이트를 활용하면 매번 새롭게 델리게이트를 선언 할 필요가 
	 * 없다는 것을 알 수 있다.)
	 * 
	 * System.Func 델리게이트는 반환 값이 존재하는 메서드를 제어 할 수 있으며
	 * System.Action 델리게이트는 반환 값이 없는 메서드를 제어 할 수 있다.
	 */
	public System.Action<CCollisionDispatcher, Collision> EnterCallback { get; set; } = null;
	public System.Action<CCollisionDispatcher, Collision> StayCallback { get; set; } = null;
	public System.Action<CCollisionDispatcher, Collision> ExitCallback { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 충돌이 시작 되었을 경우 */
	public void OnCollisionEnter(Collision other) {
		this.EnterCallback?.Invoke(this, other);
	}

	/** 충돌이 진행 중 일 경우 */
	public void OnCollisionStay(Collision other) {
		this.StayCallback?.Invoke(this, other);
	}

	/** 충돌이 종료 되었을 경우 */
	public void OnCollisionExit(Collision other) {
		this.ExitCallback?.Invoke(this, other);
	}
	#endregion // 함수
}
