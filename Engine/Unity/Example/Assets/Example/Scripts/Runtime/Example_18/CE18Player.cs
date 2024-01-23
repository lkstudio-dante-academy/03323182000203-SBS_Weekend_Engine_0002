using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 플레이어 */
public class CE18Player : CComponent {
	/** 무기 종류 */
	public enum EWeaponKinds {
		NONE = -1,
		RIFLE,
		SHOTGUN,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private EWeaponKinds m_eCurWeaponKinds = EWeaponKinds.RIFLE;
	
	private Animation m_oAnimation = null;
	private CharacterController m_oController = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oBulletRoot = null;
	[SerializeField] private List<GameObject> m_oWeaponList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public GameObject CurWeapon => m_oWeaponList[(int)m_eCurWeaponKinds];
	public GameObject CurMuzzleFlash => this.CurWeaponInfo.MuzzleFlash;
	
	public CE18WeaponInfo CurWeaponInfo => this.CurWeapon.GetComponent<CE18WeaponInfo>();
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

		m_oAnimation = this.GetComponentInChildren<Animation>();
		m_oController = this.GetComponentInChildren<CharacterController>();
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime) {
		base.OnUpdate(a_fDeltaTime);

		this.UpdateShootState(a_fDeltaTime);
		this.UpdateWeaponState(a_fDeltaTime);

		float fVertical = Input.GetAxis("Vertical");
		float fHorizontal = Input.GetAxis("Horizontal");

		this.UpdateTransformState(fVertical,
			fHorizontal, a_fDeltaTime);

		this.UpdateAnimationState(fVertical,
			fHorizontal, a_fDeltaTime);
	}

	/** 발사 상태를 갱신한다 */
	private void UpdateShootState(float a_fDeltaTime) {
		// 발사 키를 누른 상태가 아닐 경우
		if(!Input.GetKeyDown(KeyCode.Space)) {
			return;
		}
		 
		StopCoroutine("CoUpdateMuzzleFlashState");
		StartCoroutine(this.CoUpdateMuzzleFlashState());

		for(int i = 0; i < this.CurWeaponInfo.NumBulletsAtOnce; ++i) {
			var oBullet = this.CreateBullet();
			oBullet.transform.position = this.CurWeaponInfo.BulletSpawnPos.transform.position;

			var oDispatcher = oBullet.GetComponentInChildren<CCollisionDispatcher>();
			oDispatcher.EnterCallback = this.HandleOnCollisionEnter;

			switch(this.m_eCurWeaponKinds) {
				case EWeaponKinds.RIFLE:
					this.UpdateShootStateRifle(a_fDeltaTime, oBullet);
					break;

				case EWeaponKinds.SHOTGUN:
					this.UpdateShootStateShotgun(a_fDeltaTime, oBullet);
					break;
			}
		}
	}

	/** 소총 발사 상태를 갱신한다 */
	private void UpdateShootStateRifle(float a_fDeltaTime,
		CE18Bullet a_oBullet) {

		a_oBullet.transform.rotation = this.transform.rotation;
		a_oBullet.Shoot(this.transform.forward * this.CurWeaponInfo.ShootPower);
	}

	/** 샷건 발상 상태를 갱신한다 */
	private void UpdateShootStateShotgun(float a_fDeltaTime,
		CE18Bullet a_oBullet) {

		var stCenter = a_oBullet.transform.position + 
			(this.transform.forward * 15.0f);

		float fAngle = Random.Range(0.0f, 360.0f);

		var stRotation = Quaternion.AngleAxis(fAngle, 
			this.transform.forward);
		
		var stDirection = stRotation * this.transform.right;
		stDirection *= Random.Range(0.0f, 7.5f);

		var stShootPos = stCenter + stDirection;
		var stShootDirection = stShootPos - a_oBullet.transform.position;

		a_oBullet.transform.forward = stShootDirection.normalized;
		a_oBullet.Shoot(stShootDirection.normalized * this.CurWeaponInfo.ShootPower);
	}

	/** 무기 상태를 갱신한다 */
	private void UpdateWeaponState(float a_fDeltaTime) {
		// 소총 장착 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			m_eCurWeaponKinds = EWeaponKinds.RIFLE;
		}
		// 샷건 장착 키를 눌렀을 경우
		else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			m_eCurWeaponKinds = EWeaponKinds.SHOTGUN;
		}

		for(int i = 0; i < m_oWeaponList.Count; ++i) {
			m_oWeaponList[i].SetActive(i == (int)m_eCurWeaponKinds);
		}
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

		m_oController.Move(stDirection * 350.0f * a_fDeltaTime);

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
			m_oAnimation.CrossFade((a_fVertical >= float.Epsilon) ? 
				"RunF" : "RunB");
		}

		// 좌/우로 이동했을 경우
		if(!a_fHorizontal.ExIsEquals(0.0f)) {
			m_oAnimation.CrossFade((a_fHorizontal >= float.Epsilon) ?
				"RunR" : "RunL");
		}
	}

	/** 충돌 시작을 처리한다 */
	private void HandleOnCollisionEnter(CCollisionDispatcher a_oSender,
		Collision a_oCollision) {

		int nBulletLayer = LayerMask.NameToLayer("E18Bullet");

		var oBullet = (a_oSender.gameObject.layer == nBulletLayer) ?
			a_oSender.gameObject : a_oCollision.gameObject;

		var oSceneManager = CSceneManager.GetSceneManager<CExample_18>(KDefine.G_SCENE_N_EXAMPLE_18);
		oSceneManager.GameObjsPoolManager.DespawnGameObj(typeof(CE18Bullet).ToString(), oBullet);
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 총알을 생성한다 */
	private CE18Bullet CreateBullet() {
		var oSceneManager = CSceneManager.GetSceneManager<CExample_18>(KDefine.G_SCENE_N_EXAMPLE_18);

		var oBullet = oSceneManager.GameObjsPoolManager.SpawnGameObj(typeof(CE18Bullet).ToString(), () => {
			return CFactory.CreateCloneGameObj("Bullet",
				this.CurWeaponInfo.OriginBullet, m_oBulletRoot);
		});

		return oBullet.GetComponentInChildren<CE18Bullet>();
	}
	#endregion // 팩토리 함수

	#region 코루틴 함수
	/** 총구 화염 상태를 갱신한다 */
	private IEnumerator CoUpdateMuzzleFlashState() {
		int nOffsetX = Random.Range(0, 2);
		int nOffsetY = Random.Range(0, 2);

		var stOffset = new Vector2(nOffsetX * 0.5f, 
			nOffsetY * 0.5f);

		var oMaterial = this.CurWeaponInfo.MuzzleFlashMaterial;
		oMaterial.SetTextureOffset("_MainTex", stOffset);

		this.CurMuzzleFlash.SetActive(true);
		yield return new WaitForSeconds(0.05f);

		this.CurMuzzleFlash.SetActive(false);
	}
	#endregion // 코루틴 함수
}

