using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 연결 리스트 */
public class CE21LinkedList<T> {
	/** 노드 */
	private class CNode {
		public T m_tVal;

		public CNode m_oPrevNode;
		public CNode m_oNextNode;
	}

	#region 변수
	private CNode m_oHead = null;
	#endregion // 변수

	#region 프로퍼티
	public int NumVals { get; private set; } = 0;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CE21LinkedList() {
		// Do Something
	}

	/** 인덱서 */
	public T this[int a_nIdx] {
		get {
			var oNode = this.FindNodeAt(a_nIdx);
			return oNode.m_tVal;
		} set {
			var oNode = this.FindNodeAt(a_nIdx);
			oNode.m_tVal = value;
		}
	}

	/** 데이터를 추가한다 */
	public void AddVal(T a_tVal) {
		var oNewNode = this.CreateNode(a_tVal);

		// 헤드가 없을 경우
		if(m_oHead == null) {
			m_oHead = oNewNode;
		} else {
			var oTailNode = m_oHead;

			while(oTailNode.m_oNextNode != null) {
				oTailNode = oTailNode.m_oNextNode;
			}

			oTailNode.m_oNextNode = oNewNode;
			oNewNode.m_oPrevNode = oTailNode;
		}

		this.NumVals += 1;
	}

	/** 데이터를 추가한다 */
	public void InsertVal(int a_nIdx, T a_tVal) {
		Debug.Assert(a_nIdx >= 0 && a_nIdx <= this.NumVals);
		var oNewNode = this.CreateNode(a_tVal);

		// 헤드가 없을 경우
		if(m_oHead == null) {
			m_oHead = oNewNode;
			goto INSERT_VAL_EXIT;
		}

		var oNode = this.FindNodeAt(a_nIdx);
		var oPrevNode = oNode.m_oPrevNode;

		oNode.m_oPrevNode = oNewNode;

		oNewNode.m_oPrevNode = oPrevNode;
		oNewNode.m_oNextNode = oNode;

		// 헤드 노드 일 경우
		if(oNode == m_oHead) {
			m_oHead = oNewNode;
		} else {
			oPrevNode.m_oNextNode = oNewNode;
		}

INSERT_VAL_EXIT:
		this.NumVals += 1;
	}

	/** 데이터를 제거한다 */
	public void RemoveVal(T a_tVal) {
		var oNode = this.FindNode(a_tVal);

		// 데이터 제거가 불가능 할 경우
		if(oNode == null) {
			return;
		}

		this.RemoveNode(oNode);
	}

	/** 데이터를 제거한다 */
	public void RemoveValAt(int a_nIdx) {
		Debug.Assert(a_nIdx >= 0 && a_nIdx < this.NumVals);
		var oNode = this.FindNodeAt(a_nIdx);

		this.RemoveNode(oNode);
	}

	/** 노드를 제거한다 */
	private void RemoveNode(CNode a_oNode) {
		Debug.Assert(a_oNode != null);

		var oPrevNode = a_oNode.m_oPrevNode;
		var oNextNode = a_oNode.m_oNextNode;

		// 이전 노드가 존재 할 경우
		if(oPrevNode != null) {
			oPrevNode.m_oNextNode = oNextNode;
		} else {
			m_oHead = oNextNode;
		}

		// 다음 노드가 존재 할 경우
		if(oNextNode != null) {
			oNextNode.m_oPrevNode = oPrevNode;
		}

		this.NumVals -= 1;
	}

	/** 노드를 탐색한다 */
	private CNode FindNode(T a_tVal) {
		var oCurNode = m_oHead;

		for(int i = 0; i < this.NumVals; ++i) {
			// 값이 동일 할 경우
			if(oCurNode.m_tVal.Equals(a_tVal)) {
				return oCurNode;
			}

			oCurNode = oCurNode.m_oNextNode;
		}

		return null;
	}

	/** 노드를 탐색한다 */
	private CNode FindNodeAt(int a_nIdx) {
		var oCurNode = m_oHead;

		for(int i = 0; i < a_nIdx; ++i) {
			oCurNode = oCurNode.m_oNextNode;
		}

		return oCurNode;
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 노드를 생성한다 */
	private CNode CreateNode(T a_tVal) {
		return new CNode() {
			m_tVal = a_tVal
		};
	}
	#endregion // 팩토리 함수
}
