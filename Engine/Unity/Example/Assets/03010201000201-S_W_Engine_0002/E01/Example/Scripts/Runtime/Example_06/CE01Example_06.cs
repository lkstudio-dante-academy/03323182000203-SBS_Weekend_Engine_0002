using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 6 */
public class CE01Example_06 : CSceneManager
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
	[SerializeField] private float m_fInterval = 0.0f;
	[SerializeField] private float m_fJumpPower = 0.0f;
	[SerializeField] private float m_fMoveSpeed = 0.0f;

	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oScoreText = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oPlayer = null;

	[SerializeField] private GameObject m_oObstacleRoot = null;
	[SerializeField] private GameObject m_oOriginObstacle = null;

	private List<GameObject> m_oObstacleList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_06;
	public EState CurState { get; private set; } = EState.PLAY;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CE01DataStorage_06.Inst.Reset();

		this.UpdateUIsState();

		var oDispatcher = m_oPlayer.GetComponent<CTriggerDispatcher>();
		oDispatcher.EnterCallback = this.HandleOnTriggerEnter;
		oDispatcher.ExitCallback = this.HandleOnTriggerExit;
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		StartCoroutine(this.CoTryCreateObstacles());
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 게임 종료 상태 일 경우
		if(this.CurState == EState.GAME_OVER)
		{
			return;
		}

		for(int i = 0; i < m_oObstacleList.Count; ++i)
		{
			var stOffset = new Vector3(-m_fMoveSpeed * Time.deltaTime,
				0.0f, 0.0f);

			m_oObstacleList[i].transform.position += stOffset;
		}

		// 점프 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space))
		{
			var oRigidbody = m_oPlayer.GetComponent<Rigidbody>();
			oRigidbody.velocity = Vector3.zero;

			oRigidbody.AddForce(Vector3.up * m_fJumpPower,
				ForceMode.VelocityChange);
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		m_oScoreText.text = $"{CE01DataStorage_06.Inst.Score}";
	}

	/** 충돌 시작을 처리한다 */
	private void HandleOnTriggerEnter(CTriggerDispatcher a_oSender,
		Collider a_oCollider)
	{

		/*
		 * 게임 객체가 지니고 있는 태그 정보를 비교하고 싶다면 CompareTag
		 * 메서드를 활용하면 된다. (즉, CompareTag 메서드를 활용해서 태그를
		 * 비교 할 경우 내부적으로 불필요한 메모리가 생성되지 않기 때문에
		 * 가비지 컬렉션의 동작을 방지하는 것이 가능하다.)
		 */
		// 장애물과 충돌했을 경우
		if(a_oCollider.CompareTag("E06Obstacle"))
		{
			var oRigidbody = m_oPlayer.GetComponent<Rigidbody>();
			oRigidbody.useGravity = false;
			oRigidbody.constraints = RigidbodyConstraints.FreezeAll;

			this.CurState = EState.GAME_OVER;
			CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_EXAMPLE_07, false);
		}
	}

	/** 충돌 종료를 처리한다 */
	private void HandleOnTriggerExit(CTriggerDispatcher a_oSender,
		Collider a_oCollider)
	{

		// 안전 영역 일 경우
		if(a_oCollider.CompareTag("E06SafeArea"))
		{
			CE01DataStorage_06.Inst.Score += 1;
			this.UpdateUIsState();
		}
	}

	/** 장애물을 생성한다 */
	private IEnumerator CoTryCreateObstacles()
	{
		do
		{
			var oObstacle = Instantiate(m_oOriginObstacle,
				Vector3.zero, Quaternion.identity);

			oObstacle.transform.SetParent(m_oObstacleRoot.transform, false);

			oObstacle.transform.position = new Vector3(KDefine.G_DESIGN_SCREEN_WIDTH / 2.0f,
				0.0f, 0.0f);

			m_oObstacleList.Add(oObstacle);
			yield return new WaitForSeconds(m_fInterval);
		} while(this.CurState != EState.GAME_OVER);
	}
	#endregion // 함수
}
