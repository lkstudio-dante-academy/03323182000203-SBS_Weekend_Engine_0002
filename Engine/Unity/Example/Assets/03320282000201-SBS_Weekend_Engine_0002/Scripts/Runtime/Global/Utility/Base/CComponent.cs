using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/** 최상위 컴포넌트 */
public abstract class CComponent : MonoBehaviour
{
	#region 프로퍼티
	public bool IsDestroy { get; private set; } = false;
	#endregion // 프로퍼티

	#region 함수
	/*
     * 이벤트 메서드란?
     * - Unity 가 동작 중에 발생하는 여러 변화를 감지하고 처리 할 수 있는
     * 메서드를 의미한다. (즉, Unity 는 많은 이벤트 메서드를 제공하며 해당
     * 메서드를 활용하면 특정 상황에 대한 적절 처리를 구현하는 것이 가능하다.)
     * 
     * Unity 주요 이벤트 메서드
     * - Awake
     * - Start
     * - Update
     * - LateUpdate
     * - OnDestroy
     * 
     * Awake 메서드 vs Start 메서드
     * - 두 메서드 모두 특정 Game Object 를 초기화하기 위한 용도로 활용된다.
     * 
     * Awake 메서드는 Game Object 가 활성 상태가 되는 즉시 호출되는 반면
     * Start 메서드는 활성 상태가 되고 이후 프레임에 호출되는 차이점이 존재
     * 한다.
     * 
     * 따라서, 특정 Game Object 를 생성과 동시에 해당 Game Object 지니고
     * 있는 컴포넌트를 활용하고 싶다면 Awake 메서드 적절하다는 것을 알 수
     * 있다.
     * 
     * Update 메서드란?
     * - 매 프레임마다 호출되는 메서드를 의미하며 해당 메서드를 활용하면
     * 실시간으로 상태가 변하는 객체를 제어하는 것이 가능하다. (즉, Update
     * 메서드는 Unity 가 제공하는 이벤트 메서드 중 가장 호출 빈도가 많다는
     * 것을 알 수 있다.
     * 
     * 또한, Unity 는 LateUpdate 메서드를 제공하며 해당 메서드는 다른 
     * Game Object 의 Update 메서드가 모두 호출 된 후 호출되는 특징이 
     * 존재한다. (즉, 특정 Game Object 를 모두 갱신 다음에 이후 해당 객체를 
     * 제어하고 싶다면 LateUpdate 메서드를 활용하면 된다.)
     */
	/** 초기화 */
	public virtual void Awake()
	{
		// Do Something
	}

	/** 초기화 */
	public virtual void Start()
	{
		// Do Something
	}

	/** 상태를 리셋한다 */
	public virtual void Reset()
	{
		// Do Something
	}

	/** 제거 되었을 경우 */
	public virtual void OnDestroy()
	{
		this.IsDestroy = true;
	}

	/** 상태를 갱신한다 */
	public virtual void OnUpdate(float a_fDeltaTime)
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnLateUpdate(float a_fDeltaTime)
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnFixedUpdate(float a_fDeltaTime)
	{
		// Do Something
	}

	/** 내비게이션 스택 이벤트를 수신했을 경우 */
	public virtual void OnReceiveNavStackEvent(ENavStackEvent a_eEvent)
	{
		// Do Something
	}
	#endregion // 함수
}
