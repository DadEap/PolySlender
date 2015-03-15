using UnityEngine;
using System.Collections;

public class AfficherTexte : MonoBehaviour {

	private static string texte;
	private static float alpha;
	private static int time;

	void Start(){
		time = 0;
		alpha = 0;
		texte="";
	}

	public static void Afficher(string txt){
		texte = txt;
		alpha = 1;
		time = 300;
		//Debug.Log("Afficher");
	}

	void OnGUI ()
	{	
		//Debug.Log ("ongui");
		if (time > 0) {
			Color guiColor=GUI.color;
			// changer l'alpha juste pour ce label
			GUI.color = new Color(1,1,1,(float)time/300);
			GUI.Label (new Rect (Screen.width / 2 - 80, Screen.height / 2 - 40, 300, 100), texte);
			time--;
			GUI.color=guiColor; // on remet la couleur à sa valeur initiale car GUI.color est partagé pour tous les labels
		}
	}
}
