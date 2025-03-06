using UnityEngine;
using Unity.Netcode;
public class Health : NetworkBehaviour
{
    [field: SerializeField] public int MaxHealth {get; private set;} = 100;

    public NetworkVariable<int> health = new NetworkVariable<int>(100f);
    
    
}
