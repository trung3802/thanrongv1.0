﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TienKiemV2Remastered.Application.Interfaces.Client;
using TienKiemV2Remastered.Application.Interfaces.Map;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.Application.Main;
using TienKiemV2Remastered.Application.Manager;
using TienKiemV2Remastered.Application.Threading;
using TienKiemV2Remastered.DatabaseManager.Player;
using TienKiemV2Remastered.Model;
using TienKiemV2Remastered.Application.Constants;
using TienKiemV2Remastered;

namespace TienKiemV2Remastered.Application.Constants
{
    public class Session_ME : ISession_ME
    {
        public static int BaseId { get; set; }
        public int Id { get; set; }

        private readonly string KEY = "boys";
        public bool IsDisconnected { get; set; }
        private bool IsGetKeyComplete { get; set; }
        private sbyte CurR { get; set; }
        private sbyte CurW { get; set; }
        private BinaryReader Reader { get; set; } //dis
        private BinaryWriter Writer { get; set; } //dos
        private TcpClient Client { get; set; }
        public Player Player { get; set; }
        private sbyte Type { get; set; }
        public sbyte ZoomLevel { get; set; }
        private bool IsGPS { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        private bool IsQwerty { get; set; }
        private bool IsTouch { get; set; }
        private string PlastForm { get; set; }
        public string Version { get; set; }
        private sbyte LanguageId { get; set; }
        private int Provider { get; set; }
        private string Agent { get; set; }
        public string IpV4 { get; set; }
        public bool IsLogin { get; set; }
        public IMessageHandler MessageHandler { get; set; }
        public MessageCollector HandlerMsg { get; set; }
        public Sender HandlerSender { get; set; }
        public bool IsNewVersion { get; set; }
        public int TimeConnected { get; set; }

        public Session_ME(TcpClient client, string ipV4)
        {
            Id = BaseId++;
            Client = client;
            var stream = client.GetStream();
            Reader = new BinaryReader(stream, new UTF8Encoding());
            Writer = new BinaryWriter(stream, new UTF8Encoding());
            MessageHandler = new Controller(this);
            IsDisconnected = false;
            IsGetKeyComplete = false;
            CurR = 0;
            CurW = 0;
            Type = 0;
            ZoomLevel = 2;
            IsQwerty = false;
            IsTouch = false;
            IsGPS = false;
            PlastForm = null;
            Version = null;
            LanguageId = 0;
            Provider = 0;
            Agent = null;
            IpV4 = ipV4;
            IsLogin = false;
            HandlerMsg = new MessageCollector(this);
            HandlerSender = new Sender(this);
            IsNewVersion = false;
            TimeConnected = ServerUtils.CurrentTimeSecond();
        }

        public void StartSession()
        {
            HandlerMsg.Start();
            HandlerSender.Start();
        }

        public bool IsConnected()
        {
            return !IsDisconnected;
        }

        public void SendMessage(Message message)
        {
            try
            {
                if (IsConnected() && message != null && HandlerSender != null)
                {
                    HandlerSender.AddMessage(message);
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error SendMessage in Session_ME.cs: {e.Message}\n{e.StackTrace}", e);
            }
        }

        private void DoSendMessage(Message message)
        {
            try
            {
                var cmd = message.Command;
                Writer.Write(IsGetKeyComplete ? WriteKey(cmd) : cmd);
                var data = message.GetData();
                if (data != null)
                {
                    var size = data.Length;
                    if (cmd is -32 or -66 or 11 or -67 or -74 or -87 or 66)
                    {
                        var sizeTemp = 0;
                        var a = size;
                        var b = 0;
                        var c = 0;
                        if (size > 256)
                        {
                            a = size % 256;
                            sizeTemp = (size - a) / 256;
                            if (sizeTemp <= 256)
                            {
                                b = sizeTemp;
                                c = 0;
                            }
                            else
                            {
                                b = sizeTemp % 256;
                                c = (sizeTemp - b) / 256;
                            }
                        }
                        Writer.Write(WriteKey((sbyte)(a - 128)));
                        Writer.Write(WriteKey((sbyte)(b - 128)));
                        Writer.Write(WriteKey((sbyte)(c - 128)));
                    }
                    else if (IsGetKeyComplete)
                    {
                        Writer.Write(WriteKey((sbyte)(size >> 8)));
                        Writer.Write(WriteKey((sbyte)(size & 0xFF)));
                    }
                    else
                    {
                        Writer.Write((sbyte)(size & 0xFF00));
                        Writer.Write((sbyte)(size & 0xFF));
                    }

                    if (IsGetKeyComplete)
                    {
                        for (var i = 0; i < size; ++i)
                        {
                            data[i] = WriteKey(data[i]);
                        }
                    }
                    Writer.Write(ServerUtils.ConvertSbyteToByte(data));
                }
                Writer.Flush();
            }
            catch (Exception)
            {
                CloseMessage();
            }

        }

        private sbyte ReadKey(sbyte b)
        {
            sbyte[] bytes = ServerUtils.ConvertArrayByteToSByte(Encoding.ASCII.GetBytes(KEY));
            sbyte i = (sbyte)((bytes[CurR++] & 0xFF) ^ (b & 0xFF));
            if (CurR >= (sbyte)bytes.Length)
            {
                CurR %= (sbyte)bytes.Length;
            }
            return i;
        }

        private sbyte WriteKey(sbyte b)
        {
            sbyte[] bytes = ServerUtils.ConvertArrayByteToSByte(Encoding.ASCII.GetBytes(KEY));
            sbyte i = (sbyte)((bytes[CurW++] & 0xFF) ^ (b & 0xFF));
            if (CurW >= bytes.Length)
            {
                CurW %= (sbyte)bytes.Length;
            }
            return i;
        }

        public void HansakeMessage()
        {
            try
            {
                var bytes = ServerUtils.ConvertArrayByteToSByte(Encoding.ASCII.GetBytes(KEY));
                var m = new Message((sbyte)-27);
                m.Writer.WriteByte(bytes.Length);
                m.Writer.WriteByte(bytes[0]);
                for (int i = 1; i < bytes.Length; i++)
                {
                    m.Writer.WriteByte(bytes[i] ^ bytes[i - 1]);
                }
                m.Writer.WriteUTF("");
                m.Writer.WriteInt(0);
                m.Writer.WriteByte(0);
                DoSendMessage(m);
                IsGetKeyComplete = true;
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error Hansake Message in Session_ME.cs: {e.StackTrace}", e);
            }
        }

        public void SetConnect(Message message)
        {
            try
            {
                Type = message.Reader.ReadByte();
                ZoomLevel = message.Reader.ReadByte();
                IsGPS = message.Reader.ReadBoolean();
                Width = message.Reader.ReadInt();
                Height = message.Reader.ReadInt();
                IsQwerty = message.Reader.ReadBoolean();
                IsTouch = message.Reader.ReadBoolean();
                PlastForm = message.Reader.ReadUTF();
            }
            catch (Exception)
            {
                CloseMessage();
            }
            finally
            {
                message?.CleanUp();
            }
        }

        public bool LoginGame(string c_username, string c_password, string c_version, sbyte c_type, Message message)
        {
            if (IsLogin) return false;
            try
            {

                var username = c_username;
                var password = c_password;
                Version = c_version;
                Type = c_type;
                if (int.Parse(c_version.Replace(".", "")) > 213)
                {
                    IsNewVersion = true;
                }
                var player = UserDB.Login(username, password);
                if (player != null)
                {
                    UserDB.UpdateLogin(player.Id, 1);
                    IsLogin = true;
                    Player = player;
                    Player.Session = this;

                    Server.Gi().Logger.Info($"Username: {username} - Login success version: {Version} ");
                    return true;
                }
                else
                {
                    IsLogin = false;
                    return false;
                }
            }
            catch (Exception e)
            {
                Server.Gi().Logger.Error($"Error Login in Session_ME.cs: {e.Message} \n {e.StackTrace}", e);
                IsLogin = false;
                return false;
            }
            finally
            {
                message?.CleanUp();
            }
        }

        public void Disconnect()
        {
            if (!IsConnected()) return;
            MessageHandler?.OnDisconnected(this, true);
            ClearNetwork();
            Server.Gi().Logger.Info($"Session: {Id} Disconnecd.!");
        }

        public void CloseMessage()
        {
            if (IsConnected())
            {
                ClientManager.Gi().KickSession(this);
                //Server.Gi().Logger.Print("Close message at line 326 in class Session_Me.cs", "manager");
            }
        }

        public void ClearNetwork()
        {
            IsDisconnected = true;
            IsLogin = false;
            Reader?.Close();
            Writer?.Close();
            Client?.Close();
            Player = null;
            MessageHandler = null;
            Dispose();
        }

        public class MessageCollector
        {
            private Task HandlerMessage { get; }
            public MessageCollector(Session_ME session)
            {
                //new Thread(new ThreadStart(() =>
                //{
                async void Action()
                {
                    if (session == null) return;
                    while (session.IsConnected())
                    {
                        try
                        {
                            if (session.Reader != null)
                            {
                                var msg = ReadMessage(session);
                                if (msg != null && session.MessageHandler != null)
                                {
                                    await session.MessageHandler.OnMessage(msg);
                                    await Task.Delay(1);
                                    continue;
                                }

                            }

                        }
                        catch (Exception e)
                        {
                            Server.Gi().Logger.Error($"Error Message Collector in Session_ME.cs: {e.Message}\n{e.StackTrace}", e);
                        }
                        session.CloseMessage();
                        session.Reader = null;
                    }
                }
                //})).Start();
                //async void Action()
                //{
                //    if (session == null) return;
                //    while (session.IsConnected())
                //    {
                //        try
                //        {
                //            if (session.Reader != null)
                //            {
                //                var message = ReadMessage(session);
                //                if (message != null && session.MessageHandler != null)
                //                {
                //                    await session.MessageHandler.OnMessage(message);
                //                    await Task.Delay(1);
                //                    continue;
                //                }
                //            }
                //        }
                //        catch (Exception e)
                //        {
                //            Server.Gi().Logger.Error($"Error Message Collector in Session_ME.cs: {e.Message}\n{e.StackTrace}", e);
                //        }

                //        session.CloseMessage();
                //        session.Reader = null;
                //    }
                //}

                HandlerMessage = new Task(Action);
            }

            public void Start()
            {
                HandlerMessage.Start();
            }

            private Message ReadMessage(Session_ME session)
            {
                try
                {
                    var cmd = session.Reader.ReadSByte();
                    if (cmd != -27)
                    {
                        cmd = session.ReadKey(cmd);
                    }
                    var size = 0;
                    if (cmd != -27)
                    {
                        var b1 = session.Reader.ReadSByte();
                        var b2 = session.Reader.ReadSByte();
                        size = (session.ReadKey(b1) & 0xFF) << 8 | session.ReadKey(b2) & 0xFF;
                    }
                    else
                    {
                        size = session.Reader.ReadUInt16();
                    }
                    // if (size > 2000) //500
                    // {
                    //     Server.Gi().Logger.Info($"IP co dau hieu bat thuong ----------- Ban IP: {session.IpV4}...");
                    //     FireWall.BanIp(session.IpV4);
                    //     return null;
                    // }
                    var data = new sbyte[size];
                    var src = session.Reader.ReadBytes(size);
                    Buffer.BlockCopy(src, 0, data, 0, size);
                    if (cmd == -27) return new Message(cmd, data);
                    for (var i = 0; i < data.Length; i++)
                    {
                        data[i] = session.ReadKey(data[i]);
                    }
                    return new Message(cmd, data);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public class Sender
        {
            // private readonly Queue<Message> _listMessage;
            private readonly ConcurrentQueue<Message> _listMessage;
            private readonly Task _handlerSender;

            public Sender(Session_ME session)
            {
                // _listMessage = new Queue<Message>();
                _listMessage = new ConcurrentQueue<Message>();
                //  new Thread(new ThreadStart(() =>
                async void Action()
                {
                    while (session.IsConnected())
                    {
                        try
                        {
                            while (_listMessage.Count > 0)
                            {
                                // session1.DoSendMessage(_listMessage.Dequeue());
                                Message messaged;
                                _listMessage.TryDequeue(out messaged);
                                session.DoSendMessage(messaged);
                                await Task.Delay(1);
                            }

                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        await Task.Delay(1);
                    }
                }

                _handlerSender = new Task(Action);
            }


            public void AddMessage(Message message)
            {
                lock (_listMessage)
                {
                    _listMessage.Enqueue(message);
                }
            }

            public void Start()
            {
                _handlerSender.Start();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}