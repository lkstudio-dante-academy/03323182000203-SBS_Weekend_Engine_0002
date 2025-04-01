using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 3 */
public class CE01Example_03 : CSceneManager
{
	#region 변수
	[SerializeField] private Light m_oLight = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_03;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 좌/우 방향 키를 눌렀을 경우
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
		{
			float fDirection = Input.GetKey(KeyCode.LeftArrow) ?
				-1.0f : 1.0f;

			/*
			 * Unity 물체 회전 표현 방식
			 * - 오일러 회전
			 * - 사원수 회전
			 * 
			 * 오일러 회전이란?
			 * - 물체의 각 기저 벡터를 회전하는 3 가지 값을 합쳐서 최종적으로
			 * 물체의 회전 정도를 표현하는 방법을 의미한다.
			 * 
			 * 따라서, 오일러 회전은 물체의 단순한 회전을 표현하는데 적합하며
			 * 물체의 회전 정도 복잡할 수록 내부적으로 많은 연산이 필요하다.
			 * 
			 * 이는 오일러 회전이 각 축에 대한 회전을 행렬을 통해 표현하기 때문에
			 * 행렬에 특징 상 결합 순서에 따라 결과 값이 달라지기 때문이다.
			 * 
			 * 따라서, 오일러 회전은 물체의 회전 상태를 표현하기 위해서 항상 
			 * Y (Yaw), X (Pitch), Z (Roll) 순으로 각 축의 회전 값을 연산한다는 
			 * 것을 알 수 있다.
			 * 
			 * 사원수 회전이란?
			 * - 물체의 회전 상태를 나타내기 위해서 회전이 되는 축과 각도를 합쳐서
			 * 4 개의 성분으로 물체의 회전 정도를 표현하는 방법을 의미한다.
			 * 
			 * 따라서, 사원수 회전은 오일러 회전에 비해 적은 연산으로 물체의 회전을
			 * 표현 할 수 있기 때문에 Unity 를 비롯한 많은 게임 엔진에서 기본적으로
			 * 지원하는 방식이다.
			 * 
			 * 또한, 사원수는 오일러 회전에 비해 결합 순서가 존재하지 않기 때문에
			 * 복잡한 물체의 회전 정도를 비교적 수월하게 표현하는 것이 가능하다.
			 */
			m_oLight.transform.Rotate(Vector3.up,
				90.0f * fDirection * Time.deltaTime, Space.World);
		}
	}
	#endregion // 함수
}
