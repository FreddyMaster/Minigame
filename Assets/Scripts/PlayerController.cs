using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 35;
    public int shootforce;
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Makes sure the player character does not go off the screen by placing it back to the min x position 
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.z, transform.position.y);
        }
        //Makes sure the player character does not go off the screen by placing it back to the max x position 
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.z, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x + 1f, 0.5f, transform.position.z), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(1000, 0));

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Launch a projectile from the player
            GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x - 3f, 0.5f, transform.position.z), transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(new Vector3(-1000, 0));

        }

        //Move player on horizontaL Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }
}