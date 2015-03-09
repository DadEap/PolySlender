using UnityEngine;
using System.Collections;

public class vieJoueur : MonoBehaviour {

	int vie;

	// Use this for initialization
	void Start () {
		vie = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (vie <= 0) {
						Time.timeScale = 0f; // Le temps s'arrete
						GUI.TextArea (new Rect (Screen.width / 2 - 40, Screen.height / 2 - 20, 100, 40), "Game Over");
						if (GUI.Button (new Rect (Screen.width / 2 - 40, Screen.height / 2 - 30, 100, 40), "Menu Principal"))
								Application.LoadLevel ("menu"); 
				} else {
						float distance = GameObject.Find ("slender").GetComponent<SlenderDeplacement> ().distance;
						if (distance == 0)
								vie = vie - 100;
						else if (distance < 10)
								vie = vie - 30;
						else if (distance < 20)
								vie = vie - 20;
						else if (vie < 30)
								vie = vie - 10;
						else 
								vie += 10;
				}
	}
}
