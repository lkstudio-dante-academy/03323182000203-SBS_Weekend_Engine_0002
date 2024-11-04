using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 */
public partial class CE01NetworkManager_24 : CSingleton<CE01NetworkManager_24>
{
	#region 변수
	private TcpClient m_oClient = null;
	private TcpListener m_oServer = null;

	private Queue<CPacket> m_oResponsePacketQueue = new Queue<CPacket>();

	private List<TcpClient> m_oClientList = new List<TcpClient>();
	private List<(TcpClient, TcpClient)> m_oMatchingInfoList = new List<(TcpClient, TcpClient)>();
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CScheduleManager.Inst.AddComponent(this);

#if UNITY_EDITOR
		m_oServer = new TcpListener(new IPEndPoint(IPAddress.Any, 18080));
		m_oServer.Start();
#endif // UNITY_EDITOR
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();

		m_oClient = new TcpClient();
		m_oClient.ConnectAsync(IPAddress.Parse("127.0.0.1"), 18080);

		m_oClientThread = new Thread(this.ClientMain);
		m_oClientThread.Start();
	}

	/** 제거되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		m_oClientThread.Abort();

#if UNITY_EDITOR
		m_oServer.Stop();
#endif // UNITY_EDITOR
	}

	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);

#if UNITY_EDITOR
		for(int i = 0; i < m_oClientList.Count; ++i)
		{
			this.HandleRequest(m_oClientList[i]);
		}
#endif // UNITY_EDITOR
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime)
	{
		base.OnLateUpdate(a_fDeltaTime);

		while(m_oResponsePacketQueue.Count >= 1)
		{
			this.HandleResponse(m_oResponsePacketQueue.Dequeue());
		}

#if UNITY_EDITOR
		// 연결 대기 중인 요청이 없을 경우
		if(!m_oServer.Server.Poll(0, SelectMode.SelectRead))
		{
			return;
		}

		var oClient = m_oServer.AcceptTcpClient();
		m_oClientList.Add(oClient);
#endif // UNITY_EDITOR
	}

	/** 패킷을 전송한다 */
	private void SendPacket(TcpClient a_oClient, CPacket a_oPacket)
	{
		string oJSONStr = a_oPacket.GetJSONStr();
		var oBytes = System.Text.Encoding.Default.GetBytes(oJSONStr);

		a_oClient.GetStream().Write(oBytes, 0, oBytes.Length);
	}

	/** 매칭 응답을 전송한다 */
	private void SendMatchingResponse((TcpClient, TcpClient) a_stMatchingInfo)
	{
		var oPacketA = new CPacket(EPacketType.MATCHING, 1);
		var oPacketB = new CPacket(EPacketType.MATCHING, 2);

		this.SendPacket(a_stMatchingInfo.Item1, oPacketA);
		this.SendPacket(a_stMatchingInfo.Item2, oPacketB);
	}

	/** 요청을 처리한다 */
	private void HandleRequest(TcpClient a_oClient)
	{
		bool bIsValidA = a_oClient != null;
		bool bIsValidB = bIsValidA && a_oClient.Client.Poll(0, SelectMode.SelectRead);

		// 요청 처리가 불가능 할 경우
		if(!bIsValidA || !bIsValidB)
		{
			return;
		}

		var oBytes = new byte[byte.MaxValue];
		int nNumBytes = a_oClient.GetStream().Read(oBytes, 0, oBytes.Length);

		// 연결이 종료되었을 경우
		if(nNumBytes <= 0)
		{
			var oPacket = new CPacket(EPacketType.DISCONNECT);
			var stMatchingInfo = this.FindMatchingInfo(a_oClient);

			this.SendPacket(stMatchingInfo.Item1, oPacket);
			this.SendPacket(stMatchingInfo.Item2, oPacket);

			m_oClientList.Remove(stMatchingInfo.Item1);
			m_oClientList.Remove(stMatchingInfo.Item2);

			m_oMatchingInfoList.Remove(stMatchingInfo);
		}
		else
		{
			string oJSONStr = System.Text.Encoding.Default.GetString(oBytes,
				0, nNumBytes);

			var oPacket = CPacket.MakePacket(oJSONStr);

			switch(oPacket.PacketType)
			{
				case EPacketType.MATCHING:
					this.HandleRequestMatching(a_oClient, oPacket);
					break;
				case EPacketType.TOUCH_CELL:
					this.HandleRequestTouchCell(a_oClient, oPacket);
					break;
			}
		}
	}

	/** 매칭 요청을 처리한다 */
	private void HandleRequestMatching(TcpClient a_oClient, CPacket a_oPacket)
	{
		bool bIsValid = this.TryFindMatchingInfo(out (TcpClient, TcpClient) stMatchingInfo);

		// 매칭이 성사되었을 경우
		if(bIsValid)
		{
			stMatchingInfo.Item2 = a_oClient;
			m_oMatchingInfoList[m_oMatchingInfoList.Count - 1] = stMatchingInfo;

			this.SendMatchingResponse(stMatchingInfo);
		}
		else
		{
			stMatchingInfo.Item1 = a_oClient;
			m_oMatchingInfoList.Add(stMatchingInfo);
		}
	}

	/** 셀 터치 요청을 처리한다 */
	private void HandleRequestTouchCell(TcpClient a_oClient, CPacket a_oPacket)
	{
		var stMatchingInfo = this.FindMatchingInfo(a_oClient);

		var oOtherClient = (stMatchingInfo.Item1 == a_oClient) ?
			stMatchingInfo.Item2 : stMatchingInfo.Item1;

		this.SendPacket(oOtherClient, a_oPacket);
	}

	/** 매칭 정보를 탐색한다 */
	private (TcpClient, TcpClient) FindMatchingInfo(TcpClient a_oClient)
	{
		for(int i = 0; i < m_oMatchingInfoList.Count; ++i)
		{
			// 매칭 정보가 존재 할 경우
			if(m_oMatchingInfoList[i].Item1 == a_oClient ||
				m_oMatchingInfoList[i].Item2 == a_oClient)
			{
				return m_oMatchingInfoList[i];
			}
		}

		return default;
	}

	/** 매칭 정보를 탐색한다 */
	private bool TryFindMatchingInfo(out (TcpClient, TcpClient) a_stOutMatchingInfo)
	{
		a_stOutMatchingInfo = default;

		// 첫 접속 클라이언트 일 경우
		if(m_oMatchingInfoList.Count <= 0)
		{
			return false;
		}

		a_stOutMatchingInfo = m_oMatchingInfoList.LastOrDefault();
		return a_stOutMatchingInfo.Item2 == null;
	}
	#endregion // 함수
}
