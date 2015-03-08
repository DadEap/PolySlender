using UnityEngine;
using System.Collections;

public class FootStepsSound : MonoBehaviour {
	
	public AudioClip[] concreteSoundStep;
	public AudioClip[] woodSoundStep;
	public AudioClip[] metalSoundStep;
	public AudioClip[] terrainSoundStep;
	private CharacterController controller;
	private CharacterMotor motor;
	public float normalSpeed;
	public float sprintSpeed;
	public float delayBetweenStep;
	private float nextPlay;
	private float delay;
	private AudioClip step;
	
	
	// Use this for initialization
	void Start () 
	{
		motor = GetComponent<CharacterMotor>();
		controller = GetComponent<CharacterController>();
		delay = delayBetweenStep;
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		if (controller.isGrounded & controller.velocity.magnitude > 0.1 & Time.time > nextPlay)
		{
			nextPlay = Time.time + delayBetweenStep;
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
			{/*
				switch (hit.collider.tag)
				{
				case "concrete":*/
					step = concreteSoundStep[0];
					/*break;
				case "wood":
					step = woodSoundStep[Random.Range(0, 4)];
					break;
				case "metal":
					step = metalSoundStep[Random.Range(0, 4)];
					break;
				default:
					step = terrainSoundStep[Random.Range(0, 4)];
					break;
				}*/
				audio.clip = step;
				audio.Play();
			}
		}
		
		if (Input.GetKey(KeyCode.LeftShift))
		{
			motor.movement.maxForwardSpeed = sprintSpeed;
			motor.movement.maxSidewaysSpeed = sprintSpeed;
			motor.movement.maxBackwardsSpeed = sprintSpeed;
			delayBetweenStep = delay/2;
		}
		else
		{
			motor.movement.maxForwardSpeed = normalSpeed;
			motor.movement.maxSidewaysSpeed = normalSpeed;
			motor.movement.maxBackwardsSpeed = normalSpeed;
			delayBetweenStep = delay;
			return;
		}
	}
}
