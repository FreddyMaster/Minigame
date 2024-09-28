using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit! Damage applied: " + damage);
            }
        }
        else
        {
            // Damage the enemy
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Enemy hit! Damage applied: " + damage);
            }
        }

        // Destroy the projectile after collision
        Destroy(gameObject);
    }
}
