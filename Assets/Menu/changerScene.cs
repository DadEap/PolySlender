using UnityEngine;
using System.Collections;

public class changerScene : MonoBehaviour {

	//AudioClip son;
	public string levelSuivant = "projet";
	
	public void OnMouseUp() { 
			//audio.PlayOneShot (son);
			if (levelSuivant == "Quitter")
					Application.Quit ();
			else
					Application.LoadLevel (levelSuivant);
	}
}
