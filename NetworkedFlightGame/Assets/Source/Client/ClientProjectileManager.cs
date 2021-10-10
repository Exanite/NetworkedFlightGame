using System;
using System.Collections.Generic;
using LiteNetLib;
using Source.Shared;
using UnityEngine;

// BulletFactory
namespace Source.Client
{
    public class ClientProjectileManager : ClientMonoPacketHandler
    {
        public List<GameObject> projectilePrefabs;

        private ProjectileCreationPacket cachedProjectileCreationPacket;

        public override int HandlerId => (int) Handlers.ProjectileCreate;

        public override void Initialize()
        {
            base.Initialize();

            cachedProjectileCreationPacket = new ProjectileCreationPacket();
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            // Skip if is own
        }

        /// <param name="owningEntityId">
        ///     Use Ship.networkId for ships, -1 for everything else
        /// </param>
        public void CreateProjectile(int prefabId, int owningEntityId, Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            cachedProjectileCreationPacket.Set(prefabId, owningEntityId, position, rotation, velocity);
            cachedWriter.Reset();
            cachedWriter.Put(cachedProjectileCreationPacket);
            client.SendAsPacketHandlerToServer(this, cachedWriter, DeliveryMethod.ReliableUnordered);
            
            InstantiateLocal(cachedProjectileCreationPacket);
        }

        private void InstantiateLocal(ProjectileCreationPacket projectileCreationPacket)
        {
            if (projectileCreationPacket.PrefabId < 0 || projectileCreationPacket.PrefabId >= projectilePrefabs.Count)
            {
                return;
            }
            
            var prefab = projectilePrefabs[projectileCreationPacket.PrefabId];
            
            
        }
    }
}