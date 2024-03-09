using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/** Example 24 */
public class CExample_24 : CSceneManager {
	/** 상태 */
	public enum EState {
		NONE = -1,
		MATCHING,
		PLAY,
		GAME_OVER,
		[HideInInspector] MAX_VAL
	}

	/** 셀 상태 */
	public enum ECellState {
		NONE = -1,
		EMPTY,
		PLAYER_01,
		PLAYER_02,
		[HideInInspector] MAX_VAL
	}

	#region 상수
	public const int NUM_CELLS = 3;
	public const float CELL_SIZE = 300.0f;
	#endregion // 상수

	#region 변수
	private bool m_bIsDirtyState = true;

	private int m_nPlayerNumber = 0;
	private int m_nTouchableNumber = 1;

	private EState m_eState = EState.MATCHING;

	private Bounds m_stGridBounds;
	private Vector3Int m_stTouchCellIdx = Vector3Int.zero;

	private ECellState[,] m_oCellStates = null;

	[Header("=====> UIs <=====")]
	[SerializeField] private Image m_oBGImg = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oMarkerRoot = null;
	[SerializeField] private GameObject m_oOriginMarker = null;

	[SerializeField] private GameObject m_oLineRoot = null;
	[SerializeField] private GameObject m_oOriginLine = null;

	[SerializeField] private List<GameObject> m_oMatchingObjList = new List<GameObject>();
	[SerializeField] private List<GameObject> m_oPlayObjList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_24;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CE24NetworkManager.Create();

		m_oCellStates = new ECellState[NUM_CELLS, NUM_CELLS];

		// 터치 전달자를 설정한다
		var oTouchDispatcher = m_oBGImg.GetComponent<CTouchDispatcher>();
		oTouchDispatcher.BeginCallback = this.HandleOnTouchBegin;
		oTouchDispatcher.EndCallback = this.HandleOnTouchEnd;
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		this.UpdateUIsState();

		var stPivotPos = this.GetPivotPos();

		m_stGridBounds = new Bounds(Vector3.zero,
			new Vector3(NUM_CELLS * CELL_SIZE, NUM_CELLS * CELL_SIZE, NUM_CELLS * CELL_SIZE));

		for(int i = 0; i <= NUM_CELLS; ++i) {
			var oLineV = CFactory.CreateCloneGameObj<LineRenderer>("LineV",
				m_oOriginLine, m_oLineRoot);

			var oLineH = CFactory.CreateCloneGameObj<LineRenderer>("LineH",
				m_oOriginLine, m_oLineRoot);

			var oPositionsV = new Vector3[2] {
				stPivotPos + new Vector3(i * CELL_SIZE, 0.0f, 0.0f),
				stPivotPos + new Vector3(i * CELL_SIZE, NUM_CELLS * -CELL_SIZE, 0.0f)
			};

			var oPositionsH = new Vector3[2] {
				stPivotPos + new Vector3(oLineH.startWidth / -2.0f, i * -CELL_SIZE, 0.0f),
				stPivotPos + new Vector3(oLineH.startWidth / 2.0f + NUM_CELLS * CELL_SIZE, i * -CELL_SIZE, 0.0f)
			};

			oLineV.positionCount = oPositionsV.Length;
			oLineH.positionCount = oPositionsH.Length;

			oLineV.SetPositions(oPositionsV);
			oLineH.SetPositions(oPositionsH);
		}
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime) {
		base.OnLateUpdate(a_fDeltaTime);

		// 상태 갱신이 필요 할 경우
		if(m_bIsDirtyState) {
			m_bIsDirtyState = false;
			this.UpdateUIsState();
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		for(int i = 0; i < m_oMatchingObjList.Count; ++i) {
			m_oMatchingObjList[i].SetActive(m_eState == EState.MATCHING);
		}

		for(int i = 0; i < m_oPlayObjList.Count; ++i) {
			m_oPlayObjList[i].SetActive(m_eState != EState.MATCHING);
		}
	}

	/** 매칭 버튼을 눌렀을 경우 */
	public void OnTouchMatchingBtn() {
		CE24NetworkManager.Inst.SendMatchingRequest();
	}

	/** 매칭 응답을 수신했을 경우 */
	public void OnReceiveMatchingResponse(CPacket a_oPacket) {
		m_eState = EState.PLAY;
		m_nPlayerNumber = a_oPacket.Number;

		this.SetIsDirtyState(true);
	}

	/** 셀 터치 응답을 수신했을 경우 */
	public void OnReceiveTouchCellResponse(CPacket a_oPacket) {
		var stIdx = a_oPacket.Idx;
		this.AddMarker(stIdx, a_oPacket.Number);

		m_nTouchableNumber = (m_nPlayerNumber == 1) ? 1 : 2;
		this.TryDetectResult();
	}

	/** 연결 종료 응답을 수신했을 경우 */
	public void OnReceiveDisconnectResponse(CPacket a_oPacket) {
		this.FinishPlay(CE24DataStorage.EResult.WIN);
	}

	/** 결과를 판정한다 */
	private void TryDetectResult() {
		bool bIsDraw = !this.FindCell(ECellState.EMPTY);

		bool bIsWinPlayer01 = this.GetResult(1);
		bool bIsWinPlayer02 = this.GetResult(2);

		bool bIsWin = (m_nPlayerNumber == 1 && bIsWinPlayer01) ||
			(m_nPlayerNumber == 2 && bIsWinPlayer02);

		bool bIsLose = (m_nPlayerNumber == 1 && bIsWinPlayer02) ||
			(m_nPlayerNumber == 2 && bIsWinPlayer01);

		var eResult = CE24DataStorage.EResult.NONE;

		// 승리했을 경우
		if(bIsWin) {
			eResult = CE24DataStorage.EResult.WIN;
		}
		// 패배했을 경우
		else if(bIsLose) {
			eResult = CE24DataStorage.EResult.LOSE;
		}
		// 무승부 일 경우
		else if(bIsDraw) {
			eResult = CE24DataStorage.EResult.DRAW;
		}

		this.FinishPlay(eResult);
	}

	/** 플레이를 종료한다 */
	private void FinishPlay(CE24DataStorage.EResult a_eResult) {
		// 플레이 종료가 불가능 할 경우
		if(a_eResult == CE24DataStorage.EResult.NONE) {
			return;
		}

		m_eState = EState.GAME_OVER;
		CE24DataStorage.Inst.Result = a_eResult;

		CSceneLoader.Inst.LoadScene(KDefine.G_SCENE_N_EXAMPLE_25);
	}

	/** 셀을 탐색한다 */
	private bool FindCell(ECellState a_eState) {
		for(int i = 0; i < NUM_CELLS; ++i) {
			for(int j = 0; j < NUM_CELLS; ++j) {
				// 셀이 존재 할 경우
				if(m_oCellStates[i, j] == a_eState) {
					return true;
				}
			}
		}

		return false;
	}

	/** 마커를 추가한다 */
	private void AddMarker(Vector3Int a_stIdx, int a_nNumber) {
		var oMarker = CFactory.CreateCloneGameObj<CE24Marker>("Marker",
			m_oOriginMarker, m_oMarkerRoot);

		oMarker.SetText((a_nNumber == m_nPlayerNumber) ? "O" : "X");
		oMarker.transform.localPosition = this.ConvertToCellPos(a_stIdx);

		m_oCellStates[a_stIdx.y, a_stIdx.x] = (ECellState)a_nNumber;
	}

	/** 터치 시작을 처리한다 */
	private void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {

		var stLocalPos = a_oEventData.ExGetLocalPos(this.Objs);

		// 셀 영역을 터치했을 경우
		if(m_stGridBounds.Contains(stLocalPos)) {
			m_stTouchCellIdx = this.ConvertToCellIdx(stLocalPos);
		} else {
			m_stTouchCellIdx = new Vector3Int(-1, -1, -1);
		}
	}

	/** 터치 종료를 처리한다 */
	private void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {

		var stLocalPos = a_oEventData.ExGetLocalPos(this.Objs);
		var stCellIdx = this.ConvertToCellIdx(stLocalPos);

		bool bIsValidA = m_stTouchCellIdx.Equals(stCellIdx);
		bool bIsValidB = m_stGridBounds.Contains(stLocalPos);
		bool bIsValidC = m_nPlayerNumber == m_nTouchableNumber;

		// 터치 시작 위치와 다를 경우
		if(!bIsValidA || !bIsValidB || !bIsValidC) {
			return;
		}

		var eCellState = m_oCellStates[stCellIdx.y, stCellIdx.x];

		// 셀 터치가 불가능 할 경우
		if(eCellState != ECellState.EMPTY) {
			return;
		}

		CE24NetworkManager.Inst.SendTouchCellRequest(m_nPlayerNumber,
			stCellIdx);

		this.AddMarker(stCellIdx, m_nPlayerNumber);
		m_nTouchableNumber = (m_nPlayerNumber == 1) ? 2 : 1;

		this.TryDetectResult();
	}

	/** 셀 인덱스 => 위치로 변환한다 */
	public Vector3 ConvertToCellPos(Vector3Int a_stIdx) {
		float fPosX = a_stIdx.x * CELL_SIZE + (CELL_SIZE / 2.0f);
		float fPosY = a_stIdx.y * -CELL_SIZE - (CELL_SIZE / 2.0f);

		return this.GetPivotPos() + new Vector3(fPosX, fPosY, 0.0f);
	}

	/** 위치 => 셀 인덱스로 변환한다 */
	public Vector3Int ConvertToCellIdx(Vector3 a_stPos) {
		var stPivotPos = this.GetPivotPos();

		var stDelta = a_stPos - stPivotPos;
		stDelta.y = -stDelta.y;

		return new Vector3Int((int)(stDelta.x / CELL_SIZE),
			(int)(stDelta.y / CELL_SIZE), 0);
	}
	#endregion // 함수

	#region 접근 함수
	/** 결과를 반환한다 */
	private bool GetResult(int a_nNumber) {
		int nNumEqualsA = 0;
		int nNumEqualsB = 0;

		for(int i = 0; i < NUM_CELLS; ++i) {
			nNumEqualsA = 0;
			nNumEqualsB = 0;

			for(int j = 0; j < NUM_CELLS; ++j) {
				nNumEqualsA += ((int)m_oCellStates[i, j] == a_nNumber) ? 1 : 0;
				nNumEqualsB += ((int)m_oCellStates[j, i] == a_nNumber) ? 1 : 0;
			}

			if(nNumEqualsA == NUM_CELLS || nNumEqualsB == NUM_CELLS) {
				return true;
			}
		}

		nNumEqualsA = 0;
		nNumEqualsB = 0;

		for(int i = 0; i < NUM_CELLS; ++i) {
			nNumEqualsA += ((int)m_oCellStates[i, i] == a_nNumber) ? 1 : 0;
			nNumEqualsB += ((int)m_oCellStates[i, NUM_CELLS - i - 1] == a_nNumber) ? 1 : 0;
		}

		return nNumEqualsA == NUM_CELLS || nNumEqualsB == NUM_CELLS;
	}

	/** 기준 위치를 반환한다 */
	public Vector3 GetPivotPos() {
		return new Vector3(NUM_CELLS / -2.0f * CELL_SIZE,
			NUM_CELLS / 2.0f * CELL_SIZE, 0.0f);
	}

	/** 상태 갱신 여부를 변경한다 */
	public void SetIsDirtyState(bool a_bIsDirty) {
		m_bIsDirtyState = m_bIsDirtyState ? 
			m_bIsDirtyState : a_bIsDirty;
	}
	#endregion // 접근 함수
}
