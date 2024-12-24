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

    private Dictionary<int, KeyEvent> collectedKeys = new();

    //Accessors
    public GameObject Player { get { return player; } }
    public int PlayerDamage { get { return playerStats.playerDamage; } }
    public float PlayerFireRate { get { return playerStats.harpoonFireRate; } }
    public float PlayerReelInSpeed { get { return playerStats.playerReelInSpeed; } }
    public float PlayerMovementMultiplier { get { return playerStats.movementSpeedMultiplier; } }

    const int DAMAGE = 1;
    const float FIRERATE = 30f;
    const float REELINSPEED = 20f;
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

    public void AddKey(KeyEvent ctx)
    {
        //Prevent duplicate keys
        foreach (KeyEvent key in collectedKeys.Values)
        {
            if (key.keyID == ctx.keyID)
                return;
        }

        collectedKeys.Add(ctx.keyID, ctx);
    }

    /// <summary>
    /// Called by gates to check if the player has obtained the key
    /// </summary>
    /// <param name="ctx"></param>
    /// <returns></returns>
    public bool KeyCheck(KeyEvent ctx)
    {
        //Check if key is collected
        foreach (KeyEvent key in collectedKeys.Values)
        {
            if (key.keyID == ctx.keyID)
            {
                return true;
            }
        }

        return false;
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