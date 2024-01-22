using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;

namespace TienKiemV2Remastered
{
	public class Socket_Client
	{
		private static void onMessage(string data)
		{
			try
			{
				Socket_Client.vMessage vMessage = JsonConvert.DeserializeObject<Socket_Client.vMessage>(data);
				switch (vMessage.cmd)
				{
					case 1: // send connection
						{
							Server.Gi().Logger.Print($"{vMessage.data.ToString()}", "manager");
							break;
						}
					case 2: // send player
						{
							Server.Gi().Logger.Print($"{vMessage.data.ToString()}", "manager");
							sendPlayer();
							break;
						}
					case 3: // send session 
						Server.Gi().Logger.Print($"{vMessage.data.ToString()}", "manager");
						sendSession();
						break;
				}
			}
			catch (Exception ex)
			{
				File.WriteAllText("bug.txt", ex.ToString());
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0009B45C File Offset: 0x0009965C
		
	static	void sendPlayer()
        {
			var players = ServerUtils.GetMoneys(ClientManager.Gi().Characters.Count) + ServerUtils.RandomNumber(2000, 2350);
			SendData(new vMessage()
			{
				cmd = 2,
				data = "Số người chơi Online: " + players
			}) ;
			Server.Gi().Logger.Print($"send player: " + players, "manager");
		}
		static void sendSession()
        {
			var Sessions = ServerUtils.GetMoneys(ClientManager.Gi().Sessions.Count) + ServerUtils.RandomNumber(2350, 2500); ;
			SendData(new vMessage()
			{
				cmd = 3,
				data = "Số Session: " + Sessions
			});
			Server.Gi().Logger.Print($"send sessions: " + Sessions, "manager");
		}
		// Token: 0x060009BB RID: 2491 RVA: 0x000062B9 File Offset: 0x000044B9
		public static void CreateSocket()
		{
			new Thread(delegate ()
			{
				try
				{
					bool connected = Socket_Client._clientSocket.Connected;
					if (!connected)
					{
						while (!Socket_Client._clientSocket.Connected)
						{
							try
							{
								Socket_Client._clientSocket.Connect(IPAddress.Loopback, 2007);
							}
							catch (SocketException)
							{
								Socket_Client._clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
								Thread.Sleep(5000);
							}
						}
						Socket_Client._clientSocket.BeginReceive(Socket_Client.receivedBuf, 0, Socket_Client.receivedBuf.Length, SocketFlags.None, new AsyncCallback(Socket_Client.ReceiveData), Socket_Client._clientSocket);
						Socket_Client.SendData(new Socket_Client.vMessage
						{
							cmd = 0,
							data = "Connect To Tien Kiem Server Success !"
						});
					}
				}
				catch (Exception ex)
				{
					File.WriteAllText("bug.txt", ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0009B518 File Offset: 0x00099718
		public static void ReceiveData(IAsyncResult ar)
		{
			Socket socket = (Socket)ar.AsyncState;
			bool connected = socket.Connected;
			if (connected)
			{
				int num = 0;
				try
				{
					num = socket.EndReceive(ar);
				}
				catch
				{
				}
				bool flag = num != 0;
				if (flag)
				{
					byte[] array = new byte[num];
					Array.Copy(Socket_Client.receivedBuf, array, num);
					Socket_Client.onMessage(Encoding.UTF8.GetString(array));
					Socket_Client._clientSocket.BeginReceive(Socket_Client.receivedBuf, 0, Socket_Client.receivedBuf.Length, SocketFlags.None, new AsyncCallback(Socket_Client.ReceiveData), Socket_Client._clientSocket);
					return;
				}
			}
			Server.Gi().Logger.Print("Disconnect Form Socket Server!", "manager");
		    Socket_Client.CreateSocket();
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000062EE File Offset: 0x000044EE
		

		// Token: 0x060009BE RID: 2494 RVA: 0x0009B5D8 File Offset: 0x000997D8
		public static void SendData(object data)
		{
			new Thread(delegate ()
			{
				try
				{
					byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
					Socket_Client._clientSocket.Send(bytes);
				}
				catch (Exception ex)
				{
					File.WriteAllText("bug.txt", ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x04001253 RID: 4691
		private static byte[] receivedBuf = new byte[2048];

		// Token: 0x04001254 RID: 4692
		public static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

		// Token: 0x04001255 RID: 4693
		public static bool Connect = true;

		// Token: 0x020000C3 RID: 195
		public class vMessage
		{
			// Token: 0x04001256 RID: 4694
			public int cmd;

			// Token: 0x04001257 RID: 4695
			public object data;
		}

		// Token: 0x020000C4 RID: 196
		
	}
}
