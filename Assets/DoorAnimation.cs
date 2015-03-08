using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider collider)
	{
		animator.SetBool ("Open", false);
	}

	void OnTriggerEnter(Collider collider)
	{
		animator.SetBool ("Open", true);
	}
}
