using System;
using LiteNetLib;

/// <summary>
///     Arguments for client Disconnected events
/// </summary>
public class DisconnectedEventArgs : EventArgs
{
    /// <summary>
    ///     Creates a new <see cref="DisconnectedEventArgs"/>
    /// </summary>
    public DisconnectedEventArgs(NetPeer server, DisconnectInfo disconnectInfo)
    {
        this.Server = server;
        this.DisconnectInfo = disconnectInfo;
    }

    /// <summary>
    ///     The <see cref="NetPeer"/> of the server the client disconnected
    ///     from
    /// </summary>
    public NetPeer Server { get; }

    /// <summary>
    ///     Additional information about the disconnection
    /// </summary>
    public DisconnectInfo DisconnectInfo { get; }
}