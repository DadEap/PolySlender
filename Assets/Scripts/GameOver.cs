using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public int temps=100; // nombre de frames que durera l'animation
	private GameObject camera; // la camera, pour manipuler ses mouvements
	private Quaternion angleRegard; // la rotation qu'il faut effectuer pour que la camera regarde le Slender

	void Start(){
		Time.timeScale = 0f; 
		// pour une raison que j'ignore, dans le menu pause la camera ne marche plus
		// mais dans le gameover, elle marche encore
		// donc je la désactive
		camera = GameObject.Find("First Person Controller/Main Camera");
		((MouseLook)camera.GetComponent ("MouseLook")).enabled = false;
		((MouseLook)GameObject.Find("First Person Controller").GetComponent ("MouseLook")).enabled = false;
		// idem pour le mouvement
		((CharacterMotor)GameObject.Find("First Person Controller").GetComponent ("CharacterMotor")).enabled = false;
		// slender
		GameObject slender = GameObject.Find ("slender");
		float hauteurSlender = 1.6f; // hauteur en mètres du Slender, pour regarder sa tete et non ses pieds
		// on calcule l'angle qu'on doit faire effectuer à la camera :
		// il s'agit de l'angle entre la camera et la tete du Slender
		angleRegard = Quaternion.LookRotation((slender.transform.position+hauteurSlender*Vector3.up - camera.transform.position).normalized);
	}

	void Update(){
		// l'animation s'arretera après un certain nombre d'itérations
		if (temps > 0) {
			// utiliser le Slerp pour faire effectuer la rotation à la camera dans le temps
			// ici je n'utilise pas Time.deltaTime car cette animation doit s'effectuer alors que le temps est arreté
			camera.transform.rotation = Quaternion.Slerp (camera.transform.rotation, angleRegard, 0.05f);
			temps--;
		} else {
			// à la fin de l'animation on retourne au menu principal
			Application.LoadLevel("menu");
		}
	}

}
