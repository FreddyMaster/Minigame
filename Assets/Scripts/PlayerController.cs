using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 50;
    public int shootforce = 3000;
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x + 1f, 0.5f, transform.position.z), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(shootforce, 0));

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x - 3f, 0.5f, transform.position.z), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(-shootforce, 0));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            //Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x - 1f, 0.5f, transform.position.z + 2f), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shootforce));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            //Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x - 1f, 0.5f, transform.position.z - 2f), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -shootforce));
        }

        //Move player on horizontaL Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

    }
}