using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;


public class ClientGameManager
{
    private JoinAllocation allocation;
    private const string MenuSceneName = "Menu";

    public async Task<bool> InitAsync()
    {
        await UnityServices.InitializeAsync();

        AuthState authState = await AuthenticationWrapper.DoAuth();

        if(authState == AuthState.Authenticated)
        {
            return true;
        }

        return false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }

    public async Task StartClientAsync(string joinCode)
    {
        try
        {
            allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }

        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        RelayServerData relayServerData = AllocationUtils.ToRelayServerData(allocation, "dtls"); // May not work on some ISPs, try "utp" instead of "dtls"
        transport.SetRelayServerData(relayServerData);

        UserData userData = new UserData
        {
            username = PlayerPrefs.GetString(NameSelector.PlayerNameKey, "Missing Name")
        };
        
        string payload = JsonUtility.ToJson(userData); // Converts the UserData object into a JSON string.
        byte[] payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload); // Encodes the JSON string into a UTF-8 encoded byte array.

        NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes; // Sets the connection data to the byte array.

        NetworkManager.Singleton.StartClient();
    }
}
