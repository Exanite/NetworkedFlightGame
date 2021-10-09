using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

namespace Networking
{
    public abstract class UnityNetwork : MonoBehaviour, INetEventListener
    {
        protected NetManager netManager;
        protected Dictionary<int, IPacketHandler> packetHandlers;

        protected NetDataWriter cachedWriter;

        public abstract bool IsReady { get; }
        public IReadOnlyDictionary<int, IPacketHandler> PacketHandlers => packetHandlers;

        private void Awake()
        {
            netManager = new NetManager(this);
            packetHandlers = new Dictionary<int, IPacketHandler>();

            cachedWriter = new NetDataWriter();
        }

        private void FixedUpdate()
        {
            netManager.PollEvents();
        }

        public void RegisterPacketHandler(IPacketHandler handler)
        {
            packetHandlers.Add(handler.HandlerId, handler);
        }

        public void UnregisterPacketHandler(IPacketHandler handler)
        {
            UnregisterPacketHandler(handler.HandlerId);
        }

        public void UnregisterPacketHandler(int id)
        {
            packetHandlers.Remove(id);
        }

        public void SendAsPacketHandler(IPacketHandler handler, NetPeer peer, NetDataWriter writer, DeliveryMethod deliveryMethod)
        {
            ValidateIsReadyToSend();

            WritePacketHandlerDataToCachedWriter(handler, writer);
            peer.Send(cachedWriter, deliveryMethod);
        }

        protected void WritePacketHandlerDataToCachedWriter(IPacketHandler handler, NetDataWriter writer)
        {
            cachedWriter.Reset();

            cachedWriter.Put(true); // For usePacketHandler

            cachedWriter.Put(handler.HandlerId);
            cachedWriter.Put(writer.Data, 0, writer.Length);
        }

        protected void ValidateIsReadyToSend()
        {
            if (!IsReady)
            {
                throw new InvalidOperationException($"{GetType()} is not ready to send.");
            }
        }

        protected virtual void OnPeerConnected(NetPeer peer) { }

        protected virtual void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo) { }

        protected virtual void OnNetworkError(IPEndPoint endPoint, SocketError socketError) { }

        protected virtual void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var usePacketHandler = reader.GetBool();

            if (usePacketHandler)
            {
                var packetHandlerId = reader.GetInt();

                if (!packetHandlers.TryGetValue(packetHandlerId, out var packetHandler))
                {
                    return;
                }

                packetHandler.Receive(peer, reader, deliveryMethod);
            }
        }

        protected virtual void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType) { }

        protected virtual void OnNetworkLatencyUpdate(NetPeer peer, int latency) { }

        protected virtual void OnConnectionRequest(ConnectionRequest request) { }

        void INetEventListener.OnPeerConnected(NetPeer peer)
        {
            OnPeerConnected(peer);
        }

        void INetEventListener.OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            OnPeerDisconnected(peer, disconnectInfo);
        }

        void INetEventListener.OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            OnNetworkError(endPoint, socketError);
        }

        void INetEventListener.OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            OnNetworkReceive(peer, reader, deliveryMethod);
        }

        void INetEventListener.OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            OnNetworkReceiveUnconnected(remoteEndPoint, reader, messageType);
        }

        void INetEventListener.OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            OnNetworkLatencyUpdate(peer, latency);
        }

        void INetEventListener.OnConnectionRequest(ConnectionRequest request)
        {
            OnConnectionRequest(request);
        }
    }
}