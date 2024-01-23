using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 총알 */
public class CE18Bullet : CComponent {
	#region 변수
	private Rigidbody m_oRigidbody = null;
	private TrailRenderer m_oTrail = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oRigidbody = this.GetComponentInChildren<Rigidbody>();
		m_oTrail = this.GetComponentInChildren<TrailRenderer>();
	}

	/** 총알을 발사한다 */
	public void Shoot(Vector3 a_stForce) {
		m_oTrail.Clear();

		m_oRigidbody.velocity = Vector3.zero;
		m_oRigidbody.AddForce(a_stForce, ForceMode.VelocityChange);
	}
	#endregion // 함수
}
