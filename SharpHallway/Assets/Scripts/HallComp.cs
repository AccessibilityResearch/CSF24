using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HallFaceEnum
{
    Left = 0,
    Far = 1,
    Ceiling = 2,

    Right = 3,
    Close = 4,
    Floor = 5
}

//[ExecuteInEditMode]
public class HallwayComp : MonoBehaviour
{
    [SerializeField] private HallFaceEnum face;
    public HallFaceEnum Face { get { return face; } set { face = value; } }


    private int xScale, zScale = 1;

    private Renderer rend;


    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    public void Update()
    {
        switch ((int)Face)
        {
            case (< 3):
                xScale = 1;
                break;

            case (> 3):
                xScale = -1;
                break;
        }
        if (rend != null)
        {
            //rend.sharedMaterial.mainTextureScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.z) * 2.5f;
        }
    }
}
