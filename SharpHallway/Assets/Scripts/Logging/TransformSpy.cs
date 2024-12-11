using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSpy : MonoBehaviour
{
    private static TransformSpy instance;
    public static TransformSpy Instance { get{ return instance; } private set{ instance = value; } }

	private static GameObject body; //body is a bit of a misnomer, it means 'body' being tracked... Not necessarily the 'body' of the player. 
	public static GameObject Body { get{ return body; } private set{ body = value; } }

	private static Transform transfourm; //disgusting name
	public static Transform Transfourm { get{ return transfourm; } private set { transfourm = value; } }

	//I named all of these.
	//I hope all of these comments bring you as much joy as they bring to me (not a ton but it's there).

	public void Awake()
	{
		Instance = this;
		Body = gameObject;
		Transfourm = gameObject.transform;
	}

	public void Start()
	{
		if(Body == null){ throw new MissingReferenceException("TransformSpy: TransformSpy must be attached to player camera for logging to occur."); }
	}


}