using System;
using System.Collections.Generic;
using LiteNetLib;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientPlayerManager : ClientMonoPacketHandler, IEventListener<PlayerCreationEvent>, IEventListener<PlayerDestructionEvent>
    {
        public ClientNetworkManager networkManager;
        public BulletManager bulletManager;

        public LocalShip localPlayerPrefab;
        public Ship remotePlayerPrefab;

        [Header("Debug")]
        public LocalShip localPlayer;
        public Dictionary<int, Ship> playersById;

        public override int HandlerId => (int) Handlers.NotAHandler;

        public override void Initialize()
        {
            base.Initialize();

            playersById = new Dictionary<int, Ship>();
            
            eventBus.RegisterListener<PlayerCreationEvent>(this);
            eventBus.RegisterListener<PlayerDestructionEvent>(this);
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        public void On(PlayerCreationEvent e)
        {
            var isLocal = e.Id == networkManager.localId;

            var player = CreatePlayer(isLocal);
            player.networkId = e.Id;

            if (isLocal)
            {
                localPlayer = (LocalShip)player;
            }

            playersById.Add(e.Id, player);

            Debug.Log($"Player Created. Active player count: '{playersById.Count}'");
        }

        public void On(PlayerDestructionEvent e)
        {
            if (!playersById.TryGetValue(e.Id, out var player))
            {
                return;
            }

            playersById.Remove(e.Id);
            Destroy(player.gameObject);
            
            Debug.Log($"Player Destroyed. Active player count: '{playersById.Count}'");
        }

        private Ship CreatePlayer(bool isLocal)
        {
            return isLocal ? InstantiateLocalPlayer() : InstantiateRemotePlayer();
        }

        private Ship InstantiateLocalPlayer()
        {
            // Todo Set ids
            Debug.Log("Instantiating local player prefab");

            localPlayer = Instantiate(localPlayerPrefab);
            localPlayer.bulletManager = bulletManager;
  
            return localPlayer;
        }

        private Ship InstantiateRemotePlayer()
        {
            Debug.Log("Instantiating remote player prefab");

            return Instantiate(remotePlayerPrefab);
        }
    }
}