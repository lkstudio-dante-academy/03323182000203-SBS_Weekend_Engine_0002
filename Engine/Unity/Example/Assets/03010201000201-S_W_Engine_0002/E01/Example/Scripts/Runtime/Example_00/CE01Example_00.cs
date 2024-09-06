using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/** 메뉴 */
public class CE01Example_00 : CSceneManager
{
	#region 변수
	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oOriginText = null;
	[SerializeField] private GameObject m_oScrollViewContents = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_00;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		this.SetupScrollViewContents();
	}

	/** 스크롤 뷰 컨텐츠를 설정한다 */
	private void SetupScrollViewContents()
	{
		for(int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
		{
			string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);
			string oSceneName = Path.GetFileNameWithoutExtension(oScenePath);

			var oText = Instantiate(m_oOriginText,
					Vector3.zero, Quaternion.identity);

			oText.transform.SetParent(m_oScrollViewContents.transform,
				false);

			oText.GetComponent<Text>().text = oSceneName;

			int nIdx = i;

			/*
			 * 람다를 사용해서 람다 외부에 있는 변수를 캡처 할 경우
			 * 해당 변수가 실제 람다가 동작할 때 어떤 값을 지니는지
			 * 항상 주의 할 필요가 있다.
			 * 
			 * 즉, 람다가 작성 된 시점과 실제 람다가 동작하는 시점은
			 * 다르다는 것을 알 수 있다.
			 */
			oText.GetComponent<Button>().onClick.AddListener(() =>
				this.OnTouchTextBtn(nIdx));
		}
	}

	/** 텍스트 버튼을 눌렀을 경우 */
	private void OnTouchTextBtn(int a_nIdx)
	{
		string oScenePath = SceneUtility.GetScenePathByBuildIndex(a_nIdx);
		CSceneLoader.Inst.LoadScene(oScenePath);
	}
	#endregion // 함수
}
