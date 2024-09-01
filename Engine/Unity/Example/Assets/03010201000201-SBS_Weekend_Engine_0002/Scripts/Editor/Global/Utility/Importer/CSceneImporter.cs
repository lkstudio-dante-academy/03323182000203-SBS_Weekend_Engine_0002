using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

/*
 * InitializeOnLoad 속성을 이용하면 특정 스크립트가 컴파일이 완료 된 후
 * 바로 동작 할 수 있도록 초기화 구문을 실행하는 것이 가능하다. (즉, 해당
 * 속성은 에디터 환경에서 동작하는 속성이라는 것을 알 수 있다.)
 */
/** 씬 추가자 */
[InitializeOnLoad]
public static class CSceneImporter
{
	#region 함수
	/** 정적 생성자 */
	static CSceneImporter()
	{
		/*
		 * EditorApplication.projectChanged 이벤트를 이용하면 프로젝트 뷰에
		 * 변화가 발생했을 때 해당 변화를 감지하는 것이 가능하다.
		 */
		EditorApplication.projectChanged -= HandleOnProjectChanged;
		EditorApplication.projectChanged += HandleOnProjectChanged;
	}

	/** 프로젝트 뷰 변경 상태를 처리한다 */
	private static void HandleOnProjectChanged()
	{
		/*
		 * AssetDatabase 클래스는 Unity 프로젝트 상에 존재하는 에셋을 관리하는
		 * 역할을 수행한다.
		 * 
		 * Unity 는 내부적으로 에셋을 관리하기 위해서 
		 * GUID (Global Unique Identifier) 사용하기 때문에 특정 에셋의 
		 * 경로나 이름이 바뀐다고하더라도 기존에 설정 되어있는 참조가 
		 * 깨지지 않는 특징이 존재한다. (즉, GUID 를 알고 있다면 특정 
		 * 에셋을 제어하는 것이 가능하다는 것을 알 수 있다.)
		 */
		var oGUIDs = AssetDatabase.FindAssets("Example", new string[]
		{
			"Assets/03010201000201-SBS_Weekend_Engine_0002/Scenes"
		});

		var oSceneList = new List<EditorBuildSettingsScene>();

		for(int i = 0; i < oGUIDs.Length; ++i)
		{
			string oPath = AssetDatabase.GUIDToAssetPath(oGUIDs[i]);

			var oBuildSettingsScene =
				new EditorBuildSettingsScene(oPath, true);

			oSceneList.Add(oBuildSettingsScene);
		}

		EditorBuildSettings.scenes = oSceneList.ToArray();
	}
	#endregion // 함수
}
#endif
