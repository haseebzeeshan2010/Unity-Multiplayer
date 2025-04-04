using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;
public class NetworkClient : IDisposable
{

    private NetworkManager networkManager;

    private const string MenuSceneName = "Menu";

    public NetworkClient(NetworkManager networkManager)
    {
        this.networkManager = networkManager;

        networkManager.OnClientDisconnectCallback += OnClientDisconnect; // Run when a client disconnects.

    }

    private void OnClientDisconnect(ulong clientId)
    {
        if(clientId != 0 && clientId != networkManager.LocalClientId){ return; } // makes sure only client disconnects

        if(SceneManager.GetActiveScene().name != MenuSceneName) // Check if the current scene is not MainMenu
        {
            SceneManager.LoadScene(MenuSceneName); // Load the MainMenu scene
        }

        if (networkManager.IsConnectedClient) // Check if the client is still connected
        {
            networkManager.Shutdown(); // Shutdown the network manager
        }

    }

    public void Dispose()
    {
        if(networkManager != null)
        {
            networkManager.OnClientDisconnectCallback -= OnClientDisconnect; // Shutdown the network manager
        }


    }

}
