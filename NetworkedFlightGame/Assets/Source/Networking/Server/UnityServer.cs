using System;
using System.Collections.Generic;
using LiteNetLib;
using LiteNetLib.Utils;

namespace Networking.Server
{
    public class UnityServer : UnityNetwork
    {
        private readonly List<NetPeer> connectedPeers = new List<NetPeer>();

        public IReadOnlyList<NetPeer> ConnectedPeers => connectedPeers;

        public bool IsCreated { get; private set; }
        public override bool IsReady => IsCreated;

        public event EventHandler<UnityServer, PeerConnectedEventArgs> PeerConnected;
        public event EventHandler<UnityServer, PeerDisconnectedEventArgs> PeerDisconnected;

        private void OnDestroy()
        {
            Close(false);
        }

        public void Create(int port)
        {
            if (IsCreated)
            {
                throw new InvalidOperationException("Server has already been created.");
            }

            netManager.Start(port);

            IsCreated = true;
        }

        public void Close()
        {
            Close(true);
        }

        public void SendAsPacketHandlerToAll(IPacketHandler handler, NetDataWriter writer, DeliveryMethod deliveryMethod)
        {
            ValidateIsReadyToSend();
            
            WritePacketHandlerDataToCachedWriter(handler, writer);
            netManager.SendToAll(cachedWriter, deliveryMethod);
        }

        protected void Close(bool pollEvents)
        {
            if (!IsCreated)
            {
                return;
            }

            netManager.DisconnectAll();

            if (pollEvents)
            {
                netManager.PollEvents();
            }

            netManager.Stop();

            IsCreated = false;
        }

        protected override void OnPeerConnected(NetPeer peer)
        {
            base.OnPeerConnected(peer);

            connectedPeers.Add(peer);

            PeerConnected?.Invoke(this, new PeerConnectedEventArgs(peer));
        }

        protected override void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            base.OnPeerDisconnected(peer, disconnectInfo);

            connectedPeers.Remove(peer);

            PeerDisconnected?.Invoke(this, new PeerDisconnectedEventArgs(peer, disconnectInfo));
        }

        protected override void OnConnectionRequest(ConnectionRequest request)
        {
            request.AcceptIfKey(Constants.ConnectionKey);
        }
    }
}