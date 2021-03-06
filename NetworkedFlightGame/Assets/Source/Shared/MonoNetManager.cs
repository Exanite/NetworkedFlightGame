using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Networking;
using UnityEngine;

namespace Source.Shared
{
    public abstract class MonoNetManager<TNetwork, TPacketHandler> : MonoBehaviour
        where TNetwork : UnityNetwork
        where TPacketHandler : MonoPacketHandler
    {
        [Header("Dependencies")]
        public EventBus eventBus;
        public TNetwork network;
        
        [Header("Debug")]
        public List<TPacketHandler> packetHandlers;

        protected void Start()
        {
            Initialize().Forget();
        }

        protected virtual UniTask Initialize()
        {
            RegisterPacketHandlers();
            
            return UniTask.CompletedTask;
        }

        protected void RegisterPacketHandlers()
        {
            packetHandlers.Clear();
            packetHandlers.AddRange(GetComponentsInChildren<TPacketHandler>());
            
            foreach (var packetHandler in packetHandlers)
            {
                if (packetHandler.HandlerId != (int) Handlers.Unassigned)
                {
                    network.RegisterPacketHandler(packetHandler);
                }
                
                packetHandler.Initialize();
            }
        }
    }
}