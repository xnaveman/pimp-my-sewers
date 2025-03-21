using UnityEngine;

public class RatSpin : MonoBehaviour
{
    // Rotation speed in degrees per second on the z-axis.
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate the GameObject continuously on the global z-axis.
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}