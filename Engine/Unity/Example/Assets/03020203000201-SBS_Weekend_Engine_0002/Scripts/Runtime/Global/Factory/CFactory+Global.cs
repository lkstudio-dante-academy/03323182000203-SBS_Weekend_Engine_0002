using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 전역 팩토리 */
public static partial class CFactory
{
	#region 클래스 팩토리 함수
	/** 게임 객체를 생성한다 */
	public static GameObject CreateGameObj(string a_oName,
		GameObject a_oParent, bool a_bIsStayWorldStates = false)
	{

		return CFactory.CreateGameObj(a_oName,
			a_oParent, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static GameObject CreateGameObj(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldStates = false)
	{

		return CFactory.CreateGameObj(a_oName,
			a_oParent, a_stPos, Vector3.one, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static GameObject CreateGameObj(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle, bool a_bIsStayWorldStates = false)
	{

		var oGameObj = new GameObject(a_oName);

		oGameObj.transform.SetParent(a_oParent?.transform,
			a_bIsStayWorldStates);

		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localEulerAngles = a_stAngle;

		return oGameObj;
	}

	/** 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldStates = false)
	{

		return CFactory.CreateCloneGameObj(a_oName,
			a_oOrigin, a_oParent, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldStates = false)
	{

		return CFactory.CreateCloneGameObj(a_oName,
			a_oOrigin, a_oParent, a_stPos, Vector3.one, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static GameObject CreateCloneGameObj(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle, bool a_bIsStayWorldStates = false)
	{

		var oGameObj = GameObject.Instantiate(a_oOrigin,
			Vector3.zero, Quaternion.identity);

		oGameObj.transform.SetParent(a_oParent?.transform,
			a_bIsStayWorldStates);

		oGameObj.name = a_oName;

		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localEulerAngles = a_stAngle;

		return oGameObj;
	}
	#endregion // 클래스 팩토리 함수

	#region 클래스 제네릭 팩토리 함수
	/** 게임 객체를 생성한다 */
	public static T CreateGameObj<T>(string a_oName,
		GameObject a_oParent, bool a_bIsStayWorldStates = false) where T : Component
	{

		return CFactory.CreateGameObj<T>(a_oName,
			a_oParent, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static T CreateGameObj<T>(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldStates = false) where T : Component
	{

		return CFactory.CreateGameObj<T>(a_oName,
			a_oParent, a_stPos, Vector3.one, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static T CreateGameObj<T>(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle, bool a_bIsStayWorldStates = false) where T : Component
	{

		var oGameObj = CFactory.CreateGameObj(a_oName,
			a_oParent, a_stPos, a_stScale, a_stAngle, a_bIsStayWorldStates);

		return oGameObj.ExAddComponent<T>();
	}

	/** 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, bool a_bIsStayWorldStates = false) where T : Component
	{

		return CFactory.CreateCloneGameObj<T>(a_oName,
			a_oOrigin, a_oParent, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, bool a_bIsStayWorldStates = false) where T : Component
	{

		return CFactory.CreateCloneGameObj<T>(a_oName,
			a_oOrigin, a_oParent, a_stPos, Vector3.one, Vector3.zero, a_bIsStayWorldStates);
	}

	/** 게임 객체를 생성한다 */
	public static T CreateCloneGameObj<T>(string a_oName,
		GameObject a_oOrigin, GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle, bool a_bIsStayWorldStates = false) where T : Component
	{

		var oGameObj = CFactory.CreateCloneGameObj(a_oName,
			a_oOrigin, a_oParent, a_stPos, a_stScale, a_stAngle, a_bIsStayWorldStates);

		return oGameObj.GetComponentInChildren<T>();
	}
	#endregion // 클래스 제네릭 팩토리 함수
}
