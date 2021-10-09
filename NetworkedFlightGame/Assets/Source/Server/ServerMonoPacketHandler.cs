using Networking.Server;
using Source.Shared;

namespace Source.Server
{
    public abstract class ServerMonoPacketHandler : MonoPacketHandler
    {
        public UnityServer server;
    }
}