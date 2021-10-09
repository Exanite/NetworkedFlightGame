using System;
using LiteNetLib;

/// <summary>
///     Arguments for client Connected events
/// </summary>
public class ConnectedEventArgs : EventArgs
{
    /// <summary>
    ///     Creates a new <see cref="ConnectedEventArgs"/>
    /// </summary>
    public ConnectedEventArgs(NetPeer server)
    {
        this.Server = server;
    }

    /// <summary>
    ///     The <see cref="NetPeer"/> of the server the client connected to
    /// </summary>
    public NetPeer Server { get; }
}