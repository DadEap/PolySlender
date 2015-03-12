using UnityEngine;
using System.Collections;

public class MouvementRobot : MonoBehaviour {

	private float vitesse = 30;
	private float vitesseAngulaire = 10;
	private float vitesseRoues = 5;
	private bool tourner;
	private GameObject roues;

	void Start(){
		roues = transform.FindChild ("RouesRobot").gameObject;
		tourner = true;
		AudioSource bruit = (AudioSource) GetComponent ("AudioSource");
		bruit.time = bruit.clip.length * Random.Range (0.0f, 1.0f);
	}

	void FixedUpdate () {
		// animer les roues
		roues.transform.Rotate (new Vector3(0,-1*vitesseRoues,0));
		// si on est au sol, faire son déplacement
		if (rigidbody.velocity.y==0){
			if (tourner){
				// si on est en train de tourner,  tourner jusqu'à ce que la bonne direction soit atteinte
				transform.Rotate(new Vector3(0,0,vitesseAngulaire));
				if (Random.Range(1,10)<=1) tourner=false;
			}
			else{
				// sinon, avancer
				rigidbody.AddForce(transform.right*-1*vitesse);
				if (Random.Range(1,10)<=1) tourner=true;
			}

		}
	}
}
