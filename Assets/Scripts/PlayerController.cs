using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 50;
    public int shootforce = 3000;
    public GameObject projectilePrefab;
    public float projectileDamage = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShootProjectile(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShootProjectile(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShootProjectile(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShootProjectile(Vector3.back);
        }

        // Move player on horizontal and vertical inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    void ShootProjectile(Vector3 direction)
    {
        Vector3 spawnPosition = transform.position + direction * 2f + Vector3.up * 0.5f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * shootforce);
        }

        Projectile projectileScript = projectile.AddComponent<Projectile>();
        projectileScript.damage = projectileDamage;
    }
}
