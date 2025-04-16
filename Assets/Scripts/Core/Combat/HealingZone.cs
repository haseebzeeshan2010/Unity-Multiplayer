using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class HealingZone : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private Image healPowerBar;

    [Header("Settings")]

    [SerializeField] private int maxHealPower = 30; // how many times it can restore health
    [SerializeField] private float healCooldown = 60f; // how long it takes to recharge the heal power

    [SerializeField] private float healTickRate = 1f; // how often it heals
    [SerializeField] private int coinsPerTick = 10; // how many coins it consumes per tick
    [SerializeField] private int healthPerTick = 10; // how much health it restores per tick
    
    private List<TankPlayer> playersInZone = new List<TankPlayer>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!IsServer) return; // Only the server should handle healing

        if(!col.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player)) {return;} // Check if the collider is a player

        playersInZone.Add(player); // Add the player to the list of players in the zone

        Debug.Log($"Player {player.PlayerName.Value} entered the healing zone"); // Log the player entering the zone
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!IsServer) return; // Only the server should handle healing
        
        if(!col.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player)) {return;} // Check if the collider is a player

        playersInZone.Remove(player); // Remove the player to the list of players in the zone
    
        Debug.Log($"Player {player.PlayerName.Value} left the healing zone"); // Log the player entering the zone

    }

}
