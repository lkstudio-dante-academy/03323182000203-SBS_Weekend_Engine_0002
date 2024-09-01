using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 이진 탐색 트리 */
public class CE21BinarySearchTree<T> where T : System.IComparable
{
	/** 순회 타입 */
	public enum EEnumType
	{
		NONE = -1,
		PRE,
		IN,
		POST,
		LEVEL,
		[HideInInspector] MAX_VAL
	}

	/** 노드 */
	private class CNode
	{
		public T m_tVal;
		public CNode m_oLChildNode;
		public CNode m_oRChildNode;
	}

	#region 변수
	private CNode m_oRoot = null;
	#endregion // 변수

	#region 프로퍼티
	public int NumVals { get; private set; } = 0;
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CE21BinarySearchTree()
	{
		// Do Something
	}

	/** 값을 추가한다 */
	public void AddVal(T a_tVal)
	{
		var oNewNode = this.CreateNode(a_tVal);

		// 루트가 없을 경우
		if(m_oRoot == null)
		{
			m_oRoot = oNewNode;
		}
		else
		{
			var oCurNode = m_oRoot;
			CNode oParentNode = null;

			while(oCurNode != null)
			{
				oParentNode = oCurNode;

				// 왼쪽 노드로 이동해야 될 경우
				if(a_tVal.CompareTo(oCurNode.m_tVal) < 0)
				{
					oCurNode = oCurNode.m_oLChildNode;
				}
				else
				{
					oCurNode = oCurNode.m_oRChildNode;
				}
			}

			// 왼쪽 노드로 추가 되어야 할 경우
			if(a_tVal.CompareTo(oParentNode.m_tVal) < 0)
			{
				oParentNode.m_oLChildNode = oNewNode;
			}
			else
			{
				oParentNode.m_oRChildNode = oNewNode;
			}
		}

		this.NumVals += 1;
	}

	/** 값을 제거한다 */
	public void RemoveVal(T a_tVal)
	{
		var oNode = this.FindNode(a_tVal, out CNode oParentNode);

		// 값 제거가 불가능 할 경우
		if(oNode == null)
		{
			return;
		}

		// 자식 노드가 모두 존재 할 경우
		if(oNode.m_oLChildNode != null && oNode.m_oRChildNode != null)
		{
			var oRemoveNode = oNode.m_oRChildNode;
			oParentNode = oNode;

			while(oRemoveNode.m_oLChildNode != null)
			{
				oParentNode = oRemoveNode;
				oRemoveNode = oRemoveNode.m_oLChildNode;
			}

			oNode.m_tVal = oRemoveNode.m_tVal;
			oNode = oRemoveNode;
		}

		var oChildNode = oNode.m_oLChildNode ?? oNode.m_oRChildNode;

		// 루트 노드 일 경우
		if(oParentNode == null)
		{
			m_oRoot = oChildNode;
			goto REMOVE_VAL_EXIT;
		}

		// 부모 노드의 왼쪽 자식 노드 일 경우
		if(oParentNode.m_oLChildNode == oNode)
		{
			oParentNode.m_oLChildNode = oChildNode;
		}
		else
		{
			oParentNode.m_oRChildNode = oChildNode;
		}

REMOVE_VAL_EXIT:
		this.NumVals -= 1;
	}

	/** 값을 순회한다 */
	public void EnumerateVals(EEnumType a_eType,
		System.Action<T> a_oCallback)
	{

		switch(a_eType)
		{
			case EEnumType.PRE:
				this.EnumerateValsByPre(m_oRoot, a_oCallback);
				break;
			case EEnumType.IN:
				this.EnumerateValsByIn(m_oRoot, a_oCallback);
				break;
			case EEnumType.POST:
				this.EnumerateValsByPost(m_oRoot, a_oCallback);
				break;
			case EEnumType.LEVEL:
				this.EnumerateValsByLevel(m_oRoot, a_oCallback);
				break;
		}
	}

	/** 전위 순회를 수행한다 */
	private void EnumerateValsByPre(CNode a_oNode,
		System.Action<T> a_oCallback)
	{

		// 순회가 불가능 할 경우
		if(a_oNode == null)
		{
			return;
		}

		a_oCallback?.Invoke(a_oNode.m_tVal);

		this.EnumerateValsByPre(a_oNode.m_oLChildNode, a_oCallback);
		this.EnumerateValsByPre(a_oNode.m_oRChildNode, a_oCallback);
	}

	/** 중위 순회를 수행한다 */
	private void EnumerateValsByIn(CNode a_oNode,
		System.Action<T> a_oCallback)
	{

		// 순회가 불가능 할 경우
		if(a_oNode == null)
		{
			return;
		}

		this.EnumerateValsByIn(a_oNode.m_oLChildNode, a_oCallback);

		a_oCallback?.Invoke(a_oNode.m_tVal);
		this.EnumerateValsByIn(a_oNode.m_oRChildNode, a_oCallback);
	}

	/** 후위 순회를 수행한다 */
	private void EnumerateValsByPost(CNode a_oNode,
		System.Action<T> a_oCallback)
	{

		// 순회가 불가능 할 경우
		if(a_oNode == null)
		{
			return;
		}

		this.EnumerateValsByPost(a_oNode.m_oLChildNode, a_oCallback);
		this.EnumerateValsByPost(a_oNode.m_oRChildNode, a_oCallback);

		a_oCallback?.Invoke(a_oNode.m_tVal);
	}

	/** 계층 순회를 수행한다 */
	private void EnumerateValsByLevel(CNode a_oNode,
		System.Action<T> a_oCallback)
	{

		// 순회가 불가능 할 경우
		if(a_oNode == null)
		{
			return;
		}

		var oNodeQueue = new CE21Queue<CNode>();
		oNodeQueue.Enqueue(a_oNode);

		while(oNodeQueue.NumVals >= 1)
		{
			var oNode = oNodeQueue.Dequeue();
			a_oCallback?.Invoke(oNode.m_tVal);

			// 왼쪽 자식 노드가 존재 할 경우
			if(oNode.m_oLChildNode != null)
			{
				oNodeQueue.Enqueue(oNode.m_oLChildNode);
			}

			// 오른쪽 자식 노드가 존재 할 경우
			if(oNode.m_oRChildNode != null)
			{
				oNodeQueue.Enqueue(oNode.m_oRChildNode);
			}
		}
	}

	/** 노드를 탐색한다 */
	private CNode FindNode(T a_tVal, out CNode a_oOutParentNode)
	{
		var oCurNode = m_oRoot;
		a_oOutParentNode = null;

		while(oCurNode != null)
		{
			int nResult = a_tVal.CompareTo(oCurNode.m_tVal);

			// 값이 동일 할 경우
			if(nResult == 0)
			{
				break;
			}

			a_oOutParentNode = oCurNode;

			// 왼쪽 노드로 이동해야 될 경우
			if(nResult < 0)
			{
				oCurNode = oCurNode.m_oLChildNode;
			}
			else
			{
				oCurNode = oCurNode.m_oRChildNode;
			}
		}

		return oCurNode;
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 노드를 생성한다 */
	private CNode CreateNode(T a_tVal)
	{
		return new CNode()
		{
			m_tVal = a_tVal
		};
	}
	#endregion // 팩토리 함수
}
