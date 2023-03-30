using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;

public class AgentMind : MonoBehaviour
{
    private Agent agent;
    private Vector3 lastPosition;
    private float stopTimeThreshold = 1f;
    private float timeStopped;
    private Vector3 destination;
    private bool isMoving = false;

    private void Start()
    {
        // Get a reference to the Agent component
        agent = GetComponent<Agent>();

        // Initialize last position to current position
        lastPosition = transform.position;

        // Set initial destination to a random position
        destination = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
        agent.Navigate(destination);
    }

    private void Update()
    {
        // If agent is not moving, increment stop time counter
        if (!isMoving)
        {
            timeStopped += Time.deltaTime;

            // If agent has been stopped for more than stop time threshold, generate a new destination
            if (timeStopped >= stopTimeThreshold)
            {
                destination = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
                agent.Navigate(destination);

                // Reset stop time counter
                timeStopped = 0f;
            }
        }
        else
        {
            // If agent is moving, reset stop time counter and update last position
            timeStopped = 0f;
            lastPosition = transform.position;
        }

        // Check if agent has reached its destination
        if (Vector3.Distance(transform.position, destination) <= 1.5f)
        {
            // Set isMoving to false
            isMoving = false;
        }
        else
        {
            // Set isMoving to true
            isMoving = true;
        }
    }
}
