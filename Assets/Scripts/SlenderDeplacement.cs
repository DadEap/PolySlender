using UnityEngine;
using System.Collections;

public class SlenderDeplacement : MonoBehaviour {

	public GameObject player;
	public float radius;
	public bool inRange;
	public bool inSight;
	bool justwarp;
	SphereCollider rangeCollider;
	Vector3 sightRange;
	NavMeshAgent agent;
	public int timeWarping;
	int compteur;
	Vector3 target;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		inRange = false;
		inSight = false;
		radius = 30;
		justwarp = false;
		compteur = 0;
		timeWarping = 100;

	}
	
	// Update is called once per frame
	void Update () {


		if (inSight)
		{
			agent.SetDestination (player.transform.position);
		} 
		else if (inRange)
		{

		}
		else
		{
			if(!justwarp)
			{
				target = Random.insideUnitSphere*radius;
				target += transform.position;
				NavMeshHit hit;
				NavMesh.SamplePosition(target,out hit,radius,1);
				agent.Warp(hit.position);
				justwarp = true;

			}
			else
			{
				compteur++;
			}
		}
		if (compteur > timeWarping)
		{
			justwarp = false;
			compteur = 0;
		}
	
	}
}
