using LiteNetLib;
using Source.Shared;

namespace Source.Client
{
    public class ClientPlayerLifetimeHandler : ClientMonoPacketHandler
    {
        public override int HandlerId => (int) Handlers.PlayerLifetime;
        
        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
        }
    }
}