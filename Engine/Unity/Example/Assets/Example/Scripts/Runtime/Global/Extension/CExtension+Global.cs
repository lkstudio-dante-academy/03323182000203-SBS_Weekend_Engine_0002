using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 확장 메서드 */
public static class CExtension {
	#region 제네릭 클래스 메서드
	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component {
		return a_oSender.GetComponent<T>() ?? a_oSender.AddComponent<T>();
	}
	#endregion // 제네릭 클래스 메서드
}
