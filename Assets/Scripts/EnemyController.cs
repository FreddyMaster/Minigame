using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float fireRate = 1f; // Rate of fire
    public Transform firePoint;
    private float attackRange = 10f;
    public int shootforce = 3000;

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

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= attackRange)
            {
                // Attack the target
                FaceTarget();
             
                Shoot();
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
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position * 3f, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = shootDirection * 10f; // Adjust the speed as needed
        Debug.Log("Projectile velocity: " + rb.velocity);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
