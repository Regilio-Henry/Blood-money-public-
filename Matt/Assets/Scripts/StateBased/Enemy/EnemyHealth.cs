using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Gets called on by the AI script for the enemy. 
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 2;
    public int currentHealth;
    //public float flashSpeed = 5f; 
    //public Color flashCOlour = new Color(1f,0f,0f,0.1f);
    
    bool isDead;

    void Awake()
    {
        currentHealth = startingHealth; 
    }

    void Update()
    {
        if (isDead)
        {
            destory.gameObject;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death(); 
        }
    }

    void Death()
    {
        isDead = true; 

        
    }
}
