using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#region 기본
/** 마우스 버튼 */
public enum EMouseBtn
{
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	[HideInInspector] MAX_VAL
}

/** 투영 */
public enum EProjection
{
	NONE = -1,
	_2D,
	_3D,
	[HideInInspector] MAX_VAL
}

/** 내비게이션 스택 이벤트 */
public enum ENavStackEvent
{
	NONE = -1,
	BACK_KEY_DOWN,
	[HideInInspector] MAX_VAL
}

/** 패킷 타입 */
public enum EPacketType
{
	NONE = -1,
	MATCHING,
	TOUCH_CELL,
	DISCONNECT,
	[HideInInspector] MAX_VAL
}

/** 리스트 래퍼 */
public class CListWrapper<T>
{
	#region 변수
	public List<T> m_oList = new List<T>();
	public List<T> m_oAddList = new List<T>();
	public List<T> m_oRemoveList = new List<T>();
	#endregion // 변수

	#region 함수
	/** 값을 클리어한다 */
	public void Clear()
	{
		m_oList?.Clear();
		m_oAddList?.Clear();
		m_oRemoveList?.Clear();
	}
	#endregion // 함수
}

/** 풀 리스트 래퍼 */
public class CPoolListWrapper
{
	#region 변수
	public List<GameObject> m_oActiveList = new List<GameObject>();
	public List<GameObject> m_oInactiveList = new List<GameObject>();
	#endregion // 변수

	#region 함수
	/** 값을 클리어한다 */
	public void Clear()
	{
		m_oActiveList?.Clear();
		m_oInactiveList?.Clear();
	}
	#endregion // 함수
}

/** 상태 갱신 인터페이스 */
public interface IUpdatable
{
	#region 인터페이스
	/** 상태를 갱신한다 */
	public void OnUpdate(float a_fDeltaTime);

	/** 상태를 갱신한다 */
	public void OnLateUpdate(float a_fDeltaTime);
	#endregion // 인터페이스
}

/** 인덱스 */
[JsonObject]
public struct STIdx
{
	public int m_nX;
	public int m_nY;
	public int m_nZ;

	#region operator
	/** 인덱스 -> 벡터로 변환한다 */
	public static implicit operator Vector3Int(STIdx a_stSender)
	{
		return new Vector3Int(a_stSender.m_nX, a_stSender.m_nY, a_stSender.m_nZ);
	}

	/** 벡터 -> 인덱스로 변환한다 */
	public static implicit operator STIdx(Vector3Int a_stSender)
	{
		return new STIdx(a_stSender.x, a_stSender.y, a_stSender.z);
	}
	#endregion // operator

	#region 함수
	/** 생성자 */
	public STIdx(int a_nX, int a_nY, int a_nZ)
	{
		m_nX = a_nX;
		m_nY = a_nY;
		m_nZ = a_nZ;
	}
	#endregion // 함수
};

/** 패킷 */
[JsonObject]
public class CPacket
{
	#region 변수
	[JsonProperty] private STIdx m_stIdx;
	#endregion // 변수

	#region 프로퍼티
	public int Number { get; set; } = 0;
	public EPacketType PacketType { get; set; } = EPacketType.NONE;
	[JsonIgnore] public Vector3Int Idx { get; set; }
	#endregion // 프로퍼티

	#region 함수
	/** 생성자 */
	public CPacket() : this(EPacketType.NONE, 0, Vector3Int.zero)
	{
		// Do Something
	}

	/** 생성자 */
	public CPacket(EPacketType a_ePacketType) : this(a_ePacketType, 0, Vector3Int.zero)
	{
		// Do Something
	}

	/** 생성자 */
	public CPacket(EPacketType a_ePacketType, int a_nNumber) : this(a_ePacketType, a_nNumber, Vector3Int.zero)
	{
		// Do Something
	}

	/** 생성자 */
	public CPacket(EPacketType a_ePacketType, Vector3Int a_stIdx) : this(a_ePacketType, 0, a_stIdx)
	{
		// Do Something
	}

	/** 생성자 */
	public CPacket(EPacketType a_ePacketType, int a_nNumber, Vector3Int a_stIdx)
	{
		this.PacketType = a_ePacketType;
		this.Number = a_nNumber;
		this.Idx = a_stIdx;
	}

	/** 직렬화 될 경우 */
	[OnSerializing]
	private void OnSerializingMethod(StreamingContext a_oSender)
	{
		m_stIdx = this.Idx;
	}

	/** 역직렬화되었을 경우 */
	[OnDeserialized]
	private void OnDeserializedMethod(StreamingContext a_oSender)
	{
		this.Idx = m_stIdx;
	}
	#endregion // 함수

	#region 접근 함수
	/** JSON 문자열을 반환한다 */
	public string GetJSONStr()
	{
		return JsonConvert.SerializeObject(this);
	}
	#endregion // 접근 함수

	#region 팩토리 함수
	/** 패킷을 생성한다 */
	public static CPacket MakePacket(string a_oJSONStr)
	{
		return JsonConvert.DeserializeObject<CPacket>(a_oJSONStr);
	}
	#endregion // 팩토리 함수
}
#endregion // 기본
