﻿using System.Collections;
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
        if(currentHealth <= 0)
            player.GetPlayerState().setPlayerDead();
    }

    public void addHealth(float addToHealth){currentHealth += addToHealth;}
}
