using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 데이터 저장소 */
public class CE24DataStorage : CSingleton<CE24DataStorage>
{
	/** 결과 */
	public enum EResult
	{
		NONE = -1,
		WIN,
		LOSE,
		DRAW,
		[HideInInspector] MAX_VAL
	}

	#region 프로퍼티
	public EResult Result { get; set; } = EResult.NONE;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset()
	{
		base.Reset();
		this.Result = EResult.NONE;
	}
	#endregion // 함수

	#region 접근 함수
	/** 결과를 반환한다 */
	public string GetResult()
	{
		// 무승부 일 경우
		if(this.Result == EResult.DRAW)
		{
			return "무승부";
		}

		return (this.Result == EResult.WIN) ? "승리" : "패배";
	}
	#endregion // 접근 함수
}
