//#define E20_THREAD_01
//#define E20_THREAD_02
//#define E20_THREAD_03
#define E20_THREAD_04

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

/*
 * 쓰레드란?
 * - CPU 의 작업 시간을 할당받는 단위를 의미한다. (즉, 프로그램은 구동이 되었을
 * 운영체제에 의해 자동으로 쓰레드가 부여되며 해당 쓰레드를 메인 쓰레드라고 한다.)
 * 
 * 운영체제는 필요에 의해 쓰레드를 생성하는 방법을 하며 해당 방법을 사용하면 CPU 가
 * 지니고 있는 코어를 최대한 활용해서 성능 좋은 프로그램을 제작하는 것이 가능하다.
 * 
 * 단, Unity 는 멀티 쓰레드에 안전하지 않기 때문에 메인 쓰레드가 아닌 서브 쓰레드에서
 * Unity 와 관련 된 기능을 사용 할 경우 내부적으로 에러가 발생하는 단점이 존재한다.
 * 
 * 태스크란?
 * - 쓰레드를 이용한 비동기 작업을 제어 할 수 있는 기능을 의미한다. (즉, 태스크를
 * 활용하면 쓰레드를 좀더 안전하게 제어하는 것이 가능하다.)
 */
/** Example 20 */
public class CE01Example_20 : CSceneManager
{
	#region 변수
	private int m_nCount = 0;
	private object m_oLock = new object();
	private Stopwatch m_oStopwatch = new Stopwatch();

	private Thread m_oThreadA = null;
	private Thread m_oThreadB = null;

	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oThreadAText = null;
	[SerializeField] private Text m_oThreadBText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_20;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

#if E20_THREAD_01 || E20_THREAD_02 || E20_THREAD_04
		/*
		 * 쓰레드는 진입 메서드를 요구하기 때문에 반드시 특정 쓰레드를 생성하기
		 * 위해서 해당 쓰레드가 가장 처음 실행 할 메서드를 입력으로 전달 할 필요가
		 * 있다. (즉, C# 의 메인 함수는 메인 쓰레드가 진입하기 위한 함수라는 것을
		 * 알 수 있다.)
		 */
		m_oThreadA = new Thread(this.ThreadAMain);
		m_oThreadB = new Thread(this.ThreadBMain);

		/*
		 * C# 의 쓰레드는 처음 생성되었을 때는 대기 상태에 있기 때문에 Start 메서드를
		 * 호출해야지만 실행 상태로 전환되는 특징이 존재한다.
		 */
		m_oThreadA.Start();
		m_oThreadB.Start();

		/*
		 * Join 메서드는 특정 쓰레드가 종료 될 때까지 대기하는 역할을 수행한다. (즉,
		 * 해당 메서드를 활용하면 서브 쓰레드가 모두 종료 될 때까지 메인 쓰레드를
		 * 대기 시키는 것이 가능하다.)
		 */
		m_oThreadA.Join();
		m_oThreadB.Join();

#if E20_THREAD_02
		Debug.Log($"Thread 실행 결과 : {m_nCount}");
#endif
#endif
	}

#if E20_THREAD_01
	/** 쓰레드 A 진입 함수 */
	private void ThreadAMain() {
		for(int i = 0; i < 100000; ++i) {
			Debug.Log($"Thread A : {i + 1}");
		}
	}

	/** 쓰레드 B 진입 함수 */
	private void ThreadBMain() {
		for(int i = 0; i < 100000; ++i) {
			Debug.Log($"Thread B : {i + 1}");
		}
	}
#elif E20_THREAD_02
	/** 쓰레드 A 진입 함수 */
	private void ThreadAMain() {
		/*
		 * lock 키워드는 프로그램의 특정 영역에 대한 실행 여부를 제한하는 역할을
		 * 수행한다. (즉, 해당 키워드를 활용하면 쓰레드 간에 발생하는 데이터 경합
		 * 문제를 방지하는 것이 가능하다.)
		 * 
		 * 단, lock 키워드를 빈번하게 사용 할 경우 프로그램의 성능이 저하되기 
		 * 때문에 가능하면 최소한의 횟수로 멀티 쓰레드 프로그램 명령문을 작성하는
		 * 것이 중요하다.
		 */
		lock(m_oLock) {
			for(int i = 0; i < 10000000; ++i) {
				m_nCount += 1;
			}
		}
	}

	/** 쓰레드 B 진입 함수 */
	private void ThreadBMain() {
		lock(m_oLock) {
			for(int i = 0; i < 10000000; ++i) {
				m_nCount += 1;
			}
		}
	}
#elif E20_THREAD_03
	/** 메인 쓰레드 버튼을 눌렀을 경우 */
	public void OnTouchMainThreadBtn() {
		m_nCount = 0;
		m_oStopwatch.Restart();

		for(int i = 0; i < 1000000000; ++i) {
			m_nCount += 1;
		}

		UnityEngine.Debug.Log($"Main Thread 결과 : {m_nCount}, {m_oStopwatch.Elapsed}");
	}

	/** 태스크 버튼을 눌렀을 경우 */
	public void OnTouchTaskBtn() {
		m_nCount = 0;
		var oTaskList = new List<Task<int>>();

		m_oStopwatch.Restart();

		for(int i = 0; i < 10; ++i) {
			var oTask = Task.Run(this.TaskAsyncOperation);
			oTaskList.Add(oTask);
		}

		for(int i = 0; i < oTaskList.Count; ++i) {
			m_nCount += oTaskList[i].Result;
		}

		UnityEngine.Debug.Log($"Task 결과 : {m_nCount}, {m_oStopwatch.Elapsed}");
	}

	/** 비동기 작업을 수행한다 */
	private int TaskAsyncOperation() {
		int nCount = 0;

		for(int i = 0; i < 100000000; ++i) {
			nCount += 1;
		}

		return nCount;
	}
#elif E20_THREAD_04
	/** 쓰레드 A 진입 함수 */
	private void ThreadAMain()
	{
		lock(m_oLock)
		{
			for(int i = 0; i < 100000; ++i)
			{
				m_nCount += 1;
			}
		}

		m_oThreadAText.text = $"{m_nCount}";
	}

	/** 쓰레드 B 진입 함수 */
	private void ThreadBMain()
	{
		lock(m_oLock)
		{
			for(int i = 0; i < 100000; ++i)
			{
				m_nCount += 1;
			}
		}

		m_oThreadBText.text = $"{m_nCount}";
	}
#endif
	#endregion // 함수
}
