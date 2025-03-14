using System.Threading.Tasks;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private ClientSingleton clientPrefab;

    [SerializeField] private HostSingleton hostPrefab;
    private async void Start()
    {
        DontDestroyOnLoad(gameObject);
        await LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }

    private async Task LaunchInMode(bool isDedicatedServer)
    {
        if (isDedicatedServer)
        {
            // Start the server
        }
        else
        {
            ClientSingleton clientSingleton = Instantiate(clientPrefab);
            bool authenticated = await clientSingleton.CreateClient();

            HostSingleton hostSingleton = Instantiate(hostPrefab);
            hostSingleton.CreateHost();

            if(authenticated)
            {
                clientSingleton.GameManager.GoToMenu();
            }
        }
    }

}
