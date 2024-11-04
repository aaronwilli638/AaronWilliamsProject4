using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWanderBehavior : MonoBehaviour
{
    public float speed = 3f;             // Speed of the object
    public float changeDirectionInterval = 2f; // Time in seconds to change direction
    public float wanderRadius = 5f;      // Radius within which to wander

    private Vector3 wanderTarget;         // Target position for wandering
    private float timer;                  // Timer to track direction changes

    void Start()
    {
        // Initialize the first target position
        SetNewWanderTarget();
    }

    void Update()
    {
        Wander();
    }

    void Wander()
    {
        // Move towards the target position
        Vector3 direction = (wanderTarget - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if the object is close to the target position
        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
        {
            // Set a new wander target after the interval
            timer += Time.deltaTime;
            if (timer >= changeDirectionInterval)
            {
                SetNewWanderTarget();
                timer = 0; // Reset the timer
            }
        }
    }

    void SetNewWanderTarget()
    {
        // Set a new random target position within the defined radius
        Vector3 randomOffset = new Vector3(
            Random.Range(-wanderRadius, wanderRadius),
            0, // Keep it on the XZ plane; adjust if needed
            Random.Range(-wanderRadius, wanderRadius)
        );

        wanderTarget = transform.position + randomOffset;
    }
}

