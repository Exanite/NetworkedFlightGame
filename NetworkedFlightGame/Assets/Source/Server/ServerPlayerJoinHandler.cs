using System.Linq;
using LiteNetLib;
using Source.Shared;

namespace Source.Server
{
    public class ServerPlayerJoinHandler : ServerMonoPacketHandler
    {
        public ServerPlayerConnectionManager playerConnectionManager;

        public override int HandlerId => (int) Handlers.PlayerJoin;

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var joinRequest = reader.Get<ClientJoinRequest>();
            var playerName = joinRequest.PlayerName;

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

            server.SendAsPacketHandler(this, peer, cachedWriter, DeliveryMethod.ReliableOrdered);
            server.DisconnectPeer(peer);
        }

        private void OnJoinSucceeded(NetPeer peer, string playerName)
        {
            cachedWriter.Reset();
            cachedWriter.Put(true);
            cachedWriter.Put(peer.Id);

            server.SendAsPacketHandler(this, peer, cachedWriter, DeliveryMethod.ReliableOrdered);
            
            playerConnectionManager.AddPlayerConnection(peer, playerName);
        }
    }
}