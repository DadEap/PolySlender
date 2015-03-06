using UnityEngine;
using System.Collections;

public class SlenderSight : MonoBehaviour {

	public float fieldOfViewAngle = 175f;
	public bool playerInSight;
	public bool playerInRange;
	public Vector3 lastViewToGo;
	//private NavMeshAgent agent;
	private SphereCollider sphereCol;
	private GameObject player;
	// Use this for initialization
	void Start () {
	//	agent = GetComponent<NavMeshAgent> ();
		sphereCol = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - transform.position;
		float angle = Vector3.Angle(direction,transform.forward);
		playerInSight = false;

		if(angle < fieldOfViewAngle * 0.5f)
		{
			RaycastHit hit;
			if(Physics.Raycast(transform.position,direction.normalized,out hit,sphereCol.radius))
			{
				if(hit.collider.gameObject == player)
				{
					playerInSight = true;
					lastViewToGo = player.transform.position;
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
