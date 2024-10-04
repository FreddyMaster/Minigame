using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 50;
    public int shootforce = 3000;
    public float fireRate = 5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnpoint;
    public float projectileDamage = 10f;
    private float lastShotTime = 0f; // Time of the last shot
    private Animator animator;
    private float vel;
    private Vector3 oldPosition;
    private Transform character;

    private void Start()
    {
        animator = GetComponent<Animator>();
        character = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        // Reset inputs
        horizontalInput = 0;
        verticalInput = 0;

        // Check for WASD keys for movement
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1;
            character.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1;
            character.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
            character.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
            character.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }

        // Move the player
        MovePlayer(new Vector3(horizontalInput, 0, verticalInput));

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("isAttacking", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("isAttacking", false);

        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("isAttacking", false);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("isAttacking", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ShootProjectile(Vector3.right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ShootProjectile(Vector3.left);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ShootProjectile(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ShootProjectile(Vector3.back);
        }


    }

    void FixedUpdate()
    {
        vel = Vector3.Distance(oldPosition, transform.position) * 4f;
        oldPosition = transform.position;
        animator.SetFloat("Speed", vel);
    }
    void MovePlayer(Vector3 direction)
    {
        // Normalize the direction vector to ensure consistent movement speed
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        // Apply movement
        transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    void ShootProjectile(Vector3 direction)
    {
        // Shooting projectiles with arrow keys
        // Check if enough time has passed since the last shot
        if (Time.time >= lastShotTime + 1f / fireRate)
        {
            animator.SetBool("isAttacking", true);
            Vector3 spawnPosition = projectileSpawnpoint.position + direction * 3f + Vector3.up * 0.5f;
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * shootforce);
            }

            Projectile projectileScript = projectile.AddComponent<Projectile>();
            projectileScript.damage = projectileDamage;
            lastShotTime = Time.time; // Update the time of the last shot
        }
        else
        {
            return;
        }
    }
}
