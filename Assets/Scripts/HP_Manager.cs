using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Manager : MonoBehaviour
{
    [SerializeField]
    private float startingHP = 100;

    [SerializeField]
    private float maxHP = 500;

    [SerializeField]
    private float drainHP = 2;

    [SerializeField]
    private float drainInterval = 1f;

    [SerializeField]
    private float damageCooldown = 0.5f;

    private float currentHP;
    private float lastDamageTime = -Mathf.Infinity;

    // Start is called before the first frame update
    private void Start()
    {
        currentHP = startingHP;
        InvokeRepeating(nameof(DrainHP), drainInterval, drainInterval);
    }

    private void DrainHP() 
    {
        // Damage over time (game mechanic)
        currentHP -= drainHP;
    }

    public void TakeDamage(float damage) 
    {
        if(Time.time - lastDamageTime < damageCooldown) {
            return;
        }

        lastDamageTime = Time.time;
        currentHP -= damage;

        Debug.Log("Player HP: " + currentHP);

        if(currentHP < 0) {
            currentHP = 0;
            Die();
        }
    }

    public void RegenHP(float amount) 
    {
        currentHP += amount;
        if(currentHP > maxHP) {
            currentHP = maxHP;
        }
    }

    public void Die() 
    {
        Debug.Log("Player died!");

        // death code?? idk
    }
}
