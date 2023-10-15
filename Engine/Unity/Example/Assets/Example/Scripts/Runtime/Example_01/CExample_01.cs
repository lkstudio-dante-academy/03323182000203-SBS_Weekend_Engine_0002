using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 컴포넌트 기반 프로그래밍이란?
 * - 기능에 해당하는 컴포넌트를 조합함으로서 특정 사물을 표현하는 설계 방식을
 * 의미한다. (즉, 컴포넌트 기반 프로그래밍은 객체 지향 프로그래밍과 달리 특정
 * 사물을 정의하고 해당 사물의 기능을 추가하는 개념과 반대라는 것을 알 수
 * 있다.)
 * 
 * 따라서, 컴포넌트 기반 프로그래밍 개념을 활용하면 특정 사물의 역할을 
 * 유연하게 표현하는 것이 가능하다. (즉, 객체 지향 프로그래밍에서는 사물이 
 * 생성되는 순간 해당 사물의 역할이 결정되고 해당 역할을 바뀌지 않는 반면 
 * 컴포넌트 기반 프로그래밍은 컴포넌트의 조합을 통해서 사물의 역할을 결정하기 
 * 때문에 객체 지향 프로그래밍 방식에 비해 좀 더 자유롭게 사물의 역할을 
 * 표현하는 것이 가능하다.)
 * 
 * Unity 컴포넌트 관련 용어
 * - Component
 * - Game Object
 * 
 * Game Object 란?
 * - 컴포넌트를 조합하기 위한 그릇을 의미한다. (즉, Game Object 자체는 
 * 아무런 역할도 수행하지 않지만 해당 객체에 컴포넌트를 추가함으로서 특정 
 * 역할을 수행하는 사물을 표현하는 것이 가능하다.)
 * 
 * 또한, Unity 의 Game Object 는 반드시 Transform 컴포넌트를 지니고 
 * 있어야하기 때문에 Transform 컴포넌트는 제거하는 것이 불가능하다. (즉, 
 * Transform 컴포넌트를 제외한 다른 컴포넌트는 필요에 따라 얼마든지 
 * Game Object 에 추가하거나 제거하는 것이 가능하다.)
 * 
 * Transform 컴포넌트란?
 * - Unity 씬 상에 배치되는 Game Object 의 변환에 대한 것을 제어하는
 * 컴포넌트를 의미한다. (즉, Transform 컴포넌트를 활용하면 특정 물체의
 * 위치나 회전 정도를 제어하는 것이 가능하다.)
 * 
 * 또한, Transform 컴포넌트는 부모/자식의 관계를 형성하는 것이 가능하기
 * 때문에 해당 컴포넌트를 활용하면 Game Object 의 계층적인 구조를 표현하는
 * 것이 가능하다. (즉, Game Object 는 Transform 컴포넌트가 존재하기 때문에
 * 특정 Game Object 의 하위로 추가가 가능하다는 것을 알 수 있다.)
 */
/** Example 1 */
public class CExample_01 : CSceneManager
{
    #region 프로퍼티
    public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_01;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        Debug.Log("Hello, World!");
    }
    #endregion // 함수
}
