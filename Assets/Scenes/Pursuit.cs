using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PursuitMouseBehavior : MonoBehaviour
{
    public float speed = 5f;         // Speed of the pursuer
    public float predictionTime = 0.5f; // Time to predict future mouse position

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

        // Predict future position based on mouse speed (optional)
        Vector3 mouseVelocity = (targetPosition - transform.position).normalized * speed * predictionTime;
        Vector3 predictedPosition = targetPosition + mouseVelocity;

        // Calculate the direction to the predicted position
        Vector3 direction = (predictedPosition - transform.position).normalized;

        // Move towards the predicted position
        transform.position += direction * speed * Time.deltaTime;
    }
}


