using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EvadeBehavior : MonoBehaviour
{
    public float speed = 5f;            // Speed of the object
    private Vector3 evadePosition;       // Position to evade from
    private bool isEvading = false;      // Flag to check if the object should be evading

    void Update()
    {
        // Check for mouse input to update the evade position
        if (Input.GetMouseButtonDown(0))
        {
            UpdateEvadePosition();
            isEvading = true; // Start evading from the new position
        }

        // If evading, call the Evade method
        if (isEvading)
        {
            Evade(evadePosition);
        }
    }

    void UpdateEvadePosition()
    {
        // Convert mouse position to world point
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set the z position for the camera
        evadePosition = Camera.main.ScreenToWorldPoint(mousePos);
        evadePosition.z = 0; // Assuming a 2D plane; adjust as necessary
    }

    void Evade(Vector3 target)
    {
        // Calculate the direction away from the target
        Vector3 direction = (transform.position - target).normalized;

        // Move away from the target
        transform.position += direction * speed * Time.deltaTime;

        // Optional: You can add a condition to stop evading after a certain distance or time
        if (Vector3.Distance(transform.position, target) > 5f) // Example condition
        {
            isEvading = false; // Stop evading if far enough away
        }
    }
}

