using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/** Example 18 */
public class CE01Example_18 : CSceneManager
{
	/** 상태 */
	public enum EState
	{
		NONE = -1,
		PLAY,
		GAME_OVER,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	[SerializeField] private float m_fNonPlayerCreateDelay = 0.0f;

	private float m_fUpdateSkipTime = 0.0f;
	private EState m_eState = EState.PLAY;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oPlayer = null;
	[SerializeField] private GameObject m_oNonPlayerRoot = null;
	[SerializeField] private GameObject m_oOriginNonPlayer = null;
	#endregion // 변수

	#region 프로퍼티
	public CE01Player_18 Player { get; private set; } = null;
	public List<CE01NonPlayer_18> NonPlayerList { get; } = new List<CE01NonPlayer_18>();

	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_18;
	public CGameObjsPoolManager GameObjsPoolManager { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CE01DataStorage_18.Inst.Reset();

		this.GameObjsPoolManager = CFactory.CreateGameObj<CGameObjsPoolManager>("GameObjsPoolManager",
			this.gameObject);
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);
		m_fUpdateSkipTime += a_fDeltaTime;

		// NPC 생성이 불가능 할 경우
		if(m_eState != EState.PLAY ||
			m_fUpdateSkipTime.ExIsLess(m_fNonPlayerCreateDelay))
		{

			return;
		}

		m_fUpdateSkipTime -= m_fNonPlayerCreateDelay;
		var stPos = this.GetNonPlayerRandomPos();

		// 위치 계산에 실패했을 경우
		if(!stPos.ExIsValid())
		{
			return;
		}

		var oNonPlayer = CFactory.CreateCloneGameObj<CE01NonPlayer_18>("NonPlayer",
			m_oOriginNonPlayer, m_oNonPlayerRoot);

		oNonPlayer.transform.position = stPos;
		oNonPlayer.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
	}

	/** 플레이어가 사망했을 경우 */
	public void OnDeathPlayer()
	{
		// 결과 씬 출력이 불가능 할 경우
		if(m_eState == EState.GAME_OVER)
		{
			return;
		}

		m_eState = EState.GAME_OVER;
		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_EXAMPLE_19, false);
	}
	#endregion // 함수

	#region 접근 함수
	/** NPC 위치를 반환한다 */
	public Vector3 GetNonPlayerRandomPos()
	{
		int nTryTimes = 0;

		do
		{
			float fPosX = Random.Range(-1920.0f, 1920.0f);
			float fPosZ = Random.Range(-1920.0f, 1920.0f);

			var stPos = new Vector3(fPosX, -540.0f, fPosZ);
			int nAreaMask = 1 << NavMesh.GetAreaFromName("Walkable");

			bool bIsSuccess = NavMesh.SamplePosition(stPos,
				out NavMeshHit stNavMeshHit, float.MaxValue / 2.0f, nAreaMask);

			// 위치 계산에 실패했을 경우
			if(!bIsSuccess)
			{
				continue;
			}

			float fDistance = Vector3.Distance(stNavMeshHit.position,
				m_oPlayer.transform.position);

			// 플레이어와의 거리가 가까울 경우
			if(fDistance.ExIsLess(350.0f))
			{
				continue;
			}

			return stNavMeshHit.position;
		} while(nTryTimes++ < 10);

		return Vector3.positiveInfinity;
	}

	/** 플레이어를 변경한다 */
	public void SetPlayer(CE01Player_18 a_oPlayer)
	{
		this.Player = a_oPlayer;
	}

	/** NPC 를 추가한다 */
	public void AddNonPlayer(CE01NonPlayer_18 a_oNonPlayer)
	{
		this.NonPlayerList.ExAddVal(a_oNonPlayer);
	}

	/** NPC 를 제거한다 */
	public void RemoveNonPlayer(CE01NonPlayer_18 a_oNonPlayer)
	{
		this.NonPlayerList.Remove(a_oNonPlayer);
	}
	#endregion // 접근 함수
}
