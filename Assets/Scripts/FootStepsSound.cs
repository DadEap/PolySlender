using UnityEngine;
using System.Collections;

public class FootStepsSound : MonoBehaviour {
	
	public AudioClip concreteSoundStep;
	//public AudioClip[] woodSoundStep;
	//public AudioClip[] metalSoundStep;
	//public AudioClip[] terrainSoundStep;
	//public AudioClip breathless;
	private CharacterController controller;
	private CharacterMotor motor;

	public float normalSpeed;
	public float sprintSpeed;
	public float delayBetweenStep;
	//public float delayBetweenBreathless;
	public float delayBetweenRun;

	public int maxEndurance;
	public int perteEndurance;
	public int gainEndurance;

	private float nextPlay;
	private float nextRun;

	//private float nextPlayBreathless;
	private float delay;
	private AudioClip step;
	private int endurance;

	
	
	// Use this for initialization
	void Start () 
	{
		motor = GetComponent<CharacterMotor>();
		controller = GetComponent<CharacterController>();
		delay = delayBetweenStep;
		endurance = maxEndurance;
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
					step = concreteSoundStep;
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
		/*
		if ( (endurance == 0) & (Time.time > nextPlayBreathless) ) {
			nextPlayBreathless = Time.time + delayBetweenBreathless;
			audio.PlayOneShot (breathless);
		}
		*/

		if (Input.GetKey(KeyCode.LeftShift) && endurance > 0 && Time.time > nextRun )
		{
			if (Input.GetKey (KeyCode.Z) || Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) {
				//nextPlayBreathless = Time.time + delayBetweenBreathless;
				motor.movement.maxForwardSpeed = sprintSpeed;
				motor.movement.maxSidewaysSpeed = sprintSpeed;
				motor.movement.maxBackwardsSpeed = sprintSpeed;
				delayBetweenStep = delay / 2;
				endurance = endurance - perteEndurance;
				if (endurance <= 0) {
					nextRun = Time.time + delayBetweenRun;
				}
				return;
			}
		}
		else 
		{
			motor.movement.maxForwardSpeed = normalSpeed;
			motor.movement.maxSidewaysSpeed = normalSpeed;
			motor.movement.maxBackwardsSpeed = normalSpeed;
			delayBetweenStep = delay;

			if (endurance < maxEndurance) {
				endurance = endurance + gainEndurance;
			}

			return;
		}


	}
}
