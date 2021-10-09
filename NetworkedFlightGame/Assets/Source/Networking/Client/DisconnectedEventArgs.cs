using System;
using LiteNetLib;

namespace Networking.Client
{
    public class DisconnectedEventArgs : EventArgs
    {
        public DisconnectedEventArgs(NetPeer server, DisconnectInfo disconnectInfo)
        {
            Server = server;
            DisconnectInfo = disconnectInfo;
        }

        public NetPeer Server { get; }
        public DisconnectInfo DisconnectInfo { get; }
    }
}