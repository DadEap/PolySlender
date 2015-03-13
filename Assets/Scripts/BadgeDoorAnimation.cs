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
		if (collider.tag == "Player" && GestionnaireObjets.possedeObjet(GestionnaireObjets.Ramassable.CarteEtudiant))
			anim.Play ("Armature|Open");
	}

	void OnTriggerExit(Collider collider)
	{
		anim.Play ("Armature|Close");
	}

	// Update is called once per frame
	void Update () { }
}
