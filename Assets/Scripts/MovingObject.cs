using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public int hp;
	public float weight;
	public float shield;
	public float minGroundNormalY = .65f;
	public float gravityModifier = 3f;

	protected BoxCollider2D boxCollider2D;
	protected Rigidbody2D rigidBody2D;
	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	protected Vector2 velocity;
	protected Vector2 targetVelocity;
	protected Vector2 groundNormal;
	protected ContactFilter2D contactFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
	protected bool grounded;

	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	void OnEnable ()
	{
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void Start () 
	{
		boxCollider2D = GetComponent <BoxCollider2D> ();
		rigidBody2D = GetComponent <Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		hp = 100;
		weight = 0f;
		shield = 0f;
		//Sets to filter contact results based on trigger collider involvement.
		contactFilter.useTriggers = false;
		//Sets the layerMask filter property using the layerMask parameter provided and also 
		// enables layer mask filtering by setting useLayerMask to true.
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		//Sets the contact filter to filter results by layer mask.
		contactFilter.useLayerMask = true;
	}
	// Update is called once per frame
	void Update () 
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity ();
	}
	protected virtual void ComputeVelocity()
	{
		
	}
	void FixedUpdate()
	{
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;

		Movement (move, false);

		move = Vector2.up * deltaPosition.y;

		Movement (move, true);
	}
	void Movement (Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;
		if (distance > minMoveDistance) 
		{
			int count = rigidBody2D.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear ();
			for (int i = 0; i < count; i++)
				hitBufferList.Add (hitBuffer [i]);
			
			for (int i = 0; i < hitBufferList.Count; i++) 
			{
				Vector2 currentNormal = hitBufferList [i].normal;
				if (currentNormal.y > minGroundNormalY) 
				{
					grounded = true;
					if (yMovement) 
					{
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}
				//Actually, 'currentNormal' has a value on the Y axis.
				float projection = Vector2.Dot (velocity, currentNormal);
				if (projection > 0) 
				{
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList [i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}
		rigidBody2D.position = rigidBody2D.position + move.normalized * distance;
	}
}
