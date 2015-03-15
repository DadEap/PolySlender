using UnityEngine;
using System.Collections;

public class SlenderSight : MonoBehaviour {
	
	public float fieldOfViewAngle;
	public bool playerInSight;
	public bool playerInRange;
	public bool seenByPlayer;
	private GameObject player;
	private GameObject cam;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		fieldOfViewAngle = 0.75f;
		
	}

	void Update () {
		playerInSight = isVisible (transform, player.transform.position);
		seenByPlayer = isVisible (cam.transform, transform.position);
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

	public bool isVisible(Transform origin,Vector3 target)
	{
		
		Vector3 direction = (target - origin.position).normalized;
		float dot = Vector3.Dot (origin.forward, direction);
		RaycastHit hit;
		if(dot > fieldOfViewAngle && Vector3.Distance(origin.position,target) < GameObject.Find("LampeDePoche").light.range)
		{
			if(Physics.Linecast(target,origin.position,out hit))
			{
				if(hit.collider.gameObject.tag.Equals("StopView"))
					return false;
				else
					return true;
			}
			else
				return true;
		}
		return false;
	}
}
