using UnityEngine;
using System.Collections;

public class changerTxt : MonoBehaviour {
	public Color CouleurEntree = Color.yellow;
	public Color CouleurSortie = Color.black;
	public int TailleEntrer = 75;
	public	int TailleSortie = 50;

	void Awake() {
		guiText.material.color = CouleurSortie;
		guiText.fontSize = 5 * Screen.height / 100;
	}

	void OnMouseEnter() {
		guiText.material.color = CouleurEntree;
		guiText.fontSize = 10 * Screen.height / 100;
	}

	void OnMouseExit() {
		guiText.material.color = CouleurSortie;
		guiText.fontSize = 5 * Screen.height / 100;
	}
}
