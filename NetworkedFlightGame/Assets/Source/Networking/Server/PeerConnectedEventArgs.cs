using System;
using LiteNetLib;

namespace Networking.Server
{
    public class PeerConnectedEventArgs : EventArgs
    {
        public PeerConnectedEventArgs(NetPeer peer)
        {
            Peer = peer;
        }

        public NetPeer Peer { get; }
    }
}