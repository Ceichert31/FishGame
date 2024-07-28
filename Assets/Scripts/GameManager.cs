using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Manager References")]
    [SerializeField] private GameObject player;

    private int playerDamage = 5;

    //Accessors
    public GameObject Player { get { return player; } }

    public int PlayerDamage { get { return playerDamage; } }


    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    /// <summary>
    /// Increases the players damage stat
    /// </summary>
    public void IncreaseDamageStat(int amount)
    {
        playerDamage += amount;
    }
}
