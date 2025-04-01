using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 1 */
public class CE01Example_01 : CSceneManager
{
	#region ������Ƽ
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_01;
	#endregion // ������Ƽ

	#region �Լ�
	/** �ʱ�ȭ */
	public override void Awake()
	{
		base.Awake();
		Debug.Log("Hello, World!");
	}
	#endregion // �Լ�
}
