using UnityEngine;
using System.Collections;

public class SlenderDeplacement : MonoBehaviour {
	
	public float radius;
	public int timeWarping;
	private float distance;
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
		radius = 70;
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
		else if ( slenderSight.seenByPlayer) 
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
		NavMeshHit hit;
		NavMeshPath path = new NavMeshPath();
		NavMesh.SamplePosition(player.position,out hit,radius,1);
		agent.CalculatePath (hit.position, path);
		if(path.status == NavMeshPathStatus.PathComplete)
		{
			Debug.Log("true");
			agent.SetDestination(hit.position);
			hasSeen = true;
			distance = agent.remainingDistance;
		}
		else
			Warping();

	}

	void Warping()
	{
		distance = Mathf.Infinity;
		bool canWarp = true;
		lifeCompteur = 0;
		if(timeCompteur == 0)
		{

			target = Random.insideUnitSphere;
			if(target.x*radius < 8 && target.y*radius < 8 && target.z*radius < 8)
				return;

			target *= radius;
			target += player.position;
			NavMeshHit hit;
			NavMesh.SamplePosition(target,out hit,radius,1);

			GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
			Vector3 direction = hit.position - cam.transform.position;
			float angle = Vector3.Angle(direction,cam.transform.forward);
			
			if((angle < cam.camera.fieldOfView * 0.5f) && Vector3.Distance(cam.transform.position,hit.position) < 20)
			{
				canWarp = false;
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
		if(slenderSight.seenByPlayer)
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
		distance = -1.0f;
		Debug.Log (distance);
		target = player.forward *( (float)(2));
		target += player.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(target,out hit,radius,1);
		agent.Warp(hit.position);
	}

	public float getDistance()
	{
		return distance;
	}
}
