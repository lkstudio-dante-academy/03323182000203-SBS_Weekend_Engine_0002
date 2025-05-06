using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 10 */
public class CE01Example_10 : CSceneManager
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
	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oTimeText = null;
	[SerializeField] private Text m_oScoreText = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oTextRoot = null;
	[SerializeField] private GameObject m_oOriginText = null;

	private float m_fRemainTime = 10.0f;
	private EState m_eState = EState.PLAY;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_10;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CE01DataStorage_10.Inst.Reset();
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 플레이 상태가 아닐 경우
		if(m_eState != EState.PLAY)
		{
			return;
		}

		m_fRemainTime = Mathf.Max(0.0f, m_fRemainTime - Time.deltaTime);
		this.UpdateUIsState();

		// 남은 시간이 없을 경우
		if(m_fRemainTime <= float.Epsilon)
		{
			m_eState = EState.GAME_OVER;
			CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_EXAMPLE_11, false);
		}

		// 마우스 버튼을 눌렀을 경우
		if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT))
		{
			this.HandleOnMouseBtnDown();
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		m_oTimeText.text = $"{m_fRemainTime:0.00}";
		m_oScoreText.text = $"{CE01DataStorage_10.Inst.Score}";
	}

	/** 점수를 출력한다 */
	private void ShowScore(CE01Target_10 a_oTarget, int a_nScore)
	{
		var oText = Instantiate(m_oOriginText,
			Vector3.zero, Quaternion.identity);

		oText.transform.SetParent(m_oTextRoot.transform, false);
		oText.transform.position = a_oTarget.transform.position;

		oText.GetComponent<CE01UIText_10>().ShowText(a_nScore);
	}

	/** 마우스 버튼 입력을 처리한다 */
	private void HandleOnMouseBtnDown()
	{
		var stRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool bIsHit = Physics.Raycast(stRay, out RaycastHit stRaycastHit);

		// 클릭 된 대상이 없을 경우
		if(!bIsHit || !stRaycastHit.collider.TryGetComponent(out CE01Target_10 oTarget))
		{
			return;
		}

		bool bIsCatch = oTarget.TryCatch();

		// 두더지를 잡았을 경우
		if(bIsCatch)
		{
			int nScore = CE01DataStorage_10.Inst.Score;
			int nIncrScore = (oTarget.TargetType <= CE01Target_10.ETargetType.A) ? 10 : -20;

			this.ShowScore(oTarget, nIncrScore);
			CE01DataStorage_10.Inst.Score = Mathf.Max(0, nScore + nIncrScore);
		}
	}
	#endregion // 함수
}
