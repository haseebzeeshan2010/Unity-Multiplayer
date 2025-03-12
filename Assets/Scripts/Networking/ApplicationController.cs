using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }

    private void LaunchInMode(bool isDedicatedServer)
    {
        if (isDedicatedServer)
        {
            // Start the server
        }
        else
        {
            // Start the client
        }
    }

}
