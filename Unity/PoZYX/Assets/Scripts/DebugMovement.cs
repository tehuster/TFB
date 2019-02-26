using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovement : MonoBehaviour
{
	public float inputDeadzone = 0.1f;
	public float walkSpeed = 50;
	public float runSpeed = 100;
	public float turnSpeed = 10;
	public float strafeSpeed = 5;
	public bool running = false;

	public LayerMask groundLayerMask;

	private Vector3 input;
	private Vector3 movement;
	private Quaternion targetRotation;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		targetRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		GetInput();
		Turn();
	}

	void GetInput(){
		// get input
		input = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		);

		// toggle running

		if(Input.GetButton("Fire3")){
			running = !running;
			Debug.Log("Running Toggled");
		}

	}

	void FixedUpdate(){
		Move();
	}

	// use L shift to toggle runnning
	// always strafe
	void Move(){
		float movementSpeed;
		if(running){
			movementSpeed = runSpeed;
		}else{
			movementSpeed = walkSpeed;
		}

		if(Mathf.Abs(input.z) > inputDeadzone)
		{
			// move
			rb.velocity = transform.forward * input.z * movementSpeed;
		}
		else if(Mathf.Abs(input.x) > inputDeadzone)
		{
			// move
			rb.velocity = transform.right * input.x * strafeSpeed;
		}
		else
		{
			// zero velocity
			rb.velocity = Vector3.zero;
		}
	}

	void Turn(){
		/*
		if(Mathf.Abs(input.x) > inputDeadzone)
		{
			targetRotation *= Quaternion.AngleAxis(turnSpeed * input.x * Time.deltaTime, Vector3.up);
		}
		transform.rotation = targetRotation;
		*/

		// get mouse point
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		// if mouse if far enough away
			RaycastHit hit;

		// if (Physics.Raycast(cameraRay.direction, Vector3.down, out hit, Mathf.Infinity, groundLayerMask)){
		if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, groundLayerMask)){
			// get point to look at
			Vector3 pointToLookAt = hit.point;
			// modify y so we don't look at the ground
			pointToLookAt.y = transform.position.y;
			// TODO use turn speed to lerp toward pointToLookAt
			transform.LookAt(pointToLookAt);
			// transform.LookAt(transform.position + pointToLookAt, Vector3.up);
		}


	}           
}
