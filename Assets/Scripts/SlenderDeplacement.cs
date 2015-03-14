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
	private Transform player;

	private SlenderSight slenderSight;
	// Use this for initialization
	void Start () {

		lifeWarping = 500;
		lifeCompteur = 0;
		radius = 70;
		timeCompteur = 0;
		timeWarping = 100;
		slenderSight = GetComponent<SlenderSight> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		distance = Mathf.Infinity;

	}
	
	// Update is called once per frame
	void Update () {

		if(lifeCompteur > lifeWarping)
		{
			Killing();
		}
		else if ( slenderSight.seenByPlayer) 
		{
			Waiting ();
		}
		else if (slenderSight.playerInSight || slenderSight.playerInRange) {
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
		Debug.Log (path.status);
		if(path.status == NavMeshPathStatus.PathComplete)
		{
			agent.SetDestination(hit.position);
			distance = agent.remainingDistance;
		}
		else
			Warping();

		if (!slenderSight.playerInSight)
						distance = Mathf.Infinity;

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
			Vector3 direction = (hit.position - cam.transform.position).normalized;
			float dot = Vector3.Dot(cam.transform.forward,direction);
			
			if(slenderSight.isVisible(cam.transform,hit.position))
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
		if(slenderSight.playerInSight)
			lifeCompteur++;
		distance = agent.remainingDistance;
	}

	void LookForPosition()
	{
		transform.LookAt(player.transform.position);
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
