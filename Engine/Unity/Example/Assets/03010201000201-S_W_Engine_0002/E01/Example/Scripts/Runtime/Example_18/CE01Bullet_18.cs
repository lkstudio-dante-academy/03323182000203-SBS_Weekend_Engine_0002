using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 총알 */
public class CE01Bullet_18 : CComponent
{
	/** 매개 변수 */
	public record REParams
	{
		public Dictionary<CE01Player_18.EAbilityKinds, int> m_oAbilityValDict;
	}

	#region 변수
	private Rigidbody m_oRigidbody = null;
	private TrailRenderer m_oTrail = null;
	#endregion // 변수

	#region 프로퍼티
	public REParams Params { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oRigidbody = this.GetComponentInChildren<Rigidbody>();
		m_oTrail = this.GetComponentInChildren<TrailRenderer>();
	}

	/** 초기화 */
	public virtual void Init(REParams a_reParms)
	{
		this.Params = a_reParms;
	}

	/** 총알을 발사한다 */
	public void Shoot(Vector3 a_stForce)
	{
		m_oTrail.Clear();

		m_oRigidbody.velocity = Vector3.zero;
		m_oRigidbody.AddForce(a_stForce, ForceMode.VelocityChange);
	}
	#endregion // 함수

	#region 클래스 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static REParams MakeParams(Dictionary<CE01Player_18.EAbilityKinds, int> a_oAbilityValDict)
	{
		return new REParams()
		{
			m_oAbilityValDict = a_oAbilityValDict
		};
	}
	#endregion // 클래스 팩토리 함수
}
