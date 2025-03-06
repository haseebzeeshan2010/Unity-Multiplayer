using UnityEngine;
using Unity.Netcode;
public class DealDamageOnContact : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int damage = 5;
    
    private ulong ownerClientId;

    public void SetOwnerClientId(ulong clientId)
    {
        ownerClientId = clientId;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject netObj))
        {
            if (netObj.OwnerClientId == ownerClientId) return;
            
        }

        if (col.attachedRigidbody==null) return;

        if(col.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }
    }
}
