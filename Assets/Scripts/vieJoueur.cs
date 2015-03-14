using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vieJoueur : MonoBehaviour {

	#region Attributs

	public float vie; 
	public double dist;
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
			// impossible d'utiliser ce code car lorsque GameOver est désactivé, Find() ne le trouve pas
			// GameObject.Find("GameOver").SetActive(true);
			// je vais modifier cette partie du code pour l'animation de Game Over
			Time.timeScale = 0f; // Le temps s'arrete
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
				vie = vie - 10f*Time.deltaTime;
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
