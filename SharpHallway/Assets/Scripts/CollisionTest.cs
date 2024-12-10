using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AK.Wwise;
using System.Diagnostics;

public class CollisionTest : MonoBehaviour
{
    [SerializeField] public AK.Wwise.Event akThud1;
    [SerializeField] public AK.Wwise.Event akThud2;
    [SerializeField] public AK.Wwise.Event hitReaction;

    private Rigidbody rb;

    // Initialize Rigidbody to get player velocity
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure correct hit sound based on the collision object
        if (collision.gameObject.CompareTag("Door") || collision.gameObject.CompareTag("Book"))
        {
            akThud2.Post(gameObject);
            hitReaction.Post(gameObject);
        }
        else if (collision.gameObject.CompareTag("Hallway/Walls/RightWall") || collision.gameObject.CompareTag("Hallway/Walls/LeftWall") ||
                 collision.gameObject.CompareTag("Hallway/Walls/FarWall") || collision.gameObject.CompareTag("Hallway/Walls/CloseWall"))
        {
            UnityEngine.Debug.Log("Thud1 Event Triggered");
            akThud1.Post(gameObject);
            hitReaction.Post(gameObject);
        }

        // Set panning and volume based on collision
        SetCollisionPanAndVolume(collision);
    }

    private void SetCollisionPanAndVolume(Collision collision)
    {
        // Direction from the player to the collided object
        Vector3 directionToObject = collision.contacts[0].point - transform.position;

        // Get the player's forward direction (ignoring y-axis rotation)
        Vector3 playerForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        // Calculate the angle between the player's forward direction and the direction to the collision point
        float angle = Vector3.SignedAngle(playerForward, directionToObject.normalized, Vector3.up);

        // Initialize the panning values
        float leftRightPan = 0f;
        float frontRearPan = 0f;

        // If the angle is small, we assume we're hitting the front or back (top or bottom of the obstacle)
        if (Mathf.Abs(angle) < 45f)
        {
            // For top or bottom collisions, always use both speakers (pan to center)
            frontRearPan = Mathf.Sign(directionToObject.z) * 100f;  // Front or back panning
        }
        else if (angle > 0f)  // If the player is facing the right side of the object
        {
            leftRightPan = 100f;  // Pan to the right
        }
        else  // If the player is facing the left side of the object
        {
            leftRightPan = -100f;  // Pan to the left
        }

        // Set RTPC values for panning
        AkSoundEngine.SetRTPCValue("LRCollisionPan", leftRightPan);  // Apply left-right panning
        AkSoundEngine.SetRTPCValue("FRCollisionPan", frontRearPan);  // Apply front-rear panning
    }



}