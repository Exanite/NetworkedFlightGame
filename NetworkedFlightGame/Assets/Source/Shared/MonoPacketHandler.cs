using LiteNetLib;
using LiteNetLib.Utils;
using Networking;
using UnityEngine;

namespace Source.Shared
{
    public abstract class MonoPacketHandler : MonoBehaviour, IPacketHandler
    {
        protected NetDataWriter cachedWriter;

        public abstract int HandlerId { get; }

        protected virtual void Awake()
        {
            cachedWriter = new NetDataWriter();
        }

        public abstract void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod);
    }
}