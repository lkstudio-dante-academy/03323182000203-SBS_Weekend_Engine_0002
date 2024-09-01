//#define E22_SOCKET_01
#define E22_SOCKET_02

using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/** Example 22 */
public partial class CExample_22 : CSceneManager
{
	#region 변수
	private Thread m_oServerThread = null;
	private Thread m_oClientThread = null;

	private TcpListener m_oServer = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_EXAMPLE_22;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oClientThread = new Thread(this.ClientMain);
		m_oClientThread.Start();

#if UNITY_EDITOR
		m_oServerThread = new Thread(this.ServerMain);
		m_oServerThread.Start();
#endif
	}

	/** 제거되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

		m_oServerThread?.Abort();
		m_oClientThread?.Abort();
	}
	#endregion // 함수
}

#if E22_SOCKET_01
/** Example 22 */
public partial class CExample_22 : CSceneManager {
#region 함수
	/** 서버 쓰레드 메인 메서드 */
	private void ServerMain() {
		m_oServer = new TcpListener(new IPEndPoint(IPAddress.Any, 18080));
		m_oServer.Start();

		var oBytes = new byte[byte.MaxValue];
		var oClient = m_oServer.AcceptTcpClient();

		int nNumBytes = oClient.GetStream().Read(oBytes, 
			0, oBytes.Length);

		string oMsg = System.Text.Encoding.Default.GetString(oBytes, 
			0, nNumBytes);

		Debug.Log($"서버 수신 메세지: {oMsg}");
		oClient.GetStream().Write(oBytes, 0, nNumBytes);

		m_oServer.Stop();
	}

	/** 클라이언트 쓰레드 메인 메서드 */
	private void ClientMain() {
		var oSocket = new TcpClient();
		oSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18080));

		// 서버와의 연결에 실패했을 경우
		if(!oSocket.Connected) {
			return;
		}

		string oMsg = "Hello, World!";
		var oBytes = System.Text.Encoding.Default.GetBytes(oMsg);

		oSocket.GetStream().Write(oBytes, 0, oBytes.Length);
		oBytes = new byte[byte.MaxValue];

		int nNumBytes = oSocket.GetStream().Read(oBytes, 
			0, oBytes.Length);

		oMsg = System.Text.Encoding.Default.GetString(oBytes, 
			0, nNumBytes);
		
		Debug.Log($"클라이언트 수신 메세지: {oMsg}");

		oSocket.Client.Shutdown(SocketShutdown.Both);
		oSocket.Close();
	}
#endregion // 함수
}
#elif E22_SOCKET_02
/** Example 22 */
public partial class CExample_22 : CSceneManager
{
	#region 변수
	private int m_nPortNumber = 0;
	private Queue<string> m_oSendMsgQueue = new Queue<string>();
	private Queue<string> m_oReceiveMsgQueue = new Queue<string>();

	private List<TcpClient> m_oClientList = new List<TcpClient>();

	[Header("=====> UI <=====")]
	[SerializeField] private InputField m_oMsgInput = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oOriginText = null;
	[SerializeField] private GameObject m_oScrollViewContents = null;
	#endregion // 변수

	#region 함수
	/** 상태를 갱신한다 */
	public override void OnUpdate(float a_fDeltaTime)
	{
		base.OnUpdate(a_fDeltaTime);

		while(m_oReceiveMsgQueue.Count >= 1)
		{
			var oText = CFactory.CreateCloneGameObj<Text>("Text",
				m_oOriginText, m_oScrollViewContents);

			string oMsg = m_oReceiveMsgQueue.Dequeue();
			var oTokens = oMsg.Split(":");

			oText.text = oTokens[1];

			oText.color = (oTokens[0].Equals(m_nPortNumber.ToString())) ?
				Color.yellow : Color.white;
		}
	}

	/** 전송 버튼을 눌렀을 경우 */
	public void OnTouchSendBtn()
	{
		// 입력 된 텍스트가 없을 경우
		if(m_oMsgInput.text.Length <= 0)
		{
			return;
		}

		m_oSendMsgQueue.Enqueue(m_oMsgInput.text);
	}

	/** 서버 쓰레드 메인 메서드 */
	private void ServerMain()
	{
		m_oServer = new TcpListener(new IPEndPoint(IPAddress.Any, 18080));
		m_oServer.Start();

		var oBytes = new byte[byte.MaxValue];
		var oRemoveClientList = new List<TcpClient>();

		do
		{
			bool bIsTrue = m_oServer.Server.Poll(0,
				SelectMode.SelectRead);

			oRemoveClientList.Clear();

			// 연결 수락이 가능 할 경우
			if(bIsTrue)
			{
				var oClient = m_oServer.AcceptTcpClient();
				m_oClientList.Add(oClient);
			}

			for(int i = 0; i < m_oClientList.Count; ++i)
			{
				bIsTrue = m_oClientList[i].Client.Poll(0,
					SelectMode.SelectRead);

				// 수신 할 데이터가 없을 경우
				if(!bIsTrue)
				{
					continue;
				}

				int nNumBytes = m_oClientList[i].GetStream().Read(oBytes,
					0, oBytes.Length);

				// 접속이 종료 된 상태 일 경우
				if(nNumBytes <= 0)
				{
					oRemoveClientList.Add(m_oClientList[i]);
				}
				else
				{
					string oMsg = System.Text.Encoding.Default.GetString(oBytes,
						0, nNumBytes);

					int nPortNumber = (m_oClientList[i].Client.RemoteEndPoint as IPEndPoint).Port;

					oMsg = $"{nPortNumber}:{oMsg}";
					oBytes = System.Text.Encoding.Default.GetBytes(oMsg);

					this.SendServerData(oBytes, oBytes.Length);
				}
			}

			for(int i = 0; i < oRemoveClientList.Count; ++i)
			{
				m_oClientList.Remove(oRemoveClientList[i]);
			}
		} while(true);

		m_oServer.Stop();
	}

	/** 서버 데이터를 전송한다 */
	private void SendServerData(byte[] a_oBytes, int a_nNumBytes)
	{
		for(int i = 0; i < m_oClientList.Count; ++i)
		{
			m_oClientList[i].GetStream().Write(a_oBytes,
				0, a_nNumBytes);
		}
	}

	/** 클라이언트 쓰레드 메인 메서드 */
	private void ClientMain()
	{
		var oSocket = new TcpClient();
		oSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 18080));

		// 연결 요청이 실패했을 경우
		if(!oSocket.Connected)
		{
			return;
		}

		var oReadBytes = new byte[byte.MaxValue];
		m_nPortNumber = (oSocket.Client.LocalEndPoint as IPEndPoint).Port;

		do
		{
			while(m_oSendMsgQueue.Count >= 1)
			{
				string oMsg = m_oSendMsgQueue.Dequeue();
				var oWriteBytes = System.Text.Encoding.Default.GetBytes(oMsg);

				oSocket.GetStream().Write(oWriteBytes,
					0, oWriteBytes.Length);
			}

			// 수신 데이터가 존재 할 경우
			if(oSocket.Client.Poll(0, SelectMode.SelectRead))
			{
				int nNumBytes = oSocket.GetStream().Read(oReadBytes,
					0, oReadBytes.Length);

				string oMsg = System.Text.Encoding.Default.GetString(oReadBytes,
					0, nNumBytes);

				m_oReceiveMsgQueue.Enqueue(oMsg);
			}
		} while(true);

		oSocket.Client.Shutdown(SocketShutdown.Both);
		oSocket.Close();
	}
	#endregion // 함수
}
#endif