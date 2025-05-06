using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 씬 로더 */
public class CSceneLoader : CSingleton<CSceneLoader>
{
	#region 함수
	/** 씬을 로드한다 */
	public void LoadScene(string a_oSceneName, bool a_bIsSingle = true)
	{
		SceneManager.LoadScene(a_oSceneName,
			a_bIsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);
	}

	/** 씬을 비동기 로드한다 */
	public void LoadSceneAsync(string a_oSceneName,
		System.Action<CSceneLoader, AsyncOperation, bool> a_oCallback,
		bool a_bIsSingle = true)
	{

		/*
		 * StartCoroutine 메서드는 입력으로 전달 된 메서드를 코루틴 방식으로 동작
		 * 시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면 병렬 방식으로 처리되는
		 * 메서드를 구현하는 것이 가능하다.)
		 * 
		 * 코루틴이란?
		 * - 메서드가 실행 중에 return 키워드에 의해 호출이 종료 되었을 경우 다시 해당
		 * 위치부터 이어서 메서드를 실행 할 수 있는 기능을 의미한다. (즉, 일반적인
		 * 메서드는 서브루틴이라고 하며 서브루틴은 호출이 종료 된 메서드를 다시 호출
		 * 할 경우 항상 처음부터 실행되는 특징이 존재한다.)
		 * 
		 * 따라서, 코루틴의 특징을 활용하면 여러 작업을 병렬적으로 처리하는 것이 가능하다.
		 */
		StartCoroutine(this.CoLoadSceneAsync(a_oSceneName,
			a_oCallback, a_bIsSingle));
	}

	/** 씬을 비동기 로드한다 */
	private IEnumerator CoLoadSceneAsync(string a_oSceneName,
		System.Action<CSceneLoader, AsyncOperation, bool> a_oCallback,
		bool a_bIsSingle)
	{

		var oAsyncOperation = SceneManager.LoadSceneAsync(a_oSceneName,
			a_bIsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);

		do
		{
			/*
			 * yield return 키워드는 코루틴의 실행 흐름을 종료하는 역할을 수행한다.
			 * (즉, 서브루틴의 return 키워드와 비슷한 역할이라는 것을 알 수 있다.)
			 * 
			 * 단, 코루틴의 특징에 해당 키워드를 통해 코루틴이 종료 되었다하더라도
			 * 언제든지 해당 메서드의 호출을 통해 다시 이어서 코루틴의 흐름을 진행하는
			 * 것이 가능하다.
			 */
			yield return null;
			a_oCallback?.Invoke(this, oAsyncOperation, false);
		} while(!oAsyncOperation.isDone);

		a_oCallback?.Invoke(this, oAsyncOperation, true);
	}
	#endregion // 함수
}
