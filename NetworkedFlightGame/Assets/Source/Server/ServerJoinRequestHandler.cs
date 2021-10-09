using System.Linq;
using LiteNetLib;
using Source.Shared;

namespace Source.Server
{
    public class ServerJoinRequestHandler : ServerMonoPacketHandler
    {
        public ServerPlayerConnectionManager playerConnectionManager;
        
        public override int HandlerId => (int)Handlers.JoinRequest;
        
        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var playerName = reader.GetString();
            
            var isConflict = 
                playerConnectionManager.PlayerConnections.FirstOrDefault(x => x.playerName == playerName) != null;

            if (isConflict)
            {
                OnJoinFailed(peer);
                return;
            }

            OnJoinSucceeded(peer, playerName);
        }

        private void OnJoinFailed(NetPeer peer)
        {
            cachedWriter.Reset();
            cachedWriter.Put(false);
            cachedWriter.Put("Another player has the same name");
            
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.ReliableOrdered);
        }
        
        private void OnJoinSucceeded(NetPeer peer, string playerName)
        {
            playerConnectionManager.AddPlayerConnection(peer, playerName);
            
            cachedWriter.Reset();
            cachedWriter.Put(true);
            
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.ReliableOrdered);
        }
    }
}