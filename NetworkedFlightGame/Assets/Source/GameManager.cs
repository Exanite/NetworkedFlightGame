using System.Net;
using Cysharp.Threading.Tasks;
using Source.Client;
using Source.Server;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public ServerNetworkManager serverPrefab;
    public ClientNetworkManager clientPrefab;

    [Header("Runtime instantiated")]
    public ServerNetworkManager server;
    public ClientNetworkManager client;

    public void CreateServer(int port)
    {
        if (!server)
        {
            server = Instantiate(serverPrefab);
        }

        server.Create(port);
    }

    public UniTask<bool> CreateClientAndConnect(IPEndPoint endPoint, string playerName)
    {
        if (!client)
        {
            client = Instantiate(clientPrefab);
        }

        return client.Connect(endPoint, playerName);
    }
}