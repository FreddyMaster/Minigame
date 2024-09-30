using UnityEngine;

public class BirdsEyeCameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset from the player

    void Start()
    {
        // Initialize the offset based on the initial positions of the camera and player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the camera's position to follow the player while maintaining the offset
        if (player != null)
        {
            Vector3 newPosition = player.position + offset;
            transform.position = newPosition;
        }
    }
}
