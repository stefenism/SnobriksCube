using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 100)]
    public float maxHealth = 100;
    private float currentHealth = 100;

    private PlayerController player;

    void Awake()
    {
        currentHealth = maxHealth;
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        Mathf.Clamp(currentHealth, 0, 100);
        if(currentHealth <= 0)
            player.GetPlayerState().setPlayerDead();
    }

    public void addHealth(float addToHealth)
    {
        currentHealth += addToHealth;
        Mathf.Clamp(currentHealth, 0, 100);    
    }

    public float getCurrentHealth(){return currentHealth;}
}
