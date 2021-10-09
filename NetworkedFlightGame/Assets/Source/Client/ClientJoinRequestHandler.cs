using LiteNetLib;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientJoinRequestHandler : ClientMonoPacketHandler, IEventListener<ClientJoinRequest>
    {
        public override int HandlerId => (int) Handlers.JoinRequest;
        
        public override void Initialize()
        {
            base.Initialize();
            
            eventBus.AddListener(this);
        }

        public void On(ClientJoinRequest e)
        {
            cachedWriter.Put(e);

            client.SendAsPacketHandlerToServer(this, cachedWriter, DeliveryMethod.ReliableOrdered);
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var hasJoinSucceeded = reader.GetBool();

            if (!hasJoinSucceeded)
            {
                var reason = reader.GetString();

                Debug.LogWarning($"Failed to join server. Reason: '{reason}'");

                return;
            }

            Debug.Log("Successfully connected to server");
        }
    }
}