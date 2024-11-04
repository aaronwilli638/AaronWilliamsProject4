using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ClickSeekBehavior : MonoBehaviour
{
    public float speed = 5f;         // Speed of the object
    private Vector3 targetPosition;   // Current target position
    private bool isMoving = false;    // Flag to check if the object should be moving

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
            isMoving = true; // Start moving towards the new target
        }

        // If moving, call the Seek method
        if (isMoving)
        {
            Seek(targetPosition);
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

    void Seek(Vector3 target)
    {
        // Calculate the direction to the target
        Vector3 direction = (target - transform.position).normalized;

        // Move towards the target
        transform.position += direction * speed * Time.deltaTime;

        // Stop moving if close to the target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // You can set isMoving to false here if you want to stop
            // or keep it true if you want to continue seeking new targets
            isMoving = false; // Optional: uncomment to stop seeking after reaching the target
        }
    }
}


