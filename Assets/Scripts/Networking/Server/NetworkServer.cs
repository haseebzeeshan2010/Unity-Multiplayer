using UnityEngine;
using Unity.Netcode;
using System;
using System.Collections.Generic;
using System.Collections;

public class NetworkServer : IDisposable
{

    private NetworkManager networkManager;

    private Dictionary<ulong, string> clientIdToAuth = new Dictionary<ulong, string>();
    private Dictionary<string, UserData> authIdToUserData = new Dictionary<string, UserData>();

    public NetworkServer(NetworkManager networkManager)
    {
        this.networkManager = networkManager;

        networkManager.ConnectionApprovalCallback += ApprovalCheck; // Possibly change method to ConnectionApprovalCheck
        networkManager.OnServerStarted += OnNetworkReady; // Invoked when the server starts.
    }

    private void ApprovalCheck(
        NetworkManager.ConnectionApprovalRequest request,
        NetworkManager.ConnectionApprovalResponse response)
    {
        
        string payload = System.Text.Encoding.UTF8.GetString(request.Payload); // Decodes the byte array 'request.Payload' into a UTF-8 encoded string.
        UserData userData = JsonUtility.FromJson<UserData>(payload); // Converts the JSON string into a UserData object.

        clientIdToAuth[request.ClientNetworkId] = userData.userAuthId; // Maps the client ID to the authentication ID.
        authIdToUserData[userData.userAuthId] = userData; // Maps the authentication ID to the UserData object.

        response.Approved = true; // Approves the connection.
        response.CreatePlayerObject = true; // Creates a player object for the connection.
    }

    private void OnNetworkReady()
    {

        networkManager.OnClientDisconnectCallback += OnClientDisconnect; // Invoked when a client disconnects.

    }

    private void OnClientDisconnect(ulong clientId)
    {
        if (clientIdToAuth.TryGetValue(clientId, out string authId))
        {
            clientIdToAuth.Remove(clientId); // Removes the client ID from the dictionary.
            authIdToUserData.Remove(authId); // Removes the authentication ID from the dictionary.
        }
    }

    public void Dispose()
    {

        if (networkManager == null) { return; } // Checks if the network manager is null.

        networkManager.ConnectionApprovalCallback -= ApprovalCheck; // Unsubscribes from the connection approval callback.
        networkManager.OnServerStarted -= OnNetworkReady; // Unsubscribes from the server started callback.
        networkManager.OnClientDisconnectCallback -= OnClientDisconnect; // Unsubscribes from the client disconnect callback.   

        if (networkManager.IsListening) // Checks if the network manager is a server.
        {
            networkManager.Shutdown(); // Shuts down the network manager.
        }
    }

}

