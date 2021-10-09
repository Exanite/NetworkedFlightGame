using LiteNetLib;
using LiteNetLib.Utils;
using Networking;
using UnityEngine;

namespace Source.Shared
{
    public abstract class MonoPacketHandler : MonoBehaviour, IPacketHandler
    {
        [Header("Dependencies")]
        public EventBus eventBus;

        protected NetDataWriter cachedWriter;

        public abstract int HandlerId { get; }

        public virtual void Initialize()
        {
            cachedWriter = new NetDataWriter();
        }

        public abstract void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod);
    }
}