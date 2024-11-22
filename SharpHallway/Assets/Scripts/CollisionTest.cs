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
    [SerializeField] public AK.Wwise.Event akThud3;
    [SerializeField] public AK.Wwise.Event hitReaction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door" || collision.gameObject.tag == "Book")
        {
            akThud2.Post(gameObject);
            hitReaction.Post(gameObject);
        }
        if (collision.gameObject.tag == "Hallway/Walls/RightWall" || collision.gameObject.tag == "Hallway/Walls/LeftWall" || collision.gameObject.tag == "Hallway/Walls/FarWall" || collision.gameObject.tag == "Hallway/Walls/CloseWall")
        {
            UnityEngine.Debug.Log("Thud1 Event Triggered");
            akThud1.Post(gameObject);
            hitReaction.Post(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
