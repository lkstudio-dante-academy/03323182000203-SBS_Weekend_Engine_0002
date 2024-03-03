using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/** 터치 전달자 */
public class CTouchDispatcher : CComponent, IPointerDownHandler, IDragHandler, IPointerUpHandler {
	#region 프로퍼티
	public System.Action<CTouchDispatcher, PointerEventData> BeginCallback = null;
	public System.Action<CTouchDispatcher, PointerEventData> MoveCallback = null;
	public System.Action<CTouchDispatcher, PointerEventData> EndCallback = null;
	#endregion // 프로퍼티

	#region IPointerDownHandler
	/** 터치가 시작되었을 경우 */
	public virtual void OnPointerDown(PointerEventData a_oEventData) {
		this.BeginCallback?.Invoke(this, a_oEventData);
	}
	#endregion // IPointerDownHandler

	#region IDragHandler
	/** 터치가 움직였을 경우 */
	public virtual void OnDrag(PointerEventData a_oEventData) {
		this.MoveCallback?.Invoke(this, a_oEventData);
	}
	#endregion // IDragHandler

	#region IPointerUpHandler
	/** 터치가 종료되었을 경우 */
	public virtual void OnPointerUp(PointerEventData a_oEventData) {
		this.EndCallback?.Invoke(this, a_oEventData);
	}
	#endregion // IPointerUpHandler
}
