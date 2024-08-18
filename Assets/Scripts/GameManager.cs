using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Manager References")]
    [SerializeField] private GameObject player;

    private PlayerStats playerStats;

    public bool firstTimeLoading = true;

    [SerializeField] private bool hasGateKey;

    //Accessors
    public bool HasGateKey { get { return hasGateKey; } set { hasGateKey = value; } }
    public GameObject Player { get { return player; } }
    public int PlayerDamage { get { return playerStats.playerDamage; } }
    public float PlayerFireRate { get { return playerStats.harpoonFireRate; } }
    public float PlayerReelInSpeed { get { return playerStats.playerReelInSpeed; } }
    public float PlayerMovementMultiplier { get { return playerStats.movementSpeedMultiplier; } }

    const int DAMAGE = 5;
    const float FIRERATE = 20f;
    const float REELINSPEED = 15f;
    const float MOVEMENTMULTIPLIER = 1f;


    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Initialize stats
        playerStats = new PlayerStats(DAMAGE, FIRERATE, REELINSPEED, MOVEMENTMULTIPLIER);
    }

    /// <summary>
    /// Increases the players damage stat
    /// </summary>
    public void IncreaseDamage(int amount)
    {
        playerStats.playerDamage += amount;
    }

    public void IncreaseFireRate(float amount)
    {
        playerStats.harpoonFireRate += amount;
    }

    public void IncreaseReelInSpeed(float amount)
    {
        playerStats.playerReelInSpeed += amount;
    }

    public void IncreasePlayerSpeed(float amount)
    {
        playerStats.movementSpeedMultiplier += amount;
    }
}
public struct PlayerStats
{
    public PlayerStats(int damage, float fireRate, float reelInSpeed, float speedMultiplier)
    {
        playerDamage = damage;
        harpoonFireRate = fireRate;
        playerReelInSpeed = reelInSpeed;
        movementSpeedMultiplier = speedMultiplier;
    }

    public int playerDamage;
    public float harpoonFireRate;
    public float playerReelInSpeed;
    public float movementSpeedMultiplier;
}