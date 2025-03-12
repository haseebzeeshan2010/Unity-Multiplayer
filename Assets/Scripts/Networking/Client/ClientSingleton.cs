using UnityEngine;

public class ClientSingleton : MonoBehaviour
{
    private static ClientSingleton instance;

    private ClientGameManager gameManager;
    public static ClientSingleton Instance
    {
        get
        {
            if (instance != null) { return instance; }
            
            instance = FindObjectOfType<ClientSingleton>();
            if (instance == null)
            {
                Deubg.LogError("No ClientSingleton found in the scene. Please add one.");
                return null;
            }

            return instance;
        }
    }
    private void Start()
    {
        Don'tDestroyOnLoad(gameObject);
    }

    public async Task CreateClient()
    {
        gameManager = new ClientGameManager();

        await gameManager.InitAsync();
    }

}
