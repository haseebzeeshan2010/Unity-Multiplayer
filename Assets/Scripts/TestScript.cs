using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputReader.MoveEvent += HandleMove;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
    }

    private void HandleMove(Vector2 movement)
    {
        Debug.Log("Move event received with value: " + movement);
    }
}
