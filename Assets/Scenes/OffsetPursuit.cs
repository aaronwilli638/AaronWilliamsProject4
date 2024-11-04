using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursuitMouseBehavior : MonoBehaviour
{
    public float speed = 5f;          // Speed of the pursuer
    public float desiredOffset = 2f;   // Desired distance to maintain from the mouse

    void Update()
    {
        PursueMouse();
    }

    void PursueMouse()
    {
        // Get the current mouse position in world space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set the z position for the camera
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
        targetPosition.z = 0; // Assuming a 2D plane; adjust as necessary

        // Calculate the direction to the target (mouse position)
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Calculate the desired position based on the offset
        Vector3 desiredPosition = targetPosition - direction * desiredOffset;

        // Calculate the direction to the desired position
        Vector3 pursuitDirection = (desiredPosition - transform.position).normalized;

        // Move towards the desired position
        transform.position += pursuitDirection * speed * Time.deltaTime;
    }
}

