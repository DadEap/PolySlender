using UnityEngine;
using System.Collections;

public class Clignotement : MonoBehaviour {

	private vieJoueur scriptVieJoueur;
	private AudioSource son;

	// Use this for initialization
	void Start () {
		scriptVieJoueur = (vieJoueur) GameObject.FindGameObjectWithTag("Player").GetComponent("vieJoueur");
		son = (AudioSource) GetComponent ("AudioSource");
	}

	void FixedUpdate () {
		int offsetTexture = Random.Range(1,20);
		float frequenceTexture = (100-scriptVieJoueur.vie)/100;
		int couleurTexture = Random.Range (1, 5);
		// borné entre 0 et 1 pour etre sur que ça pète pas les oreilles
		float volumeSon = Mathf.Min(1, Mathf.Max(0,(50 - scriptVieJoueur.vie + Random.Range(0,25))/200));
		renderer.material.SetTextureOffset ("_MainTex", new Vector2 (offsetTexture, offsetTexture));
		renderer.material.color = new Color(couleurTexture,couleurTexture,couleurTexture,Random.Range(0f,frequenceTexture));
		son.volume = volumeSon;
	}
}
