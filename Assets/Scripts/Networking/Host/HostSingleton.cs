using UnityEngine;
using System.Threading.Tasks;
public class HostSingleton : MonoBehaviour
{
    private static HostSingleton instance;

    private HostGameManager gameManager;
    public static HostSingleton Instance
    {
        get
        {
            if (instance != null) { return instance; }
            
            instance = FindAnyObjectByType<HostSingleton>();
            if (instance == null)
            {
                Debug.LogError("No HostSingleton found in the scene. Please add one.");
                return null;
            }

            return instance;
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreateHost()
    {
        gameManager = new HostGameManager();
    }

}
