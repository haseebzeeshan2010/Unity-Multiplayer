using UnityEngine;

public class CoinWallet : NetworkBehaviour
{
    public NetworkVariable<int> TotalCoins = new NetworkVariable<int>(0);


    onTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent(out Coin coin)) {return;}

        int coinValue = coin.Value;
        if(!IsServer) {return;}

        TotalCoins.Value += coinValue;
    }
}
