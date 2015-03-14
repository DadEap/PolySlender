using UnityEngine;
using System.Collections;

public class Clignotement : MonoBehaviour {

	public int frequence;
	private float timer;

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			timer = frequence * Random.value*Random.value;
			yield return new WaitForSeconds(timer);
			light.enabled = !light.enabled;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
