using UnityEngine;
using Unity.Netcode;
public class Leaderboard : NetworkBehaviour
{
    [SerializeField] private Transform laederboardEntityHolder;
    [SerializeField] private LeaderboardEntityDisplay leaderboardEntityPrefab;

    private NetworkList<LeaderboardEntityState> leaderboardEntities;

    private void Awake()
    {
        leaderboardEntities = new NetworkList<LeaderboardEntityState>();
    }
}
