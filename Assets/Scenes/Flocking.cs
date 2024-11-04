using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class FlockingBehavior : MonoBehaviour
{
    public float speed = 5f;                      // Speed of the flocking objects
    public float separationDistance = 1.5f;        // Minimum distance to maintain from other objects
    public float alignmentWeight = 1f;             // Weight for alignment behavior
    public float cohesionWeight = 1f;              // Weight for cohesion behavior
    public float cohesionRadius = 5f;              // Radius for cohesion
    public float maxForce = 2f;                    // Maximum steering force

    private List<Transform> flock;                 // List of all flocking objects

    void Start()
    {
        // Initialize flock with other objects in the scene
        flock = new List<Transform>(GameObject.FindObjectsOfType<FlockingBehavior>().Length);
        foreach (var flockingObject in GameObject.FindObjectsOfType<FlockingBehavior>())
        {
            if (flockingObject.transform != transform)
            {
                flock.Add(flockingObject.transform);
            }
        }
    }

    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        // Apply separation, alignment, and cohesion
        acceleration += Separation() * separationDistance;
        acceleration += Alignment() * alignmentWeight;
        acceleration += Cohesion() * cohesionWeight;

        // Update position and velocity
        Vector3 velocity = (transform.forward * speed) + acceleration;
        transform.position += velocity * Time.deltaTime;

        // Optional: Make the object look in the direction of movement
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

    Vector3 Separation()
    {
        Vector3 steer = Vector3.zero;
        int count = 0;

        foreach (Transform other in flock)
        {
            float distance = Vector3.Distance(transform.position, other.position);
            if (distance < separationDistance)
            {
                Vector3 diff = (transform.position - other.position).normalized / distance; // Weight by distance
                steer += diff;
                count++;
            }
        }

        if (count > 0)
        {
            steer /= count; // Average steer
        }

        return Limit(steer);
    }

    Vector3 Alignment()
    {
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (Transform other in flock)
        {
            sum += other.forward; // Use forward direction of each object
            count++;
        }

        if (count > 0)
        {
            sum /= count; // Average direction
            return Limit(sum).normalized; // Normalize the direction
        }

        return Vector3.zero;
    }

    Vector3 Cohesion()
    {
        Vector3 centerOfMass = Vector3.zero;
        int count = 0;

        foreach (Transform other in flock)
        {
            float distance = Vector3.Distance(transform.position, other.position);
            if (distance < cohesionRadius)
            {
                centerOfMass += other.position;
                count++;
            }
        }

        if (count > 0)
        {
            centerOfMass /= count;
            Vector3 steer = (centerOfMass - transform.position).normalized;
            return Limit(steer);
        }

        return Vector3.zero;
    }

    Vector3 Limit(Vector3 vector)
    {
        if (vector.magnitude > maxForce)
        {
            return vector.normalized * maxForce; // Limit to max steering force
        }
        return vector;
    }
}

