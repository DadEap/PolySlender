using UnityEngine;
using System.Collections;

public class BadgeDoorAnimation : MonoBehaviour {
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag.Equals("Player") && GestionnaireObjets.possedeObjet(GestionnaireObjets.Ramassable.CarteEtudiant) )
			anim.Play ("Armature|Open");
		if (collider.tag.Equals("slender"))
			anim.Play ("Armature|Open");
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.tag.Equals("Player") || collider.tag.Equals("slender"))
			anim.Play ("Armature|Close");
	}

	// Update is called once per frame
	void Update () { }
}
