using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/** Example 24 */
public class CExample_24 : CSceneManager {
	#region 상수
	public const int NUM_CELLS = 3;
	public const float CELL_SIZE = 300.0f;
	#endregion // 상수

	#region 변수
	[Header("=====> UIs <=====")]
	[SerializeField] private Image m_oBGImg = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oLineRoot = null;
	[SerializeField] private GameObject m_oOriginLine = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_24;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CE24NetworkManager.Create();

		// 터치 전달자를 설정한다
		var oTouchDispatcher = m_oBGImg.GetComponent<CTouchDispatcher>();
		oTouchDispatcher.BeginCallback = this.HandleOnTouchBegin;
		oTouchDispatcher.EndCallback = this.HandleOnTouchEnd;
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
	
		var stPivotPos = new Vector3(NUM_CELLS / -2.0f * CELL_SIZE,
			NUM_CELLS / 2.0f * CELL_SIZE, 0.0f);

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

	/** 매칭 버튼을 눌렀을 경우 */
	public void OnTouchMatchingBtn() {
		CE24NetworkManager.Inst.SendMatchingRequest();
	}

	/** 터치 시작을 처리한다 */
	private void HandleOnTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {

		Debug.Log($"{a_oEventData.position}");
		Debug.Log($"{a_oEventData.pointerCurrentRaycast.worldPosition}");
		Debug.Log("");
	}

	/** 터치 종료를 처리한다 */
	private void HandleOnTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {

	}
	#endregion // 함수
}
