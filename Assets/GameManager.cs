using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float startingHealth;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image dashBar;

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

    public void GetStartingHealth(float startingHP)
    {
        startingHealth = startingHP;
    }

    public void UpdateHealthUi(float currentHealth)
    {
        healthBar.fillAmount = currentHealth / startingHealth;
    }

    public void UpdateDashUI(float dashPercentage) {
        dashBar.fillAmount = dashPercentage;
    }
}
