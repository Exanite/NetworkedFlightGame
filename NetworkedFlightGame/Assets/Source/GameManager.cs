using Source.Client;
using Source.Server;
using UnityEngine;

// Here comes the god object
public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public ServerNetworkManager serverPrefab;
    public ClientNetworkManager clientPrefab;

    [Header("Runtime instantiated")]
    public ServerNetworkManager server;
    public ClientNetworkManager client;

    public void StartServer()
    {
        if (server) { }
    }

    public void StartClient()
    {
        if (client) { }
    }
}