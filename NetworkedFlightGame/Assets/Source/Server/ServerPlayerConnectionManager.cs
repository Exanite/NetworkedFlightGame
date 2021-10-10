using System.Collections.Generic;
using LiteNetLib;
using UnityEngine;

namespace Source.Server
{
    public class ServerPlayerConnectionManager : MonoBehaviour
    {
        public EventBus eventBus;

        public Dictionary<int, PlayerConnection> playerConnectionsById;

        public IReadOnlyCollection<PlayerConnection> PlayerConnections => playerConnectionsById.Values;

        private void Awake()
        {
            playerConnectionsById = new Dictionary<int, PlayerConnection>();
        }

        public void AddPlayerConnection(NetPeer peer, string name)
        {
            var playerConnection = new PlayerConnection(peer, name);

            playerConnectionsById.Add(playerConnection.Id, playerConnection);

            eventBus.PushEvent(new PlayerConnectionAddedEvent { PlayerConnection = playerConnection });
        }
    }
}