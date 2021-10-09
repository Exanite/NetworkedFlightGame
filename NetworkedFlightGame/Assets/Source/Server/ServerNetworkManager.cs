using Cysharp.Threading.Tasks;
using Networking.Server;
using Source.Shared;
using UnityEngine;

namespace Source.Server
{
    public class ServerNetworkManager : MonoNetManager<UnityServer, ServerMonoPacketHandler>
    {
        [Header("Settings")]
        public int port = 17175;

        protected override async UniTask Initialize()
        {
            await base.Initialize();

            Debug.Log("Server starting");

            network.Create(port);
        }
    }
}