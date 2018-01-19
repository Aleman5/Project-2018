using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

	private enum States
	{	
		Idle,
		Moving,
		Jumping,
		Died
	};

	private States currentState;

	public Vector2 jumpingForce;

	protected override void Start () 
	{
		currentState = States.Idle;
		jumpingForce.Set (0, 20f);
		//rigidBody2D.gravityScale = gravity;

		base.Start ();
	}
	private void FixedUpdate () // It's using "FixedUpdate" because a Rigidbody is being used.
	{
		StateMachine ();
	}
	private void StateMachine()
	{
		switch (currentState) 
		{
		case States.Idle:
			Movement ();
			Jump ();
			break;
		case States.Moving:
			Movement ();
			Jump ();
			break;
		case States.Jumping:
			Movement ();
			break;
		case States.Died:
			break;
		}
	}
	private void Movement()
	{
		movement.Set (0, 0, 0);
		if (Input.GetButton ("Right"))
			movement.Set (Input.GetAxisRaw("Right"), 0, 0);
		else if(Input.GetButton("Left"))
			movement.Set (-Input.GetAxisRaw("Left"), 0, 0);
		movement = movement.normalized * speed * Time.deltaTime;
		rigidBody2D.MovePosition (transform.position + movement);
	}
	private void Jump()
	{
		if(Input.GetButtonDown("Jump"))
		{
			rigidBody2D.AddForce (jumpingForce, ForceMode2D.Impulse);
			//currentState = States.Jumping;
		}
	}
}
