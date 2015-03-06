using UnityEngine;
using System.Collections;

public class SlenderDeplacement : MonoBehaviour {

	public Transform player;
	public bool isSeen;
	public float radius;
	public int timeWarping;
	private NavMeshAgent agent;
	private int timeCompteur;
	private Vector3 target;
	private bool hasSeen;

	private SlenderSight slenderSight;
	// Use this for initialization
	void Start () {

		radius = 20;
		timeCompteur = 0;
		timeWarping = 100;
		isSeen = false;
		hasSeen = false;
		slenderSight = GetComponent<SlenderSight> ();
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {

		if (slenderSight.playerInRange)
		{
			LookForPosition();
		}

		if (isSeen && slenderSight.playerInSight) 
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
		agent.SetDestination (player.position);
		hasSeen = true;
	}

	void Warping()
	{

		if(timeCompteur == 0)
		{
			if(hasSeen)
			{
				target = slenderSight.lastViewToGo;
				hasSeen = false;
			}
			else
			{
				target = Random.insideUnitSphere*radius;
				target += player.position;
			}
			NavMeshHit hit;
			NavMesh.SamplePosition(target,out hit,radius,1);
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
	}

	void LookForPosition()
	{
		agent.transform.LookAt (player.transform.position);
	}
}
