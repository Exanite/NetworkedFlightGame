using System.Net;
using Cysharp.Threading.Tasks;
using Networking.Client;
using UnityEngine;

namespace Source.Client
{
    public class ClientNetworkManager : MonoBehaviour
    {
        public UnityClient client;
        public int port = 17175;

        public string playerName = "Player";
        
        public ClientJoinRequestHandler joinRequestHandler;
        
        private async UniTask Start()
        {
            client.RegisterPacketHandler(joinRequestHandler);

            var connectResult = await client.ConnectAsync(new IPEndPoint(IPAddress.Loopback, port));

            Debug.Log(connectResult.IsSuccess);

            if (!connectResult.IsSuccess)
            {
                return;
            }
            
            joinRequestHandler.SendJoinRequest(playerName);
        }
    }
}