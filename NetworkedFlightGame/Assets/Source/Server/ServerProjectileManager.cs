using LiteNetLib;
using Source.Shared;

namespace Source.Server
{
    public class ServerProjectileManager : ServerMonoPacketHandler
    {
        private ProjectileCreationPacket cachedProjectileCreationPacket;
        
        public override int HandlerId => (int) Handlers.ProjectileCreate;

        public override void Initialize()
        {
            base.Initialize();

            cachedProjectileCreationPacket = new ProjectileCreationPacket();
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            cachedProjectileCreationPacket.Deserialize(reader);
            cachedWriter.Reset();
            cachedWriter.Put(cachedProjectileCreationPacket);
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.ReliableUnordered);
        }
    }
}