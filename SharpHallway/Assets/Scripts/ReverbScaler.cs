using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// in order to use this correctly, drag the hallway object into the pastel field of this script in the inspector
[ExecuteInEditMode]
public class ReverbScaler : MonoBehaviour
{
    public Pastel pastel; // Reference to the Pastel script

    private Vector3 previousScale;  // To track the previous scale
    private Vector3 previousPosition; // To track the previous position

    void Update()
    {
        if (pastel != null)
        {
            // Calculate the new scale
            Vector3 newScale = new Vector3(
                pastel.Width, 
                pastel.Height, 
                pastel.Length
            );

            // Calculate the new position
            Vector3 newPosition = new Vector3(
                0, 
                0.5f * pastel.Height, 
                0.5f * pastel.Length
            );

            // Apply the scale only if it has changed
            if (previousScale != newScale)
            {
                transform.localScale = newScale;
                previousScale = newScale;
            }

            // Apply the position only if it has changed
            if (previousPosition != newPosition)
            {
                transform.position = newPosition;
                previousPosition = newPosition;
            }
        }
    }
}

