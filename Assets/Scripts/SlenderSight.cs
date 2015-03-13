using UnityEngine;
using System.Collections;

public class SlenderSight : MonoBehaviour {
	
	public float fieldOfViewAngle;
	public bool playerInSight;
	public bool playerInRange;
	public bool seenByPlayer;
	private GameObject player;
	void Start () {
	//	sphereCol = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		fieldOfViewAngle = 120f;
		
	}

	void Update () {
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction,transform.forward);
		playerInSight = false;
		seenByPlayer = false;
		
		if(angle < fieldOfViewAngle * 0.5f && Vector3.Distance(player.transform.position,transform.position) < 20)
		{
			playerInSight = true;
		}

		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		direction = transform.position - cam.transform.position;
		angle = Vector3.Angle(direction,cam.transform.forward);
		
		if(angle < cam.camera.fieldOfView * 0.5f)
		{
			RaycastHit hit;
			if(Physics.Raycast(cam.transform.position,direction.normalized,out hit,20))
			{
				if(hit.collider.gameObject == this.gameObject)
				{
					seenByPlayer = true;
				}
			}
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) 
		{
			playerInRange = true;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == player)
		{
			playerInRange = false;
			
		}
	}

	bool isVisible(GameObject origin,GameObject target)
	{
		Vector3 eyesOrigin = origin.transform.position;
		return true;
	}
}
