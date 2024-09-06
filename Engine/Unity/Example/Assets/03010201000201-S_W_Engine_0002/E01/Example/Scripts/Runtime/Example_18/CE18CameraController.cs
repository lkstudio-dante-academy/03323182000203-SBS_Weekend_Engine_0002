using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 카메라 제어자 */
public class CE18CameraController : CComponent
{
	#region 변수
	[Header("=====> Etc <=====")]
	[SerializeField] private float m_fHeight = 0.0f;
	[SerializeField] private float m_fDistance = 0.0f;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oLookTarget = null;
	[SerializeField] private GameObject m_oFollowTarget = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.UpdateCameraState(true);
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime)
	{
		base.OnLateUpdate(a_fDeltaTime);
		this.UpdateCameraState();
	}

	/** 카메라 상태를 갱신한다 */
	private void UpdateCameraState(bool a_bIsImmediate = false)
	{
		var stHeight = Vector3.up * m_fHeight;
		var stDistance = m_oFollowTarget.transform.forward * -m_fDistance;

		var stFollowTargetPos = m_oFollowTarget.transform.position;

		// 즉시 갱신 모드 일 경우
		if(a_bIsImmediate)
		{
			this.transform.position = stFollowTargetPos +
				stHeight + stDistance;
		}
		else
		{
			this.transform.position = Vector3.Lerp(this.transform.position,
				stFollowTargetPos + stHeight + stDistance, Time.deltaTime * 10.0f);
		}

		this.transform.LookAt(m_oLookTarget.transform);
	}
	#endregion // 함수
}
