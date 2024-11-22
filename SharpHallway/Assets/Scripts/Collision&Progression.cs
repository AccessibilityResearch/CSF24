using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;
using System.Diagnostics;

public class NewBehaviourScript : MonoBehaviour
{
    //[SerializeField] public AK.Wwise.Event akOOFEvent;
    [SerializeField] public AK.Wwise.Event akThud1;
    [SerializeField] public AK.Wwise.Event akThud2;
    [SerializeField] public AK.Wwise.Event akThud3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door" || collision.gameObject.tag == "Book")
        {
            akThud2.Post(gameObject);
        }
        if (collision.gameObject.tag == "Hallway/Walls/RightWall" || collision.gameObject.tag == "Hallway/Walls/LeftWall" || collision.gameObject.tag == "Hallway/Walls/FarWall" || collision.gameObject.tag == "Hallway/Walls/CloseWall")
        {
           
            akThud1.Post(gameObject);
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
