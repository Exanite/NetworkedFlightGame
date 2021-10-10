using System;
using System.Collections.Generic;
using LiteNetLib;
using Source.Shared;
using UnityEngine;

namespace Source.Server
{
    public class ServerPlayerLifetimeHandler : ServerMonoPacketHandler,
        IEventListener<PlayerConnectionAddedEvent>,
        IEventListener<PlayerConnectionRemovedEvent>
    {
        public override int HandlerId => (int) Handlers.PlayerLifetime;

        [Header("Debug")]
        public List<int> instantiatedPlayerIds;

        public override void Initialize()
        {
            base.Initialize();

            instantiatedPlayerIds = new List<int>();

            eventBus.RegisterListener<PlayerConnectionAddedEvent>(this);
            eventBus.RegisterListener<PlayerConnectionRemovedEvent>(this);
        }

        public override void Receive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        public void On(PlayerConnectionAddedEvent e)
        {
            // Tell all players to instantiate new player
            // Tell new player to instantiate other players and self
            var newPlayerId = e.PlayerConnection.Id;

            instantiatedPlayerIds.Add(newPlayerId);

            var creationEvent = new PlayerCreationEvent
            {
                Id = newPlayerId,
            };

            eventBus.PushEvent(creationEvent);

            cachedWriter.Reset();
            cachedWriter.Put(creationEvent);
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.ReliableOrdered);

            foreach (var instantiatedPlayerId in instantiatedPlayerIds)
            {
                if (instantiatedPlayerId == newPlayerId)
                {
                    continue;
                }

                creationEvent.Id = instantiatedPlayerId;

                cachedWriter.Reset();
                cachedWriter.Put((int) PlayerLifetimePacketType.Creation);
                cachedWriter.Put(creationEvent);
                server.SendAsPacketHandler(this, e.PlayerConnection.peer, cachedWriter, DeliveryMethod.ReliableOrdered);
            }
        }

        public void On(PlayerConnectionRemovedEvent e)
        {
            var id = e.PlayerConnection.Id;

            instantiatedPlayerIds.Remove(id);

            var destructionEvent = new PlayerDestructionEvent();

            eventBus.PushEvent(destructionEvent);

            cachedWriter.Reset();
            cachedWriter.Put((int) PlayerLifetimePacketType.Destruction);
            cachedWriter.Put(destructionEvent);
            server.SendAsPacketHandlerToAll(this, cachedWriter, DeliveryMethod.ReliableOrdered);
        }
    }
}