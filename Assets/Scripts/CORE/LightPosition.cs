using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPosition : MonoBehaviour
{
    // Assign the target (e.g., your camera) in the Inspector.
    public Transform target;

    // Optional: add offset if you need to adjust the light's rotation relative to the target.
    public Vector3 rotationOffset;

    void Update()
    {
        if (target != null)
        {
            // Get the full Euler angles from the target.
            Vector3 targetEuler = target.rotation.eulerAngles;
            // Apply the rotation offset.
            targetEuler += rotationOffset;
            // Set the light's rotation to track both x and y rotation.
            transform.eulerAngles = targetEuler;
            // Optionally, set the light's position to the target's position.
            transform.position = target.position;
        }
    }
}

