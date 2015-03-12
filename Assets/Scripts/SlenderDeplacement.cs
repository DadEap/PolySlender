using UnityEngine;
using System.Collections;

public class SlenderDeplacement : MonoBehaviour {
	
	public float radius;
	public double distance;
	private int timeWarping;
	private int lifeWarping;
	private NavMeshAgent agent;
	private int timeCompteur;
	private int lifeCompteur;
	private Vector3 target;
	private bool hasSeen;
	private Transform player;
	private int showCompteur = 50;

	private SlenderSight slenderSight;
	// Use this for initialization
	void Start () {

		lifeWarping = 500;
		lifeCompteur = 0;
		radius = 20;
		timeCompteur = 0;
		timeWarping = 100;
		hasSeen = false;
		slenderSight = GetComponent<SlenderSight> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		distance = Mathf.Infinity;

	}
	
	// Update is called once per frame
	void Update () {

		if (slenderSight.playerInRange)
		{
			LookForPosition();
		}

		if(lifeCompteur > lifeWarping)
		{
			Killing();
		}
		else if (slenderSight.seenByPlayer && slenderSight.playerInSight) 
		{
			Waiting ();
		}
		else if (slenderSight.playerInSight) {
			Chasing ();
		}  
		else 
		{
			Warping();
		}
	}

	void Chasing()
	{
		if (showCompteur == 0) 
		{
			showCompteur = 50;
		}
		agent.SetDestination (player.position);
		hasSeen = true;
		showCompteur--;
		distance = agent.remainingDistance;
	}

	void Warping()
	{
		distance = Mathf.Infinity;
		bool canWarp = true;
		lifeCompteur = 0;
		if(timeCompteur == 0)
		{
			if(hasSeen)
			{
				//target = slenderSight.lastViewToGo;
				hasSeen = false;
			}
			else
			{
				target = Random.insideUnitSphere;
				if(target.x*radius < 8 && target.y*radius < 8 && target.z*radius < 8)
					return;

				target *= radius;
				target += player.position;
			}
			NavMeshHit hit;
			NavMesh.SamplePosition(target,out hit,radius,1);

			GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
			Vector3 direction = hit.position - cam.transform.position;
			float angle = Vector3.Angle(direction,cam.transform.forward);
			
			if(angle < 175f * 0.5f)
			{
				RaycastHit rayHit;
				if(Physics.Raycast(cam.transform.position,direction.normalized,out rayHit))
				{
					if(rayHit.collider.gameObject == this.gameObject)
					{
						canWarp = false;
					}
				}
			}
			if(canWarp)
				agent.Warp(hit.position);			
		}
		timeCompteur++;
		if (timeCompteur > timeWarping)
		{
			timeCompteur = 0;
		}
	}

	void Waiting ()
	{
		agent.Stop();
		lifeCompteur++;
		distance = agent.remainingDistance;
	}

	void LookForPosition()
	{
		agent.transform.LookAt (player.transform.position);
		//distance = agent.remainingDistance;
	}

	void Killing()
	{
		distance = -1.0;
		Debug.Log (distance);
		target = player.forward *( (float)(2));
		target += player.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(target,out hit,radius,1);
		agent.Warp(hit.position);
	}

	public double getDistance()
	{
		return distance;
	}
}
