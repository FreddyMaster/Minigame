using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private ScoreManager scoreManager;

    public UnityEvent<float> OnHealthChanged;

    public int scoreValue = 1;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged.Invoke(currentHealth);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        OnHealthChanged.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add score when enemy dies
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }
        else
        {
            Debug.LogWarning("ScoreManager not found. Score not updated.");
        }

        Destroy(gameObject);
    }
}
