using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;

public class ClientNetworkTransform : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        // Call the base method
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;
    }
    private void Update()// <--Line where error was thrown updated to remove override
    {
        // Only the owner can commit to the transform
        CanCommitToTransform = IsOwner;

        // Check if we are connected to a server or listening for connections
        if (NetworkManager != null)
        {
            if (NetworkManager.IsConnectedClient || NetworkManager.IsListening)
            {
                if (CanCommitToTransform)
                {
                    TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);
                }
            }
        }
    }

    protected override bool OnIsServerAuthoritative()
    {
        // This is a server authoritative object
        return false;
    }



    private void TryCommitTransformToServer(Transform transform, double timestamp)
    {
        Debug.Log("Committing transform to server");
    }
}
