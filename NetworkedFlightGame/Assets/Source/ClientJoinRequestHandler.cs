using LiteNetLib;
using UnityEngine;

namespace Source
{
    public class ClientJoinRequestHandler : ClientMonoPacketHandler
    {
        public override int HandlerId => (int)Handlers.JoinRequest;

        public void SendJoinRequest(string playerName)
        {
            cachedWriter.Reset();
            
            cachedWriter.Put(playerName);
            
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