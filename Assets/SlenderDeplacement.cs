using UnityEngine;
using System.Collections;

public class SlenderDeplacement : MonoBehaviour {

	public Vector3 target;
	public float radius;
	public bool inRange;
	public bool inSight;
	bool justwarp;
	SphereCollider rangeCollider;
	Vector3 sightRange;
	NavMeshAgent agent;
	public int timeWarping;
	int compteur;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		inRange = false;
		inSight = false;
		radius = 20;
		justwarp = false;
		compteur = 0;
		timeWarping = 500;

	}
	
	// Update is called once per frame
	void Update () {


		if (inSight)
		{
			agent.SetDestination(target);
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
