using LiteNetLib;
using Source.Shared;

namespace Source.Client
{
    public class ClientIdHandler : ClientMonoPacketHandler, IEventListener<ClientIdAssignmentEvent>
    {
        public ClientNetworkManager networkManager;
            
        public override int HandlerId => (int) Handlers.Unassigned;

        public override void Initialize()
        {
            base.Initialize();
            
            eventBus.RegisterListener(this);
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            throw new System.NotImplementedException();
        }

        public void On(ClientIdAssignmentEvent e)
        {
            networkManager.localNetworkId = e.Id;
        }
    }
}