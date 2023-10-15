using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 프리팹이란?
 * - 특정 Game Object 를 에셋에 형태로 저장 할 수 있는 기능을 의미한다.
 * (즉, 프리팹을 활용하면 Game Object 를 에셋의 형태로 저장함으로서 새로운
 * 사본 객체를 손쉽게 생성하는 것이 가능하다.)
 * 
 * 또한, 프리팹을 통해 생성 된 사본 Game Object 는 원본 프리팹과 변경 사항을
 * 공유하기 때문에 원본 프리팹 or 사본 Game Object 의 변경 사항을 손쉽게
 * 동기화하는 것이 가능하다.
 * 
 * Unity 는 컴포넌트 기반 프로그래밍 방식을 채택하고 있기 때문에 특정 역할을
 * 수행하는 사물을 생성하기 위해서는 사물에 해당하는 Game Object 를 생성 한
 * 다음에 컴포넌트를 추가하는 별도의 과정이 필요하다.
 * 
 * 따라서, Unity 에서 새로운 Game Object 를 생성하고 싶다면 미리
 * 정의 되어있는 원본 Game Object 를 기반으로 사본 Game Object 를 생성하는
 * 방식을 주로 사용한다. (즉, new GameObject 명령문을 통해 새로운 
 * Game Object 를 생성하는 것도 가능하지만 해당 방식은 잘 활용되지 않는다는
 * 것을 알 수 있다.)
 * 
 * 스크립트란?
 * - Unity 가 제공해주는 컴포넌트 이외에 사용자 (프로그래머) 가 필요에 따라
 * 직접 제작해서 사용하는 컴포넌트를 의미한다. (즉, 스크립트를 활용하면
 * 제작하는 프로젝트에 따라 원하는 기능을 자유롭게 구현하는 것이 가능하다.)
 * 
 * Unity 스크립트 제작 규칙
 * - C# 클래스 이름과 파일명 일치 (2021 버전 이하)
 * - 직/간접적으로 MonoBehaviour 클래스 상속
 * 
 * 위의 규칙을 따르지 않을 경우 해당 클래스는 스크립트 컴포넌트가 아닌 단순한
 * C# 의 클래스라는 것을 알 수 있다. (즉, new 키워드를 통해서 객체로 
 * 생성하는 것은 가능하지만 Game Object 에 컴포넌트 형태로 추가시키는 것은
 * 불가능하다는 것을 의미한다.)
 */
/** Example 2 */
public partial class CExample_02 : CSceneManager
{
    #region 변수
    /*
     * SerializeField 속성을 활용하면 protected 보호 수준 이상으로 명시 된
     * 멤버를 Unity 에디터 상에서 설정 할 수 있도록 입력 필드를 노출 시키는
     * 것이 가능하다. (즉, 해당 속성을 활용하지 않으면 public 보호 수준을
     * 제외하고는 Unity 에디터 상에서 해당 변수의 값을 설정하는 것이 불가능
     * 하다는 것을 알 수 있다.)
     */
    [SerializeField] private GameObject m_oTargetRoot = null;
    [SerializeField] private GameObject m_oOriginTarget = null;
    #endregion // 변수

    #region 프로퍼티
    public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_02;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
    }

    /** 상태를 갱신한다 */
    public void Update()
    {
        /*
         * Input 클래스란?
         * - 다양한 입력 장치에 대한 처리 기능을 제공하는 클래스를 의미한다.
         * (즉, 해당 클래스를 이용하면 키보드 및 마우스와 같은 입력 장치의
         * 입력 값에 따라 다양한 결과를 만들어내는 프로그램을 제작하는 것이
         * 가능하다.)
         */
        // 스페이스 키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
             * Instantiate 메서드를 활용하면 원본 Game Object 를 기반으로
             * 사본 Game Object 를 생성하는 것이 가능하다. (즉, 해당
             * 메서드를 활용하면 새로운 Game Object 손쉽게 생성하는 것이
             * 가능하다.)
             */
            var oTarget = Instantiate(m_oOriginTarget, 
                Vector3.zero, Quaternion.identity);
            
            /*
             * Transform 컴포넌트의 SetParent 메서드를 활용하면 특정 객체를
             * 자식으로 추가하는 것이 가능하다. (즉, 해당 메서드를 활용함으로서
             * Game Object 간에 계층적인 구조를 형성 시킬 수 있다.)
             */
            oTarget.transform.SetParent(m_oTargetRoot.transform, false);
        }
    }
    #endregion // 함수
}
