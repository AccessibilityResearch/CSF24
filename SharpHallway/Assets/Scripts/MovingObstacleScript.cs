using System;
using System.Collections;
using UnityEngine;

public class MovingObstacleScript : MonoBehaviour
{
    private bool isRespawning = false;
    private Renderer objRenderer;

    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public float RespawnDelay; // time before respawning
    public float Speed;
    public bool BackNForth;

    public string EventName;
    public uint EventId;


    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        transform.position = StartPoint; // Initialize position at start point
    }

    private void Update()
    {
        if (StartPoint == null || EndPoint == null)
        {
            Debug.LogWarning("Start or End point is not assigned!");
            return;
        }

        if (BackNForth == true)
        {
            // Move the object back and forth between startPoint and endPoint
            transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.PingPong(Time.time * Speed, 1.0f));
        }
        else if (!isRespawning)
        {
            if (Vector3.Distance(transform.position, EndPoint) < 0.1f) // Check if near endPoint
            {
                StartCoroutine(Respawn());
            }
            else
            {
                // Move from startPoint to endPoint only
                transform.position = Vector3.MoveTowards(transform.position, EndPoint, Speed * Time.deltaTime);
            }
        }
    }

    private IEnumerator Respawn()
    {
        isRespawning = true;

        objRenderer.enabled = false;    // Hide the object
        transform.position = StartPoint;   // Move the object to startPoint
        yield return new WaitForSeconds(RespawnDelay);  // Wait for the respawn delay
        objRenderer.enabled = true; // Show the object

        isRespawning = false;
    }
}