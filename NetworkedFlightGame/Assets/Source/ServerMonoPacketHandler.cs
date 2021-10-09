using Networking.Server;
using UnityEngine;

namespace Source
{
    public abstract class ServerMonoPacketHandler : MonoPacketHandler
    {
        [Header("Dependencies")]
        public UnityServer server;
    }
}