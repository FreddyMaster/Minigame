using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public UnityEvent<float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged.Invoke(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        OnHealthChanged.Invoke(currentHealth);
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}
