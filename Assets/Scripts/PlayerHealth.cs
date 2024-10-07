using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public UnityEvent<float> OnHealthChanged;
    private Animator animator;
    public GameOverScreen gameOverScreen;
    private ScoreManager scoreManager;
    private PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }

        currentHealth = maxHealth;
        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(currentHealth);
        }

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found!");
        }
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("playerController component not found!");
        }
    }

    public void TakeDamage(float damage)
    {


        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        } else if (animator != null)
            {
                animator.SetTrigger("takingDamage");
            }
        }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetBool("Dead", true);
        }
        if (gameOverScreen != null && scoreManager != null)
        {
            gameOverScreen.Setup(scoreManager.GetScore());
        }
        else
        {
            Debug.LogError("GameOverScreen or ScoreManager is not assigned!");
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }
}
