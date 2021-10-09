using Networking.Server;
using UnityEngine;

namespace Source.Server
{
    public class ServerNetworkManager : MonoBehaviour
    {
        public UnityServer server;
        public int port = 17175;

        public ServerJoinRequestHandler joinRequestHandler;

        private void Start()
        {
            server.RegisterPacketHandler(joinRequestHandler);

            server.Create(port);
        }
    }
}