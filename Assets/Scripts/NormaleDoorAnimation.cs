using UnityEngine;
using System.Collections;

public class NormaleDoorAnimation : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider collider)
	{
		anim.Play ("Armature|Open");
	}

	void OnTriggerExit(Collider collider)
	{
		anim.Play ("Armature|Close");
	}

	// Update is called once per frame
	void Update () { }
}
