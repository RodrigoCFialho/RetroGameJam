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
    private float totalHealthRecovered = 0;

    private bool invulnerable = false;

    // Start is called before the first frame update
    private void Start()
    {
        currentHP = startingHP;
        GameManager.Instance.GetStartingHealth(maxHP);
        InvokeRepeating(nameof(DrainHP), drainInterval, drainInterval);
    }

    public void SetInvulnerability(bool invulnerability) {
        invulnerable = invulnerability;
    }

    private void DrainHP() 
    {
        // Damage over time (game mechanic)
        TakeDamage(drainHP);
    }

    public void TakeDamage(float damage) 
    {
        if(!invulnerable) {
            if(Time.time - lastDamageTime < damageCooldown) {
                return;
            }

            lastDamageTime = Time.time;
            currentHP -= damage;

            GameManager.Instance.UpdateHealthUi(currentHP);

            if(currentHP < 0) {
                currentHP = 0;
                Die();
            }
        }
    }

    public void RegenHP(float amount) 
    {
        totalHealthRecovered += amount;
        currentHP += amount;
        if(currentHP > maxHP) {
            currentHP = maxHP;
        }

        GameManager.Instance.UpdateHealthUi(currentHP);
    }

    public void Die() 
    {
    }
}
