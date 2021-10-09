using Networking.Client;
using UnityEngine;

namespace Source
{
    public abstract class ClientMonoPacketHandler : MonoPacketHandler
    {
        [Header("Dependencies")]
        public UnityClient client;
    }
}