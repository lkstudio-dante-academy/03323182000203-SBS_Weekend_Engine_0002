using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 무기 정보 */
public class CE01WeaponInfo_18 : CComponent
{
	#region 변수
	[Header("=====> Etc <=====")]
	[SerializeField] private int m_nNumBulletsAtOnce = 0;
	[SerializeField] private float m_fShootPower = 0.0f;
	[SerializeField] private MeshRenderer m_oMuzzleFlashRenderer = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oMuzzleFlash = null;
	[SerializeField] private GameObject m_oOriginBullet = null;
	[SerializeField] private GameObject m_oBulletSpawnPos = null;
	#endregion // 변수

	#region 프로퍼티
	public int NumBulletsAtOnce => m_nNumBulletsAtOnce;
	public float ShootPower => m_fShootPower;

	public Material MuzzleFlashMaterial => m_oMuzzleFlashRenderer.material;

	public GameObject MuzzleFlash => m_oMuzzleFlash;
	public GameObject OriginBullet => m_oOriginBullet;
	public GameObject BulletSpawnPos => m_oBulletSpawnPos;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		m_oMuzzleFlash.SetActive(false);
	}
	#endregion // 함수
}
