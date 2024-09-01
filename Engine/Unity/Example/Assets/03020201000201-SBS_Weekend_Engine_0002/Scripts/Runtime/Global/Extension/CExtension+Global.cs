using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** 확장 메서드 */
public static class CExtension
{
	#region 클래스 메서드
	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this float a_fSender)
	{
		return !float.IsNaN(a_fSender) &&
			!float.IsInfinity(a_fSender) && !float.IsNegativeInfinity(a_fSender);
	}

	/** 유효 여부를 검사한다 */
	public static bool ExIsValid(this Vector3 a_stSender)
	{
		return a_stSender.x.ExIsValid() &&
			a_stSender.y.ExIsValid() && a_stSender.z.ExIsValid();
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this float a_fSender, float a_fRhs)
	{
		return a_fSender >= a_fRhs - float.Epsilon &&
			a_fSender <= a_fRhs + float.Epsilon;
	}

	/** 미만 여부를 검사한다 */
	public static bool ExIsLess(this float a_fSender, float a_fRhs)
	{
		return a_fSender < a_fRhs - float.Epsilon;
	}

	/** 이하 여부를 검사한다 */
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs)
	{
		return a_fSender.ExIsLess(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	/** 초과 여부를 검사한다 */
	public static bool ExIsGreat(this float a_fSender, float a_fRhs)
	{
		return a_fSender > a_fRhs + float.Epsilon;
	}

	/** 이상 여부를 검사한다 */
	public static bool ExIsGreatEquals(this float a_fSender, float a_fRhs)
	{
		return a_fSender.ExIsGreat(a_fRhs) || a_fSender.ExIsEquals(a_fRhs);
	}

	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx(this int a_nSender)
	{
		return a_nSender >= 0;
	}

	/** 제거 여부를 검사한다 */
	public static bool ExIsDestroy(this CComponent a_oSender)
	{
		return a_oSender.IsDestroy || a_oSender.gameObject == null;
	}

	/** 앱 종료 여부를 검사한다 */
	public static bool ExIsQuitApp(this CSceneManager a_oSender)
	{
		return CSceneManager.IsQuitApp || !Application.isPlaying;
	}

	/** 상태 갱신 가능 여부를 검사한다 */
	public static bool ExIsEnableUpdate(this CComponent a_oSender)
	{
		return a_oSender.enabled && a_oSender.gameObject.activeInHierarchy;
	}

	/** 월드 위치를 반환한다 */
	public static Vector3 ExGetWorldPos(this PointerEventData a_oSender)
	{
		var stNormalizePos = a_oSender.ExGetNormalizePos();

		float fAspect = KDefine.DeviceScreenSize.x /
			KDefine.DeviceScreenSize.y;

		float fDesignScreenWidth = KDefine.G_DESIGN_SCREEN_HEIGHT *
			fAspect;

		float fWorldPosX = fDesignScreenWidth *
			stNormalizePos.x / 2.0f;

		float fWorldPosY = KDefine.G_DESIGN_SCREEN_HEIGHT *
			stNormalizePos.y / 2.0f;

		return new Vector3(fWorldPosX, fWorldPosY, 0.0f);
	}

	/** 로컬 위치를 반환한다 */
	public static Vector3 ExGetLocalPos(this PointerEventData a_oSender,
		GameObject a_oParent)
	{

		return a_oSender.ExGetWorldPos().ExToLocal(a_oParent);
	}

	/** 월드 => 로컬 공간으로 변환한다 */
	public static Vector3 ExToLocal(this Vector3 a_stSender,
	GameObject a_oParent, bool a_bIsCoord = true)
	{

		var stVec4 = new Vector4(a_stSender.x,
			a_stSender.y, a_stSender.z, a_bIsCoord ? 1.0f : 0.0f);

		return a_oParent.transform.worldToLocalMatrix * stVec4;
	}

	/** 로컬 => 월드 공간으로 변환한다 */
	public static Vector3 ExToWorld(this Vector3 a_stSender,
		GameObject a_oParent, bool a_bIsCoord = true)
	{

		var stVec4 = new Vector4(a_stSender.x,
			a_stSender.y, a_stSender.z, a_bIsCoord ? 1.0f : 0.0f);

		return a_oParent.transform.localToWorldMatrix * stVec4;
	}

	/** 정규 위치를 반환한다 */
	private static Vector3 ExGetNormalizePos(this PointerEventData a_oSender)
	{
		var stPos = a_oSender.position;

		float fNormalizePosX = (stPos.x * 2.0f / KDefine.DeviceScreenSize.x) - 1.0f;
		float fNormalizePosY = (stPos.y * 2.0f / KDefine.DeviceScreenSize.y) - 1.0f;

		return new Vector3(fNormalizePosX, fNormalizePosY, 0.0f);
	}
	#endregion // 클래스 메서드

	#region 제네릭 클래스 메서드
	/** 인덱스 유효 여부를 검사한다 */
	public static bool ExIsValidIdx<T>(this List<T> a_oSender, int a_nIdx)
	{
		return a_nIdx >= 0 && a_nIdx < a_oSender.Count;
	}

	/** 값을 추가한다 */
	public static void ExAddVal<T>(this List<T> a_oSender, T a_tVal)
	{
		// 값이 존재 할 경우
		if(a_oSender.Contains(a_tVal))
		{
			return;
		}

		a_oSender.Add(a_tVal);
	}

	/** 값을 제거한다 */
	public static void ExRemoveValAt<T>(this List<T> a_oSender, int a_nIdx)
	{
		// 인덱스가 유효하지 않을 경우
		if(!a_oSender.ExIsValidIdx(a_nIdx))
		{
			return;
		}

		a_oSender.RemoveAt(a_nIdx);
	}

	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component
	{
		return a_oSender.GetComponent<T>() ?? a_oSender.AddComponent<T>();
	}

	/** 값을 복사한다 */
	public static void ExCopyTo<K, V>(this Dictionary<K, V> a_oSender,
		Dictionary<K, V> a_oDestDict, System.Func<K, V, V> a_oCallback)
	{

		foreach(var stKeyVal in a_oSender)
		{
			var tVal = a_oCallback(stKeyVal.Key, stKeyVal.Value);
			a_oDestDict.Add(stKeyVal.Key, tVal);
		}
	}
	#endregion // 제네릭 클래스 메서드
}
