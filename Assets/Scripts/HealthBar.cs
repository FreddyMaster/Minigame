using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerHealth playerHealth;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }

        if (healthBar == null)
        {
            healthBar = GetComponent<Slider>();
        }

        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        Debug.Log("HealthBar initialized. Max Health: " + playerHealth.maxHealth);

        // Subscribe to the OnHealthChanged event
        playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the HealthBar is destroyed
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }

    private void UpdateHealthBar(float health)
    {
        healthBar.value = health;
        Debug.Log("HealthBar updated. Current Health: " + health);
    }
}
