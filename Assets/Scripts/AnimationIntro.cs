using UnityEngine;
using System.Collections;

public class AnimationIntro : MonoBehaviour {

	private int plan;
	private GameObject camera1;
	private GameObject camera2;
	private GameObject fading;
	private GameObject jimmy;
	private Quaternion angle1;
	private GameObject slender;
	private GameObject fakeSlender;
	private float alpha;
	private int counter1;
	private GameObject FPController;
	private Vector3 jimmyHead;

	// Use this for initialization
	void Start () {
		FPController = GameObject.FindGameObjectWithTag ("Player");
		FPController.SetActive (false);
		counter1 = 0;
		camera1 = GameObject.Find ("AnimationIntro/Camera1");
		fading = GameObject.Find ("AnimationIntro/Camera1/Plane");
		camera2 = GameObject.Find ("AnimationIntro/Camera2");
		jimmy = GameObject.Find ("AnimationIntro/Jimmy");
		fakeSlender = GameObject.Find ("AnimationIntro/slender");
		slender = GameObject.Find ("slender");
		slender.SetActive (false);
		fakeSlender.SetActive (false);
		alpha = 1f;
		fading.renderer.material.color = new Color (0, 0, 0, 1f);
		jimmyHead = new Vector3 (-40.8f,1.4f, -21.9f);
		angle1 = Quaternion.LookRotation ((jimmyHead-camera1.transform.position).normalized);
		camera2.SetActive (false);
		plan = 1;
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyIdle");
		jimmy.animation.PlayQueued("JimmyOuvrePorte");
	}
	
	// Update is called once per frame
	void Update () {

		//---------------- plan 1 ------------------
		// jimmy ouvre la porte

		if (plan==1){
			// diminuer l'opacité du fade in
			if (alpha>0.01f) {
				alpha -= 0.01f;
				fading.renderer.material.color = new Color (0, 0, 0, alpha);
			}
			// tourner la camera vers Jimmy
			camera1.transform.rotation = Quaternion.Slerp(camera1.transform.rotation,angle1,0.01f);
			// zoomer légèrement
			((Camera)camera1.GetComponent("Camera")).fieldOfView-=0.01f;
			// on passe au plan 2 si la camera a fini de tourner et que l'animation "entrer" est finie
			if (
				Quaternion.Angle(camera1.transform.rotation,angle1)<5f
				&&
				!jimmy.animation.isPlaying
			) {
				plan=2;
				// zoomer
				((Camera)camera1.GetComponent("Camera")).fieldOfView=5;
				// déclencher l'animation suivante
				jimmy.animation.PlayQueued("JimmyRegarde");
				camera1.transform.rotation=angle1;
			}
		}

		// ----------------- plan 2 -----------------
		// jimmy regatde à l'intérieur

		else if (plan==2){
			// camera fixe
			// condition pour passer au plan suivant :
			if (!jimmy.animation.isPlaying) {
				plan=3;
				((Camera)camera1.GetComponent("Camera")).fieldOfView=50;
				jimmy.animation.PlayQueued("JimmyEntre");
			}
		}

		//----------------- plan 3 -----------------
		// jimmy entre

		else if (plan==3){
			// camera fixe
			// condition pour passer au plan suivant :
			if (!jimmy.animation.isPlaying) {
				plan=4;
				// changer de camera
				camera1.SetActive(false);
				camera2.SetActive(true);
				// apparition du slender
				fakeSlender.SetActive(true);
			}
		}


		//----------------- plan 4 -----------------
		// on voit le slender

		else if (plan==4) {
			// zoomer légèrement
			((Camera)camera2.GetComponent("Camera")).fieldOfView-=0.1f;
			// condition pour passer au plan suivant : camera assez zoomée
			if (((Camera)camera2.GetComponent("Camera")).fieldOfView<50f){
				plan=5;
				// changer de camera
				camera2.SetActive(false);
				camera1.SetActive(true);
				// animation suivante
				((Camera)camera1.GetComponent("Camera")).fieldOfView=20f;
				jimmy.animation.PlayQueued("JimmySurprise");
			}
		}

		//----------------- plan 5 -----------------
		// jimmy tente de s'échapper

		else if (plan==5){
			// diparition mystérieuse
			fakeSlender.SetActive(false);
			// condition pour passer au plan suivant : fin de l'animation
			if (!jimmy.animation.isPlaying)
			{
				plan=6;
				// changer de camera
				camera1.SetActive(false);
				camera2.SetActive(true);
			}
		}

		//----------------- plan 5 -----------------
		// slender n'est plus là

		else if (plan==6){
			((Camera)camera2.GetComponent("Camera")).fieldOfView+=0.05f;
			if (((Camera)camera2.GetComponent("Camera")).fieldOfView>=60f){
				plan=7;
			}
		}
		else if (plan>6){
			FPController.SetActive (true);
			slender.SetActive(true);
			Destroy(this.gameObject);
		}
	}
}
