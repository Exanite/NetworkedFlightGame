using Cysharp.Threading.Tasks;
using Networking.Server;
using Source.Shared;
using UnityEngine;

namespace Source.Server
{
    public class ServerNetworkManager : MonoNetManager<UnityServer, ServerMonoPacketHandler>
    {
        protected override async UniTask Initialize()
        {
            await base.Initialize();

            RegisterEvents();
        }

        public void Create(int port)
        {
            Debug.Log($"Creating server on port '{port}'");

            network.Create(port);
        }
        
        private void RegisterEvents()
        {
            network.PeerConnected += Network_OnPeerConnected;
            network.PeerDisconnected += Network_OnPeerDisconnected;
        }

        private void Network_OnPeerConnected(UnityServer sender, PeerConnectedEventArgs e)
        {
            eventBus.PushEvent(e);
        }
        
        private void Network_OnPeerDisconnected(UnityServer sender, PeerDisconnectedEventArgs e)
        {
            eventBus.PushEvent(e);
        }
    }
}