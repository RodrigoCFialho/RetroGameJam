using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float maxHealth;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image dashBar;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GetStartingHealth(float maxHP)
    {
        maxHealth = maxHP;
    }

    public void UpdateHealthUi(float currentHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        Debug.Log("HP: " + healthBar.fillAmount);
    }

    public void UpdateDashUI(float dashPercentage) 
    {
        dashBar.fillAmount = dashPercentage;
    }

    public void PauseGame() 
    {
        if(!isPaused) {
            Time.timeScale = 0f;
            isPaused = true;
        } else {
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
