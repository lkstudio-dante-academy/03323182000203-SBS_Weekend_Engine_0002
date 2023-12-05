using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

/** 씬 추가자 */
[InitializeOnLoad]
public static class CSceneImporter {
	#region 함수
	/** 정적 생성자 */
	static CSceneImporter() {
		EditorApplication.projectChanged -= HandleOnProjectChanged;
		EditorApplication.projectChanged += HandleOnProjectChanged;
	}

	/** 프로젝트 뷰 변경 상태를 처리한다 */
	private static void HandleOnProjectChanged() {
		var oGUIDs = AssetDatabase.FindAssets("Example", new string[] {
			"Assets/Example/Scenes"
		});

		var oSceneList = new List<EditorBuildSettingsScene>();

		for(int i = 0; i < oGUIDs.Length; ++i) {
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
