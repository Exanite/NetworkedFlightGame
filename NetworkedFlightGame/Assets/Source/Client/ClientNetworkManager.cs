using System.Net;
using Cysharp.Threading.Tasks;
using Networking.Client;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public class ClientNetworkManager : MonoNetManager<UnityClient, ClientMonoPacketHandler>
    {
        [Header("Settings")]
        public int port = 17175;
        public string playerName = "Player";

        protected override async UniTask Initialize()
        {
            await base.Initialize();

            Debug.Log("Client starting");

            var connectResult = await network.ConnectAsync(new IPEndPoint(IPAddress.Loopback, port));

            Debug.Log($"Connect isSuccess: {connectResult.IsSuccess}");

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