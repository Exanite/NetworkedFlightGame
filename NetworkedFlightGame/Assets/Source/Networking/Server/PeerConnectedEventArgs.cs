using System;
using LiteNetLib;

/// <summary>
///     Arguments for server PeerConnected events
/// </summary>
public class PeerConnectedEventArgs : EventArgs
{
    /// <summary>
    ///     Creates a new <see cref="PeerConnectedEventArgs"/>
    /// </summary>
    public PeerConnectedEventArgs(NetPeer peer)
    {
        this.Peer = peer;
    }

    /// <summary>
    ///     The <see cref="NetPeer"/> that connected to the server
    /// </summary>
    public NetPeer Peer { get; }
}