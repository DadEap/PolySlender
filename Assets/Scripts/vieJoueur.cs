using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vieJoueur : MonoBehaviour {

	#region Attributs

	public float vie; 
	public double dist;
	public GameObject gameOver; // l'objet (prefab) à instancier lors du Game Over
	private GameObject slender;

	#endregion
	
	#region Proprietes
	#endregion
	
	#region Constructeur
	#endregion
	
	#region Methodes

	// Use this for initialization
	void Start () {
		vie = 100.0f;
		dist = Mathf.Infinity;
		slender = GameObject.Find("slender");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (vie);
		if (vie <= 0) 
		{
			// déclenchement du Game Over :
			Instantiate(gameOver, Vector3.zero, Quaternion.identity);
			// la condition d'arret du jeu est atteinte, on se débarrasse donc de ce script
			// cela évite de créer en boucle des GameOver
			Destroy(this);
		}
		else
		{
			dist = ((SlenderDeplacement)slender.GetComponent ("SlenderDeplacement")).getDistance ();
			if (dist < 0f) 
			{
				// comment on a une distance inférieure à 0 ?
				vie = vie - 100*Time.deltaTime;

			} 
			else if (dist < 5f) 
			{
				vie = vie - 20f*Time.deltaTime;
			} 
			else  if (dist < 10f) 
			{
				vie = vie - 5f*Time.deltaTime;
			} 
			else if (vie < 15f) 
			{
				vie = vie - 1f*Time.deltaTime;
			} 
			else 
			{
				if (vie < 100)
					vie += 5f*Time.deltaTime;
				if (vie >= 100)
					vie = 100;
			}
		}
	}

	#endregion
}
