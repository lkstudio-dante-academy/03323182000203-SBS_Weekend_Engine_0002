using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 - 클라이언트 */
public partial class CE24NetworkManager : CSingleton<CE24NetworkManager>
{
	#region 변수
	private Thread m_oClientThread = null;
	#endregion // 변수

	#region 함수
	/** 매칭 요청을 전송한다 */
	public void SendMatchingRequest()
	{
		var oPacket = new CPacket(EPacketType.MATCHING);
		this.SendPacket(m_oClient, oPacket);
	}

	/** 셀 터치 요청을 전송한다 */
	public void SendTouchCellRequest(int a_nNumber,
		Vector3Int a_stIdx)
	{

		var oPakcet = new CPacket(EPacketType.TOUCH_CELL,
			a_nNumber, a_stIdx);

		this.SendPacket(m_oClient, oPakcet);
	}

	/** 클라이언트 메인 메서드 */
	private void ClientMain()
	{
		var oBytes = new byte[byte.MaxValue];

		do
		{
			// 수신 패킷이 없을 경우
			if(!m_oClient.Client.Poll(0, SelectMode.SelectRead))
			{
				continue;
			}

			int nNumBytes = m_oClient.GetStream().Read(oBytes,
				0, oBytes.Length);

			// 연결이 종료되었을 경우
			if(nNumBytes <= 0)
			{
				break;
			}

			string oJSONStr = System.Text.Encoding.Default.GetString(oBytes,
				0, nNumBytes);

			var oPacket = CPacket.MakePacket(oJSONStr);
			m_oResponsePacketQueue.Enqueue(oPacket);
		} while(true);
	}

	/** 응답을 처리한다 */
	private void HandleResponse(CPacket a_oPacket)
	{
		switch(a_oPacket.PacketType)
		{
			case EPacketType.MATCHING:
				this.HandleResponseMatching(a_oPacket);
				break;
			case EPacketType.TOUCH_CELL:
				this.HandleResponseTouchCell(a_oPacket);
				break;
			case EPacketType.DISCONNECT:
				this.HandleResponseDisconnect(a_oPacket);
				break;
		}
	}

	/** 매칭 응답을 처리한다 */
	private void HandleResponseMatching(CPacket a_oPacket)
	{
		var oSceneManager = CSceneManager.GetSceneManager<CExample_24>(KDefine.G_SCENE_N_EXAMPLE_24);
		oSceneManager.OnReceiveMatchingResponse(a_oPacket);
	}

	/** 셀 터치 응답을 처리한다 */
	private void HandleResponseTouchCell(CPacket a_oPacket)
	{
		var oSceneManager = CSceneManager.GetSceneManager<CExample_24>(KDefine.G_SCENE_N_EXAMPLE_24);
		oSceneManager.OnReceiveTouchCellResponse(a_oPacket);
	}

	/** 연결 종료 응답을 처리한다 */
	private void HandleResponseDisconnect(CPacket a_oPacket)
	{
		var oSceneManager = CSceneManager.GetSceneManager<CExample_24>(KDefine.G_SCENE_N_EXAMPLE_24);
		oSceneManager.OnReceiveDisconnectResponse(a_oPacket);
	}
	#endregion // 함수
}
