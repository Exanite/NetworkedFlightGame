using System;
using LiteNetLib;

/// <summary>
///     Arguments for server PeerDisconnected events
/// </summary>
public class PeerDisconnectedEventArgs : EventArgs
{
    /// <summary>
    ///     Creates a new <see cref="PeerDisconnectedEventArgs"/>
    /// </summary>
    public PeerDisconnectedEventArgs(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        this.Peer = peer;
        this.DisconnectInfo = disconnectInfo;
    }

    /// <summary>
    ///     The <see cref="NetPeer"/> that disconnected from the server
    /// </summary>
    public NetPeer Peer { get; }

    /// <summary>
    ///     Additional information about the disconnection
    /// </summary>
    public DisconnectInfo DisconnectInfo { get; }
}