using UnityEngine;
using System.Collections;

public class curseur : MonoBehaviour {
	public Texture2D curs;
	
	public void Start() {
		Screen.showCursor = false; 
	}
	
	public void OnGUI() {
		Vector3 positionSouris = Input.mousePosition;
		Rect positionCurseur = new Rect(positionSouris.x, Screen.height - positionSouris.y, curs.width, curs.height);
		GUI.Label(positionCurseur,curs);
	}
}
