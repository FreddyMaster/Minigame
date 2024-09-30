using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float fireRate = 1f; // Rate of fire (shots per second)
    public Transform firePoint;
    private float attackRange = 10f;
    public int shootforce = 3000;
    public float projectileDamage = 10f;


    private float lastShotTime = 0f; // Time of the last shot

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

            agent.SetDestination(target.position);

            if (distance <= attackRange)
            {
                // Attack the target
                FaceTarget();

                // Check if enough time has passed since the last shot
                if (Time.time >= lastShotTime + 1f / fireRate)
                {
                    Shoot();
                    lastShotTime = Time.time; // Update the time of the last shot
                }
            }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        Debug.Log("Shooting at target");

        // Calculate the direction to the target
        Vector3 directionToTarget = (target.position - firePoint.position).normalized;

        // Determine the closest cardinal direction
        Vector3 shootDirection = Vector3.zero;
        if (Mathf.Abs(directionToTarget.x) > Mathf.Abs(directionToTarget.z))
        {
            shootDirection = directionToTarget.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            shootDirection = directionToTarget.z > 0 ? Vector3.forward : Vector3.back;
        }

        // Instantiate and shoot the projectile
        Vector3 spawnPosition = transform.position + shootDirection * 3f + Vector3.up * 0.5f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootDirection * shootforce);
        }

        Projectile projectileScript = projectile.AddComponent<Projectile>();
        projectileScript.damage = projectileDamage;
        Debug.Log("Projectile velocity: " + rb.velocity);
    }
}
