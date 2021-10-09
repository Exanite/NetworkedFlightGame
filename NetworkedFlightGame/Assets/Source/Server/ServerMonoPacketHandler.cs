using Networking.Server;
using Source.Shared;
using UnityEngine;

namespace Source.Server
{
    public abstract class ServerMonoPacketHandler : MonoPacketHandler
    {
        [Header("Dependencies")]
        public UnityServer server;
    }
}