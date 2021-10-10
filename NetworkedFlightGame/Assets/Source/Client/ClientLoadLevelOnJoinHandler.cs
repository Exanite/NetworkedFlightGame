using LiteNetLib;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientLoadLevelOnJoinHandler : ClientMonoPacketHandler, IEventListener<ClientJoinSucceededEvent>
    {
        public GameObject levelPrefab;

        public GameObject level;
        
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


        public void On(ClientJoinSucceededEvent e)
        {
            level = Instantiate(levelPrefab);
        }
    }
}