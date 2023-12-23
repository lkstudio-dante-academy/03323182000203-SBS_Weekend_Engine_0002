using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/*
 * 내비게이션 메쉬란?
 * - Unity 에서 지원하는 AI 관련 기능 중 하나로서 경로를 탐색하는 기능을 
 * 의미한다. (즉, 내비게이션 메쉬를 활용하면 특정 위치까지 이동하기 위한
 * 경로 탐색을 손쉽게 구현하는 것이 가능하다.)
 * 
 * Unity 는 내부적으로 특정 위치까지 이동하기 위한 경로를 계산하기 위해서
 * A* 알고리즘을 사용하고 있기 때문에 탐색에 대한 결과는 해당 경로까지
 * 이동하기 위한 최단 거리가 계산된다는 것을 알 수 있다.
 * 
 * 내비게이션 메쉬 관련 주요 컴포넌트
 * - Nav Mesh Surface (Unity 버전 2022 이상)
 * - Nav Mesh Agent
 * - Nav Mesh Obstacle
 * - Off Mesh Link
 * 
 * Nav Mesh Surface 컴포넌트란?
 * - 경로 탐색에 사용 될 내비게이션 메쉬 맵을 생성해주는 역할을 수행하는
 * 컴포넌트를 의미한다. (즉, 해당 컴포넌트를 내비게이션 메쉬 맵을 생성해야지만
 * 내부적으로 경로 탐색이 가능하다는 것을 알 수 있다.)
 * 
 * 해당 컴포넌트는 Unity 2022 버전 이상부터 사용 가능하며 해당 컴포넌트를
 * 활용하면 내비게이션 메쉬 맵을 동적으로 생성하는 것이 가능하다. (즉, 해당
 * 컴포넌트가 존재하지 않던 Unity 2022 미만 버전에서는 내비게이션 메쉬 맵을
 * 동적으로 생성하는 것이 불가능하다는 것을 알 수 있다.)
 * 
 * Nav Mesh Agent 컴포넌트란?
 * - 내비게이션 맵을 기반으로 실제 경로를 탐색하기 위한 대상을 제어하는 역할을
 * 수행한다. (즉, 특정 게임 객체가 해당 컴포넌트가 지니고 있다면 Unity 의
 * 내비게이션 메쉬를 이용해서 특정 위치가 이동하기 위한 경로를 탐색 후 해당
 * 정보를 기반으로 목적지까지 이동하기 위한 처리를 손쉽게 구현하는 것이
 * 가능하다.)
 * 
 * 단, Nav Mesh Agent 는 반드시 내비게이션 맵 위에서만 정상적으로 동작하기
 * 때문에 해당 컴포넌트를 지니고 있는 게임 객체는 반드시 내비게이션 맵 위에
 * 생성해 줄 필요가 있다. (즉, 내비게이션 맵 이외에 해당 컴포넌트를 지닌
 * 게임 객체를 생성 할 경우 내부적으로 에러가 발생한다는 것을 알 수 있다.)
 * 
 * Nav Mesh Obstacle 컴포넌트란?
 * - 동적으로 움직이는 장애물을 표현하는 역할을 수행하는 컴포넌트를 의미한다.
 * (즉, 기본적으로 장애물은 움직이지 않는 정적인 대상으로 인지하지만 해당
 * 컴포넌트를 활용하면 동적으로 움직이는 대상도 장애물로 인지시키는 것이
 * 가능하다.)
 * 
 * Off Mesh Link 컴포넌트란?
 * - 떨어져 있는 내비게이션 메쉬 맵을 연결해주는 역할을 수행하는 컴포넌트를
 * 의미한다. (즉, 내비게이션 메쉬 맵은 상황에 따라 2 개 이상 생성 될 수 
 * 있다는 것을 알 수 있다.)
 * 
 * Unity 의 Nav Mesh Agent 는 기본적으로 해당 컴포넌트가 위치해 있는
 * 내비게이션 맵 이외의 영역으로는 이동하는 것이 불가능하지만 떨어져 있는 
 * 내비게이션 맵이 Off Mesh Link 로 연결 되어있을 경우 해당 맵을 이동하는 
 * 것이 가능다는 특징이 존재한다.
 */
/** Example 15 */
public class CExample_15 : CSceneManager {
	#region 변수
	private Tween m_oObstacleAni = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oTarget = null;
	[SerializeField] private GameObject m_oObstacle = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_15;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oObstacleAni = m_oObstacle.transform.DOMoveX(400.0f, 2.0f);
		m_oObstacleAni.SetAutoKill().SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();
		m_oObstacleAni?.Kill();
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		var stRay = this.MainCamera.ScreenPointToRay(Input.mousePosition);
		bool bIsHit = Physics.Raycast(stRay, out RaycastHit stRaycastHit);

		// 클릭 된 물체가 존재 할 경우
		if(bIsHit && Input.GetMouseButtonDown((int)EMouseBtn.LEFT)) {
			var oNavMeshAgent = m_oTarget.GetComponent<NavMeshAgent>();
			oNavMeshAgent.SetDestination(stRaycastHit.point);
		}
	}
	#endregion // 함수
}
