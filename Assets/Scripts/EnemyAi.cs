using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform playerTransform; // New: Reference to the player's transform

    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private float projectileSpeed = 32f;

    private float currentHealth;
    private bool canAttack = true;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private void Awake()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform is not set. Please assign it in the inspector.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);

        if (canAttack)
        {
            FireProjectile();
            canAttack = false;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || projectileSpawnPoint == null) return;

        Vector3 direction = (playerTransform.position - projectileSpawnPoint.position).normalized;
        GameObject projectileObj = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = projectileObj.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing a Rigidbody component.");
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add any death effects or animations here
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
