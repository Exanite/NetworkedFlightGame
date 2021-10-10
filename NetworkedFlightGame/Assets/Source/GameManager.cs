using System.Net;
using Cysharp.Threading.Tasks;
using Networking.Client;
using Source.Client;
using Source.Server;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        client.network.Disconnected += Client_OnDisconnected;

        return client.Connect(endPoint, playerName);
    }
    
    private void Client_OnDisconnected(UnityClient sender, DisconnectedEventArgs e)
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex);
    }
}