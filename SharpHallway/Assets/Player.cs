using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Player : MonoBehaviour
{
	private static Player instance;
	public static Player Instance { get { return instance; } set{ instance = value; } }

    private HashSet<string> collisions = new HashSet<string>();
	private HashSet<string> prevCollisions = new HashSet<string>();

    private FPSController controller;
	
	private bool fin = false;
	public bool Fin { set { fin = value; } }

	public void Awake()
	{
		Instance = this;
		controller = gameObject.GetComponent<FPSController>();
	}

	public IEnumerator ResetPos()
	{
		//make sure that we are not using vr
		if(controller != null) { controller.enabled = false; }
		Vector3 closeWallPosition = Pastel.Instance.Close.transform.position; // Respawning position set to the "Close" Wall
		gameObject.transform.position = new Vector3(closeWallPosition.x, 1, closeWallPosition.z); // position.y has to be 1 (floor level)

		yield return new WaitForSeconds(0.1f); //This is needed so that the character actually gets moved (unity issue prob)
		if(controller != null) { controller.enabled = true; }
		fin = false;
	}

    public void FixedUpdate() {
		foreach(var v in collisions) {
			if(!prevCollisions.Contains(v)) {
				Debug.Log(v);
				LogManager.Instance.LogCollision(v); 
			}
		}

		prevCollisions.Clear();

		foreach(var v in collisions) {
			prevCollisions.Add(v);
		}

		collisions.Clear();
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
	{
		string name = hit.gameObject.name;
		collisions.Add(name);
	}
	

    public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "FinishZone") {
			if (!fin) {
				fin = true;
				GameManager.Instance.TriggerLevelTransition();
			}
		}
	}
}
