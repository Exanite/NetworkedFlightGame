using Networking.Client;
using Source.Shared;
using UnityEngine;

namespace Source.Client
{
    public abstract class ClientMonoPacketHandler : MonoPacketHandler
    {
        [Header("Dependencies")]
        public UnityClient client;
    }
}