using System.Collections.Generic;
using System.Net;
using Cysharp.Threading.Tasks;
using Networking.Client;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientNetworkManager : MonoBehaviour
    {
        [Header("Dependencies")]
        public EventBus eventBus;
        public UnityClient client;

        [Space]
        public List<ClientMonoPacketHandler> packetHandlers;

        [Header("Settings")]
        public int port = 17175;
        public string playerName = "Player";

        private async UniTask Start()
        {
            foreach (var packetHandler in packetHandlers)
            {
                client.RegisterPacketHandler(packetHandler);
            }

            var connectResult = await client.ConnectAsync(new IPEndPoint(IPAddress.Loopback, port));

            Debug.Log(connectResult.IsSuccess);

            if (!connectResult.IsSuccess)
            {
                return;
            }

            eventBus.PushEvent(new ClientJoinRequest
            {
                PlayerName = playerName,
            });
        }
    }
}