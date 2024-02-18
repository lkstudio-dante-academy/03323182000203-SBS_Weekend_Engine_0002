//#define E21_ARRAY_LIST
//#define E21_LINKED_LIST
#define E21_STACK_QUEUE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 자료구조란?
 * - 다수의 데이터를 효율적으로 관리 할 수 있는 정형화 된 방법을 의미한다.
 * (즉, 자료구조를 활용하면 대량의 데이터를 좀 더 효율적으로 처리하는 것이
 * 가능하다.)
 * 
 * 자료구조 종류
 * - 선형 자료구조
 * - 비선형 자료구조
 * 
 * 선형 자료구조 종류
 * - 리스트 (배열, 연결)
 * - 스택 / 큐
 * 
 * 비선형 자료구조 종류
 * - 트리
 * - 해시 테이블
 * - 그래프
 * 
 * 리스트 자료구조란?
 * - 관리되는 데이터의 순서가 존재하는 자료구조를 의미한다. (즉, 해당 
 * 자료구조를 활용하면 데이터의 순차성을 보장하는 것이 가능하다.)
 * 
 * 리스트 자료구조는 내부적은 구현 방식에 따라 배열 리스트와 연결 리스트로
 * 구분된다.
 * 
 * 배열 리스트 자료구조란?
 * - 배열을 기반으로 데이터의 순차성을 보장하는 자료구조를 의미한다. (즉,
 * 내부적으로 배열을 사용하기 때문에 데이터의 접근이 굉장히 빠르다는 장점이
 * 존재한다는 것을 알 수 있다.)
 * 
 * 단, 특정 위치에 존재하는 데이터를 추가하거나 제거 할 경우 내부적으로 많은 
 * 데이터의 이동이 발생 할 수 있다는 단점이 존재한다. (즉, 빈번하게 데이터가 
 * 추가/제거 될 경우 성능이 저하 된다는 것을 알 수 있다.)
 * 
 * 연결 리스트 라료구조란?
 * - 참조를 기반으로 데이터의 순차성을 보장하는 자료구조를 의미한다. (즉,
 * 참조를 통해 데이터의 순서를 만들기 때문에 메모리의 물리적인 순서에 영향을
 * 받지 않는다는 것을 알 수 있다.)
 * 
 * 연결 리스트는 참조를 기반으로 데이터의 구조가 형성 되기 때문에 특정 위치에
 * 데이터를 추가하거나 제거 할 경우 배열 리스트와 데이터의 이동이 발생하지
 * 않는다는 장점이 존재한다.
 * 
 * 단, 연결 리스트는 임의 접근이 불가능하기 때문에 특정 데이터의 위치를 알고
 * 있다고 하더라도 항상 처음부터 차례대로 접근해야하는 단점이 존재한다. (즉,
 * 순차 접근만 가능하다는 것을 알 수 있다.)
 * 
 * 스택 자료구조란?
 * - LIFO (Last In First Out) 구조로 데이터의 순서를 제어하는 자료구조를
 * 의미한다.
 * 
 * 큐 자료구조란?
 * - FIFO (First In First Out) 구주로 데이터의 순서를 제어하는 자료구조를
 * 의미한다.
 * 
 * 즉, 스택과 큐 자료구조는 데이터의 입/출력 순서가 자료구조에 의해 제어되기
 * 때문에 특정 위치에 존재하는 데이터에 접근하는 것이 불가능하다.
 * 
 * 트리 자료구조란?
 * - 데이터 간에 부모/자식 관계를 형성 시킴으로서 계층적인 구조로 데이터를
 * 제어하는 자료구조를 의미한다. (즉, 해당 자료구조를 활용하면 계층적인
 * 형태를 표현해야되는 다양한 상황에 맞게 데이터를 제어하는 것이 가능하다.)
 * 
 * 트리 자료구조는 내부적인 구현 방식에 따라 배열과 연결 (참조) 을 통해서
 * 구현하는 것이 가능하다.
 * 
 * 단, 배열을 통해 트리를 구현 할 경우 자식의 최대 개수가 제한 되어있는
 * N-링크 구조만 구현하는 것이 가능하다. (즉, 연결을 통해 트리를 구현 할 경우
 * 자식의 개수가 정해져 있지 않은 트리를 구현하는 것도 가능하다는 것을 알 수
 * 있다.)
 */
/** Example 21 */
public class CExample_21 : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_21;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

#if E21_ARRAY_LIST
		var oValList = new CE21ArrayList<int>();

		for(int i = 0; i < 10; ++i) {
			oValList.AddVal(i + 1);
		}

		/*
		 * C# 의 문자열 자료형은 변경이 불가능한 데이터이기 때문에 특정
		 * 문자열을 결합하거나 수정 할 경우 내부적으로 새로운 문자열 데이터가
		 * 만들어진다는 특징이 존재한다.
		 * 
		 * 따라서, 여러 문자열을 빈번하게 결합 할 경우 내부적으로 임시적인
		 * 데이터가 만들어지는 것을 방지하기 위해서는 StringBuilder 를
		 * 사용하는 것을 추천한다. (즉, StringBuilder 는 내부적으로 임시
		 * 데이터를 생성하지 않는다는 것을 알 수 있다.)
		 */
		var oStrBuilder = new System.Text.StringBuilder();

		for(int i = 0; i < oValList.NumVals; ++i) {
			oStrBuilder.AppendFormat("{0}, ", oValList[i]);
		}

		Debug.Log(oStrBuilder.ToString());
#elif E21_LINKED_LIST
		var oValList = new CE21LinkedList<int>();

		for(int i = 0; i < 10; ++i) {
			oValList.AddVal(i + 1);
		}
		
		var oStrBuilder = new System.Text.StringBuilder();
		Debug.Log("=====> 리스트 요소 <=====");

		for(int i = 0; i < oValList.NumVals; ++i) {
			oStrBuilder.AppendFormat("{0}, ", oValList[i]);
		}

		Debug.Log(oStrBuilder.ToString());
		
		oValList.RemoveVal(0);
		oValList.RemoveValAt(0);

		Debug.Log("=====> 리스트 요소 - 제거 후 <=====");
		oStrBuilder.Clear();

		for(int i = 0; i < oValList.NumVals; ++i) {
			oStrBuilder.AppendFormat("{0}, ", oValList[i]);
		}

		Debug.Log(oStrBuilder.ToString());
#elif E21_STACK_QUEUE
		var oValStack = new CE21Stack<int>();
		var oValQueue = new CE21Queue<float>();

		for(int i = 0; i < 10; ++i) {
			oValStack.Push(i + 1);
			oValQueue.Enqueue(i + 1.0f);
		}

		var oStrBuilder = new System.Text.StringBuilder();
		Debug.Log("=====> 스택 요소 <=====");

		while(oValStack.NumVals >= 1) {
			oStrBuilder.AppendFormat("{0}, ", oValStack.Pop());
		}

		Debug.Log(oStrBuilder.ToString());
		
		oStrBuilder.Clear();
		Debug.Log("=====> 큐 요소 <=====");

		while(oValQueue.NumVals >= 1) {
			oStrBuilder.AppendFormat("{0}, ", oValQueue.Dequeue());
		}

		Debug.Log(oStrBuilder.ToString());
#endif
	}
	#endregion // 함수
}
