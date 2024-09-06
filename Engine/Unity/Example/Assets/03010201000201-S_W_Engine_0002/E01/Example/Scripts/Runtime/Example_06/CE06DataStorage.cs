using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 데이터 저장소 */
public class CE06DataStorage : CSingleton<CE06DataStorage>
{
	#region 프로퍼티
	public int Score { get; set; } = 0;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 리셋한다 */
	public override void Reset()
	{
		base.Reset();
		this.Score = 0;
	}
	#endregion // 함수
}
