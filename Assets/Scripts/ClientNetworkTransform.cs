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
    protected override void Update()// <--Line where error is thrown
    {
        // Only the owner can commit to the transform
        CanCommitToTransform = IsOwner;
        base.Update();


        // Check if we are connected to a server or listening for connections
        if (NetworkManager != null)
        {
            if(NetworkManager.IsConnectedClient || NetworkManager.IsListening)
            {
                if(CanCommitToTransform)
                {
                    TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time); //<--Error thrown here as well
                }
            }
        }
    }

    protected override bool OnIsServerAuthoritative()
    {
        // This is a server authoritative object
        return false;
    }
}
