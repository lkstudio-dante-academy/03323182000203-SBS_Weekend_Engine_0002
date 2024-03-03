using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/** 네트워크 관리자 - 클라이언트 */
public partial class CE24NetworkManager : CSingleton<CE24NetworkManager> {
	#region 변수
	private Thread m_oClientThread = null;
	#endregion // 변수

	#region 함수
	/** 매칭 요청을 전송한다 */
	public void SendMatchingRequest() {
		var oPacket = new CPacket(EPacketType.MATCHING);
		this.SendPacket(m_oClient, oPacket);
	}

	/** 셀 터치 요청을 전송한다 */
	public void SendTouchCellRequest(int a_nNumber, 
		Vector3Int a_stIdx) {

		var oPakcet = new CPacket(EPacketType.TOUCH_CELL, 
			a_nNumber, a_stIdx);

		this.SendPacket(m_oClient, oPakcet);
	}

	/** 클라이언트 메인 메서드 */
	private void ClientMain() {
		var oBytes = new byte[byte.MaxValue];

		do {
			// 수신 패킷이 없을 경우
			if(!m_oClient.Client.Poll(0, SelectMode.SelectRead)) {
				continue;
			}

			m_oClient.GetStream().Read(oBytes, 0, oBytes.Length);
			string oJSONStr = System.Text.Encoding.Default.GetString(oBytes);

			var oPacket = CPacket.MakePacket(oJSONStr);

			switch(oPacket.PacketType) {
				case EPacketType.MATCHING: this.HandleResponseMatching(oPacket); break;
				case EPacketType.TOUCH_CELL: this.HandleResponseTouchCell(oPacket);  break;
			}
		} while(true);
	}

	/** 매칭 응답을 처리한다 */
	private void HandleResponseMatching(CPacket a_oPacket) {
		Debug.Log($"########: {a_oPacket.GetJSONStr()}");
	}

	/** 셀 터치 응답을 처리한다 */
	private void HandleResponseTouchCell(CPacket a_oPacket) {

	}
	#endregion // 함수
}
