using LiteNetLib;
using Source.Shared;

namespace Source.Client
{
    public class ClientPlayerLifetimeHandler : ClientMonoPacketHandler
    {
        public override int HandlerId => (int) Handlers.PlayerLifetime;

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var packetType = (PlayerLifetimePacketType) reader.GetInt();

            switch (packetType)
            {
                case PlayerLifetimePacketType.Creation:
                {
                    var creationEvent = reader.Get<PlayerCreationEvent>();
                    eventBus.PushEvent(creationEvent);

                    break;
                }
                case PlayerLifetimePacketType.Destruction:
                {
                    var destructionEvent = reader.Get<PlayerDestructionEvent>();
                    eventBus.PushEvent(destructionEvent);

                    break;
                }
            }
        }
    }
}