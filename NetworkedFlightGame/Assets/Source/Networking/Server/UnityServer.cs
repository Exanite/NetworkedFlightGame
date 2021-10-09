using System;
using System.Collections.Generic;
using LiteNetLib;
using UnityEngine;

/// <summary>
///     Server that can accept connections from
///     <see cref="Client.UnityClient"/>s
/// </summary>
public class UnityServer : UnityNetwork
{
    [Header("Server:")]
    [SerializeField]
    private ushort port = Constants.DefaultPort;

    private readonly List<NetPeer> connectedPeers = new List<NetPeer>();

    /// <summary>
    ///     Event fired when a <see cref="NetPeer"/> connects to the server
    /// </summary>
    public event EventHandler<UnityServer, PeerConnectedEventArgs> ClientConnectedEvent;

    /// <summary>
    ///     Event fired when a <see cref="NetPeer"/> disconnects from the
    ///     server
    /// </summary>
    public event EventHandler<UnityServer, PeerDisconnectedEventArgs> ClientDisconnectedEvent;

    /// <summary>
    ///     Port the server will listen on
    /// </summary>
    public ushort Port
    {
        get => port;

        set => port = value;
    }

    /// <summary>
    ///     Is the server created and ready for connections?
    /// </summary>
    public bool IsCreated { get; private set; }

    /// <summary>
    ///     List of all the <see cref="NetPeer"/>s connected to the server
    /// </summary>
    public IReadOnlyList<NetPeer> ConnectedPeers => connectedPeers;

    protected override bool IsReady => IsCreated;

    private void OnDestroy()
    {
        Close(false);
    }

    /// <summary>
    ///     Creates the server
    /// </summary>
    public void Create()
    {
        if (IsCreated)
        {
            throw new InvalidOperationException("Server has already been created.");
        }

        netManager.Start(Port);

        IsCreated = true;
    }

    /// <summary>
    ///     Closes the server
    /// </summary>
    public void Close()
    {
        Close(true);
    }

    /// <summary>
    ///     Closes the server
    /// </summary>
    /// <param name="pollEvents">
    ///     Should events be polled?
    ///     <para/>
    ///     Note: Should be <see langword="false"/> if called when the
    ///     <see cref="Application"/> is quitting
    /// </param>
    protected void Close(bool pollEvents)
    {
        if (!IsCreated)
        {
            return;
        }

        netManager.DisconnectAll();

        if (pollEvents)
        {
            netManager.PollEvents();
        }

        netManager.Stop();

        IsCreated = false;
    }

    protected override void OnPeerConnected(NetPeer peer)
    {
        connectedPeers.Add(peer);

        ClientConnectedEvent?.Invoke(this, new PeerConnectedEventArgs(peer));
    }

    protected override void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        connectedPeers.Remove(peer);

        ClientDisconnectedEvent?.Invoke(this, new PeerDisconnectedEventArgs(peer, disconnectInfo));
    }

    protected override void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        netPacketProcessor.ReadAllPackets(reader, peer);
        reader.Recycle();
    }

    protected override void OnConnectionRequest(ConnectionRequest request)
    {
        request.AcceptIfKey(Constants.ConnectionKey);
    }
}