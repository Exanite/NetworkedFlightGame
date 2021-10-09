using System;
using System.Net;
using Cysharp.Threading.Tasks;
using LiteNetLib;
using LiteNetLib.Utils;

namespace Networking.Client
{
    public class UnityClient : UnityNetwork
    {
        private DisconnectInfo previousDisconnectInfo;

        public NetPeer Server { get; private set; }

        public bool IsConnecting { get; private set; }
        public bool IsConnected { get; private set; }
        public override bool IsReady => IsConnected;

        public event EventHandler<UnityClient, ConnectedEventArgs> Connected;
        public event EventHandler<UnityClient, DisconnectedEventArgs> Disconnected;

        private void OnDestroy()
        {
            Disconnect(false);
        }

        public async UniTask<ConnectResult> ConnectAsync(IPEndPoint endPoint)
        {
            if (IsConnected)
            {
                throw new InvalidOperationException("Client is already connected.");
            }

            if (IsConnecting)
            {
                throw new InvalidOperationException("Client is already connecting.");
            }

            IsConnecting = true;

            netManager.Start();
            netManager.Connect(endPoint, Constants.ConnectionKey);

            await UniTask.WaitUntil(() => !IsConnecting);

            return new ConnectResult(IsConnected, previousDisconnectInfo.Reason.ToString());
        }

        public void Disconnect()
        {
            Disconnect(true);
        }

        protected void Disconnect(bool pollEvents)
        {
            netManager.DisconnectAll();

            if (pollEvents)
            {
                netManager.PollEvents();
            }

            netManager.Stop();

            IsConnected = false;
            IsConnecting = false;
        }
        
        public void SendAsPacketHandlerToAll(IPacketHandler handler, NetDataWriter writer, DeliveryMethod deliveryMethod)
        {
            ValidateIsReadyToSend();
            
            WritePacketHandlerDataToCachedWriter(handler, writer);
            Server.Send(cachedWriter, deliveryMethod);
        }

        protected override void OnPeerConnected(NetPeer server)
        {
            base.OnPeerConnected(server);
            
            Connected?.Invoke(this, new ConnectedEventArgs(server));

            IsConnecting = false;
            IsConnected = true;

            Server = server;
        }

        protected override void OnPeerDisconnected(NetPeer server, DisconnectInfo disconnectInfo)
        {
            base.OnPeerDisconnected(server, disconnectInfo);
            
            if (IsConnected)
            {
                Disconnected?.Invoke(this, new DisconnectedEventArgs(server, disconnectInfo));
            }

            IsConnecting = false;
            IsConnected = false;

            Server = null;
            previousDisconnectInfo = disconnectInfo;
        }

        protected override void OnConnectionRequest(ConnectionRequest request)
        {
            request.Reject();
        }
    }
}