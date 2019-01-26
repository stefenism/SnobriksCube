using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0, 100)]
    public float maxHealth = 100;
    private float currentHealth = 100;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void addHealth(float addToHealth){currentHealth += addToHealth;}
}
