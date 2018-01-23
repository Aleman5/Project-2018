using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {
	/*
	enum States
	{	
		Idle,
		Moving,
		Jumping,
		Died
	};
	
	//States currentState;

	void Awake () 
	{
		currentState = States.Idle;
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
			break;
		case States.Moving:
			break;
		case States.Jumping:
			break;
		case States.Died:
			break;
		}
	}
	*/

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 12;

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		} else if (Input.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0)
				velocity.y = velocity.y * 0.5f;
		}

		if (move.x > 0.01f) {
			if (spriteRenderer.flipX)
				spriteRenderer.flipX = false;
		}
		else if (move.x < -0.01f) {
			if (!spriteRenderer.flipX)
				spriteRenderer.flipX = true;
		}
		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;
	}
}










/*
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
*/