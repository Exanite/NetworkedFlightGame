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

        public GameObject localPlayerPrefab;
        public GameObject remotePlayerPrefab;

        [Header("Debug")]
        public GameObject localPlayer;
        public Dictionary<int, GameObject> playersById;

        public override int HandlerId => (int) Handlers.NotAHandler;

        public override void Initialize()
        {
            base.Initialize();

            playersById = new Dictionary<int, GameObject>();
            
            eventBus.RegisterListener<PlayerCreationEvent>(this);
            eventBus.RegisterListener<PlayerDestructionEvent>(this);
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        public void On(PlayerCreationEvent e)
        {
            var isLocalPlayer = e.Id == networkManager.localId;

            var player = isLocalPlayer ? InstantiateLocalPlayer() : InstantiateRemotePlayer();

            if (isLocalPlayer)
            {
                localPlayer = player;
            }

            playersById.Add(e.Id, player);
        }

        public void On(PlayerDestructionEvent e)
        {
            if (!playersById.TryGetValue(e.Id, out var player))
            {
                return;
            }

            playersById.Remove(e.Id);
            Destroy(player);
        }

        private GameObject InstantiateLocalPlayer()
        {
            Debug.Log("Instantiating local player prefab");

            localPlayer = Instantiate(localPlayerPrefab);

            return localPlayer;
        }

        private GameObject InstantiateRemotePlayer()
        {
            Debug.Log("Instantiating remote player prefab");

            return Instantiate(remotePlayerPrefab);
        }
    }
}