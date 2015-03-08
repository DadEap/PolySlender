using UnityEngine;
using System.Collections;

public class SlenderSight : MonoBehaviour {
	
	public float fieldOfViewAngle = 175f;
	public bool playerInSight;
	public bool playerInRange;
	public bool seenByPlayer;
	public Vector3 lastViewToGo;
	//private SphereCollider sphereCol;
	private GameObject player;
	void Start () {
	//	sphereCol = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		
	}

	void Update () {
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction,transform.forward);
		playerInSight = false;
		seenByPlayer = false;
		
		if(angle < fieldOfViewAngle * 0.5f)
		{
			RaycastHit hit;
			if(Physics.Raycast(transform.position,direction.normalized,out hit))
			{
				if(hit.collider.gameObject == player)
				{
					playerInSight = true;
					lastViewToGo = player.transform.position;
				}
			}
		}

		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		direction = transform.position - cam.transform.position;
		angle = Vector3.Angle(direction,cam.transform.forward);
		
		if(angle < fieldOfViewAngle * 0.5f)
		{
			RaycastHit hit;
			if(Physics.Raycast(cam.transform.position,direction.normalized,out hit))
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
}
