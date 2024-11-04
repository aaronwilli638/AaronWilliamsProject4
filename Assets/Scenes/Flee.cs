using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class FleeBehavior : MonoBehaviour
{
    public float speed = 5f;         // Speed of the object
    private Vector3 targetPosition;   // Current target position
    private bool isFleeing = false;   // Flag to check if the object should be fleeing

    void Start()
    {
        // Initialize target position to the object's starting position
        targetPosition = transform.position;
    }

    void Update()
    {
        // Check for mouse input to update the target position
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTargetPosition();
            isFleeing = true; // Start fleeing from the new target
        }

        // If fleeing, call the Flee method
        if (isFleeing)
        {
            Flee(targetPosition);
        }
    }

    void UpdateTargetPosition()
    {
        // Convert mouse position to world point
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set the z position for the camera
        targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
        targetPosition.z = 0; // Assuming a 2D plane; adjust as necessary
    }

    void Flee(Vector3 target)
    {
        // Calculate the direction away from the target
        Vector3 direction = (transform.position - target).normalized;

        // Move away from the target
        transform.position += direction * speed * Time.deltaTime;

        // Optional: Add logic to stop fleeing if needed
        // For example, you could set isFleeing to false based on a distance condition.
    }
}

