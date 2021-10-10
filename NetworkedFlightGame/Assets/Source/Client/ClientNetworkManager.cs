using System.Net;
using Cysharp.Threading.Tasks;
using Networking.Client;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientNetworkManager : MonoNetManager<UnityClient, ClientMonoPacketHandler>
    {
        public int localId = -1;
        
        protected override async UniTask Initialize()
        {
            await base.Initialize();

            RegisterEvents();
        }

        public async UniTask<bool> Connect(IPEndPoint endPoint, string playerName)
        {
            Debug.Log($"Trying to connect to endpoint '{endPoint}' with player name '{playerName}'");

            var connectResult = await network.ConnectAsync(endPoint);

            if (!connectResult.IsSuccess)
            {
                Debug.Log($"Connection failed");
                
                return false;
            }
            
            Debug.Log($"Connection succeeded");

            eventBus.PushEvent(new ClientJoinRequest
            {
                PlayerName = playerName,
            });

            return true;
        }

        private void RegisterEvents()
        {
            network.Connected += Network_OnConnected;
            network.Disconnected += Network_OnDisconnected;
        }

        private void Network_OnConnected(UnityClient sender, ConnectedEventArgs e)
        {
            eventBus.PushEvent(e);
        }
        
        private void Network_OnDisconnected(UnityClient sender, DisconnectedEventArgs e)
        {
            eventBus.PushEvent(e);
        }
    }
}