using LiteNetLib;
using Source.Shared;

namespace Source.Server
{
    public class ServerPlayerStateSyncHandler : ServerMonoPacketHandler
    {
        public override int HandlerId => (int)Handlers.PlayerState;
        
        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            cachedWriter.Put(reader.RawData, reader.Position, reader.AvailableBytes);
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.Unreliable);
        }
    }
}