using System.Collections.Generic;
using LiteNetLib;
using Networking.Server;
using UnityEngine;

namespace Source.Server
{
    public class ServerPlayerConnectionManager : MonoBehaviour, IEventListener<PeerDisconnectedEventArgs>
    {
        public EventBus eventBus;

        public Dictionary<int, PlayerConnection> playerConnectionsById;

        public IReadOnlyCollection<PlayerConnection> PlayerConnections => playerConnectionsById.Values;

        private void Awake()
        {
            playerConnectionsById = new Dictionary<int, PlayerConnection>();
            
            eventBus.RegisterListener(this);
        }

        public void AddPlayerConnection(NetPeer peer, string name)
        {
            var playerConnection = new PlayerConnection(peer, name);

            playerConnectionsById.Add(playerConnection.Id, playerConnection);

            eventBus.PushEvent(new PlayerConnectionAddedEvent { PlayerConnection = playerConnection });
        }

        public void On(PeerDisconnectedEventArgs e)
        {
            if (!playerConnectionsById.TryGetValue(e.Peer.Id, out var playerConnection))
            {
                return;
            }

            playerConnectionsById.Remove(e.Peer.Id);
            eventBus.PushEvent(new PlayerConnectionRemovedEvent() { PlayerConnection = playerConnection });
        }
    }
}