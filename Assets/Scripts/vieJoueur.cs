using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vieJoueur : MonoBehaviour {

	#region Attributs

	public float vie; 
	public double dist;
	private bool isOver;
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
		isOver = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (vie <= 0) 
		{
			GameObject over = GameObject.Find("GameOver");
			over.SetActive(true);
			Time.timeScale = 0f; // Le temps s'arrete
			isOver = true;
		}
		else
		{
			dist = ((SlenderDeplacement)slender.GetComponent ("SlenderDeplacement")).getDistance ();
			if (dist < 0f) 
			{
				vie = vie - 100;

			} 
			else if (dist < 5f) 
			{
				vie = vie - 0.1f;
			} 
			else  if (dist < 10f) 
			{
				vie = vie - 0.05f;
			} 
			else if (vie < 15f) 
			{
				vie = vie - 0.01f;
			} 
			else 
			{
				if (vie < 100)
					vie += 0.5f;
				if (vie >= 100)
					vie = 100;
			}
		}
	}
	/*
	void OnGui ()
	{
		if (isOver) 
		{
			//GUI.TextArea (new Rect (Screen.width / 2 - 40, Screen.height / 2 - 20, 100, 40), "Game Over");
			if (GUI.Button (new Rect (Screen.width / 2 - 40, Screen.height / 2 - 30, 100, 40), "Menu Principal"))
			{
				Application.LoadLevel ("menu");
			}

		}
	}*/
	#endregion
}
