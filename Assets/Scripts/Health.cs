using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour {
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

    private SpriteRenderer spriteRenderer;

    private float currentHP;
    private float lastDamageTime = -Mathf.Infinity;
    private float totalHealthRecovered = 0;

    private bool invulnerable = false;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start() 
    {
        currentHP = startingHP;
        GameManager.Instance.GetStartingHealth(startingHP);
        InvokeRepeating(nameof(DrainHP), drainInterval, drainInterval);
    }

    public void SetInvulnerability(bool invulnerability) 
    {
        invulnerable = invulnerability;
    }

    private void DrainHP() 
    {
        // Damage over time (game mechanic)
        TakeDamage(drainHP);
    }

    public void TakeDamage(float damage) 
    {

        if(damage > 2) 
        {
            StartCoroutine(Blink());
        }

        if(!invulnerable) 
        {
            if(Time.time - lastDamageTime < damageCooldown) {
                return;
            }

            lastDamageTime = Time.time;
            currentHP -= damage;

            GameManager.Instance.UpdateHealthUi(currentHP);

            if(currentHP < 0) 
            {
                currentHP = 0;
                Die();
            }
        }
    }

    private IEnumerator Blink() 
    {
        int numberOfBllinks = 0;
        while(numberOfBllinks < 4) 
        {
            numberOfBllinks++;
            spriteRenderer.color = new Color(1, 1, 1, 0.1f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
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
