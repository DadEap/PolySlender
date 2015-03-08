#if UNITY_EDITOR
using UnityEngine;
#endif

using System.Collections;

public class Acceleration : MonoBehaviour {

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
	void Start () {
		motor = GameObject.Find("First Person Controller").GetComponent<CharacterMotor>();
		controller = GetComponent<CharacterController>();
		delay = delayBetweenStep;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			motor.movement.maxForwardSpeed = 6;
			motor.movement.maxSidewaysSpeed = 6;
			motor.movement.maxBackwardsSpeed = 6;
			delayBetweenStep = delay / 2;
		} else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			motor.movement.maxForwardSpeed = 3;
			motor.movement.maxSidewaysSpeed = 3;
			motor.movement.maxBackwardsSpeed = 3;
			delayBetweenStep = delay;
		}
	}
}
