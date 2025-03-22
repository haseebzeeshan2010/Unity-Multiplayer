using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkServer
{
    private NetworkManager networkManager;
    public NetworkServer(NetworkManager networkManager)
    {
        this.networkManager = networkManager;

        networkManager.ConnectionApprovalCallback += ApprovalCheck; // Possibly change method to ConnectionApprovalCheck
    }

    private void ApprovalCheck(
        NetworkManager.ConnectionApprovalRequest request,
        NetworkManager.ConnectionApprovalResponse response)
    {
        
        string payload = System.Text.Encoding.UTF8.GetString(request.Payload); // Decodes the byte array 'request.Payload' into a UTF-8 encoded string.
        UserData userData = JsonUtility.FromJson<UserData>(payload); // Converts the JSON string into a UserData object.

        Debug.Log(userData.username);

        response.Approved = true; // Approves the connection.
        response.CreatePlayerObject = true; // Creates a player object for the connection.
    }

}

