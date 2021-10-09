using Networking.Client;
using Source.Shared;

namespace Source.Client
{
    public abstract class ClientMonoPacketHandler : MonoPacketHandler
    {
        public UnityClient client;
    }
}