using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 장애물 */
public class CE06Obstacle : CComponent {
	#region 변수
	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oUpObstacle = null;
	[SerializeField] private GameObject m_oDownObstacle = null;
	[SerializeField] private GameObject m_oSafeArea = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		float fUpObstaclePercent = Random.Range(0.1f, 0.5f);
		float fDownObstaclePercent = 0.6f - fUpObstaclePercent;
		float fSafeAreaPercent = 0.4f;

		float fUpObstacleHeight = KDefine.G_DESIGN_SCREEN_HEIGHT * 
			fUpObstaclePercent;

		float fDownObstacleHeight = KDefine.G_DESIGN_SCREEN_HEIGHT * 
			fDownObstaclePercent;

		float fSafeAreaHeight = KDefine.G_DESIGN_SCREEN_HEIGHT *
			fSafeAreaPercent;

		var stUpObstacleScale = m_oUpObstacle.transform.localScale;
		stUpObstacleScale.y = fUpObstacleHeight;

		var stDownObstacleScale = m_oDownObstacle.transform.localScale;
		stDownObstacleScale.y = fDownObstacleHeight;

		var stSafeAreaScale = m_oSafeArea.transform.localScale;
		stSafeAreaScale.y = fSafeAreaHeight;

		var stUpObstaclePos = m_oUpObstacle.transform.position;

		stUpObstaclePos.y = (KDefine.G_DESIGN_SCREEN_HEIGHT / 2.0f) - 
			(fUpObstacleHeight / 2.0f);

		var stDownObstaclePos = m_oDownObstacle.transform.position;

		stDownObstaclePos.y = (KDefine.G_DESIGN_SCREEN_HEIGHT / -2.0f) +
			(fDownObstacleHeight / 2.0f);

		var stSafeAreaPos = m_oSafeArea.transform.position;

		stSafeAreaPos.y = stUpObstaclePos.y -
			(fUpObstacleHeight / 2.0f) - (fSafeAreaHeight / 2.0f);

		m_oUpObstacle.transform.localScale = stUpObstacleScale;
		m_oUpObstacle.transform.position = stUpObstaclePos;

		m_oDownObstacle.transform.localScale = stDownObstacleScale;
		m_oDownObstacle.transform.position = stDownObstaclePos;

		m_oSafeArea.transform.localScale = stSafeAreaScale;
		m_oSafeArea.transform.position = stSafeAreaPos;
	}
	#endregion // 함수
}
