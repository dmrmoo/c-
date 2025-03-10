using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player's transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset; // Offset to keep the camera at a fixed position relative to the player
    private float fixedYPosition;


    void Start()
    {
        fixedYPosition = transform.position.y;

    }


    void LateUpdate()
    {
        // follows the x but keeps the y fixed
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, fixedYPosition, player.position.z);

        // Smoothly move towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
