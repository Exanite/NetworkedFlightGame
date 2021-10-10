using System;
using System.Collections.Generic;
using LiteNetLib;
using Source.Shared;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Client
{
    public class ClientPlayerManager : ClientMonoPacketHandler, IEventListener<PlayerCreationEvent>, IEventListener<PlayerDestructionEvent>
    {
        public ClientNetworkManager networkManager;
        public ClientProjectileManager clientProjectileManager;

        public LocalShip localPlayerPrefab;
        public Ship remotePlayerPrefab;

        [Header("Settings")]
        public float spawnDistance = 1500;

        [Header("Debug")]
        public LocalShip localPlayer;
        public Dictionary<int, Ship> playersById;

        public override int HandlerId => (int) Handlers.Unassigned;

        public override void Initialize()
        {
            base.Initialize();

            transform.root.GetComponent<ClientPlayerManager>();

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
            var isLocal = e.Id == networkManager.localNetworkId;
            var player = CreatePlayer(e.Id, isLocal);

            if (isLocal)
            {
                localPlayer = (LocalShip) player;
            }

            playersById.Add(e.Id, player);

            Debug.Log($"Player '{e.Id}' created. Active player count: '{playersById.Count}'");
        }

        public void On(PlayerDestructionEvent e)
        {
            if (!playersById.TryGetValue(e.Id, out var player))
            {
                return;
            }

            playersById.Remove(e.Id);
            Destroy(player.gameObject);

            Debug.Log($"Player '{e.Id}' destroyed. Active player count: '{playersById.Count}'");
        }

        private Ship CreatePlayer(int id, bool isLocal)
        {
            var player = isLocal ? InstantiateLocalPlayer() : InstantiateRemotePlayer();
            player.networkId = id;
            player.transform.SetParent(transform);

            return player;
        }

        private Ship InstantiateLocalPlayer()
        {
            Debug.Log("Instantiating local player prefab");

            localPlayer = Instantiate(localPlayerPrefab);
            localPlayer.projectileManager = clientProjectileManager;
            localPlayer.transform.position = Random.onUnitSphere * spawnDistance;
            localPlayer.transform.rotation = Quaternion.LookRotation(Vector3.zero - localPlayer.transform.position);

            return localPlayer;
        }

        private Ship InstantiateRemotePlayer()
        {
            Debug.Log("Instantiating remote player prefab");

            return Instantiate(remotePlayerPrefab);
        }
    }
}