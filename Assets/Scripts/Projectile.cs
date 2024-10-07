using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        bool shouldDestroy = false;

        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                shouldDestroy = true; // Set flag to destroy projectile
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            // Damage the enemy
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                shouldDestroy = true; // Set flag to destroy projectile
            }
        }

        // Destroy the projectile only if it hit a player or an enemy
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }
}
