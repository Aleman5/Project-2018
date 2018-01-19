using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {


	public BoxCollider2D boxCollider2D;
	public Rigidbody2D rigidBody2D;
	public int hp;
	public float speed;
	public float weight;
	public float shield;
	//public float gravity; // Don't know how to implement it.

	protected int floorMask;
	protected Vector3 movement;
	protected Animator animator;

	protected virtual void Start () 
	{
		boxCollider2D = GetComponent <BoxCollider2D> ();
		rigidBody2D = GetComponent <Rigidbody2D> ();
		hp = 100;
		speed = 4f;
		weight = 0f;
		shield = 0f;
		//gravity = 9.8f;

		floorMask = LayerMask.GetMask ("Floor");
		movement.Set (0, 0, 0);
		animator = GetComponent<Animator> ();
	}
	/*
	protected virtual void OnCollide <T> ()
	{
		
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
	*/
}
