using LiteNetLib;
using Source.Shared;

namespace Source.Server
{
    public class ServerPlayerStateSyncHandler : ServerMonoPacketHandler
    {
        private PlayerStatePacket cachedPlayerStatePacket;
        
        public override int HandlerId => (int)Handlers.PlayerState;

        public override void Initialize()
        {
            base.Initialize();

            cachedPlayerStatePacket = new PlayerStatePacket();
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            cachedPlayerStatePacket.Deserialize(reader);
            cachedWriter.Reset();
            cachedWriter.Put(cachedPlayerStatePacket);
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.Unreliable);
        }
    }
}