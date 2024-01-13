using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 플레이어 */
public class CE18Player : CComponent {
	#region 변수
	private Animation m_oAnimation = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

		m_oAnimation = this.GetComponentInChildren<Animation>();
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		float fVertical = Input.GetAxis("Vertical");
		float fHorizontal = Input.GetAxis("Horizontal");

		this.UpdateTransformState(fVertical,
			fHorizontal, a_fDeltaTime);

		this.UpdateAnimationState(fVertical,
			fHorizontal, a_fDeltaTime);
	}

	/** 변환 상태를 갱신한다 */
	private void UpdateTransformState(float a_fVertical,
		float a_fHorizontal, float a_fDeltaTime) {

		var stDirection = (this.transform.forward * a_fVertical) +
			(this.transform.right * a_fHorizontal);

		// 보정이 필요 할 경우
		if(stDirection.magnitude >= 1.0f - float.Epsilon) {
			stDirection = stDirection.normalized;
		}

		this.transform.position += stDirection * 350.0f * a_fDeltaTime;

		// 마우스 버튼을 눌렀을 경우
		if(Input.GetMouseButton((int)EMouseBtn.LEFT)) {
			float fMouseX = Input.GetAxis("Mouse X");

			this.transform.Rotate(Vector3.up,
				fMouseX * 180.0f * a_fDeltaTime, Space.World);
		}
	}

	/** 애니메이션 상태를 갱신한다 */
	private void UpdateAnimationState(float a_fVertical,
		float a_fHorizontal, float a_fDeltaTime) {
		
		// 입력이 없을 경우
		if(a_fVertical.ExIsEquals(0.0f) && a_fHorizontal.ExIsEquals(0.0f)) {
			m_oAnimation.CrossFade("Idle");
			return;
		}

		// 전/후방으로 이동했을 경우
		if(!a_fVertical.ExIsEquals(0.0f)) {
			m_oAnimation.CrossFade((a_fVertical >= float.Epsilon) ? "RunF" : "RunB");
		}
	}
	#endregion // 함수
}

