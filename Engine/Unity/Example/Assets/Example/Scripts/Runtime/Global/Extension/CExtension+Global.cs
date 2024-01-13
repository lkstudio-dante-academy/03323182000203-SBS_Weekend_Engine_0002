using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 확장 메서드 */
public static class CExtension {
	#region 클래스 메서드
	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this float a_fSender, float a_fRhs) {
		return a_fSender >= a_fRhs - float.Epsilon &&
			a_fSender <= a_fRhs + float.Epsilon;
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this int a_nSender) {
		return a_nSender >= 0;
	}

	/** 제거 여부를 검사한다 */
	public static bool ExIsDestroy(this CComponent a_oSender) {
		return a_oSender.IsDestroy || a_oSender.gameObject == null;
	}

	/** 앱 종료 여부를 검사한다 */
	public static bool ExIsQuitApp(this CSceneManager a_oSender) {
		return CSceneManager.IsQuitApp || !Application.isPlaying;
	}

	/** 상태 갱신 가능 여부를 검사한다 */
	public static bool ExIsEnableUpdate(this CComponent a_oSender) {
		return a_oSender.enabled && a_oSender.gameObject.activeInHierarchy;
	}
	#endregion // 클래스 메서드

	#region 제네릭 클래스 메서드
	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T> a_oSender, int a_nIdx) {
		return a_nIdx >= 0 && a_nIdx < a_oSender.Count;
	}

	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal) {
		// 값이 존재 할 경우
		if(a_oSender.Contains(a_tVal)) {
			return;
		}

		a_oSender.Add(a_tVal);
	}

	/** 값을 제거한다 */
	public static void ExRemoveValAt<T>(this List<T> a_oSender, int a_nIdx) {
		// 인덱스가 유효하지 않을 경우
		if(!a_oSender.ExIsValidIdx(a_nIdx)) {
			return;
		}

		a_oSender.RemoveAt(a_nIdx);
	}

	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component {
		return a_oSender.GetComponent<T>() ?? a_oSender.AddComponent<T>();
	}
	#endregion // 제네릭 클래스 메서드
}
