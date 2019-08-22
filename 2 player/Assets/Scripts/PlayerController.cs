using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public float slideForce;

	public string hor;
	public string vert;
	public KeyCode jump;
	public KeyCode shoot;
	public KeyCode stand;
	public KeyCode slide;
	//public string shootRight;
	// public string shootLeft;

	public GameObject fireBall;
	public Transform handPoint;
	public int direction;


	private Rigidbody2D theRB;
	private bool clickedJump;
	private int jumpNumber;
	public int maxJumpNumber;

	public Transform groundCheckPointA;
	public Transform groundCheckPointB;
	public LayerMask whatIsGround;

	public bool isGrounded;
	public bool isStanding;
	public bool isSliding;

	public string state;

	[SerializeField]
	private bool isGroundedOld;
	[SerializeField]
	private bool landing;
	[SerializeField]
	private float slideEnd;

	private Animator anim;
	public float xComponent;
	public float yComponent;

	public float gravityUp;
	public float gravityDown;
	public float slideLength;
	public float airSpeed;
	public float phaseHeight;

	public AudioSource laserGun;

	private Canvas healthCanvas;

	public ProjectileObject assignedProjectile;
	public Gun assignedGun;

	private GameObject gunInstance;
	private Gun gunScript;

	float timeToFire = 0;

	// Use this for initialization
	void Start () {
		theRB = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();

		healthCanvas = transform.GetComponentInChildren<Canvas> ();
		if (this.gameObject.tag == "Player 1") {
			assignedProjectile = GameManager.p1Projectile;
			Debug.Log (GameManager.p1Projectile + "Start");
			Debug.Log (this.gameObject.tag);
		}
		else if (this.gameObject.tag == "Player 2") {
			assignedProjectile = GameManager.p2Projectile;
			Debug.Log (GameManager.p2Projectile + "Start");
			Debug.Log (this.gameObject.tag);
		} else {
			Debug.Log ("not finding tag");
		}
		AssignGun (assignedGun);


	}

	// Update is called once per frame
	void Update() {

//		Debug.Log (GameManager.p1Projectile);
//		if (this.tag == "Player1") {
//			assignedProjectile = GameManager.p1Projectile;
//		}
//		if (this.tag == "Player2") {
//			assignedProjectile = GameManager.p2Projectile;
//		}
		isGrounded = Physics2D.OverlapArea(groundCheckPointA.position, groundCheckPointB.position, whatIsGround);
		landing = isGrounded && !isGroundedOld;

		xComponent = Input.GetAxis(hor);
		yComponent = Input.GetAxis(vert);

		if (landing) {
			//Debug.Log ("landed");
		}
		//if moving left look left
		if(theRB.velocity.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
			healthCanvas.transform.localScale = new Vector3 (-0.022f, 0.022f, 1f);
		}
		//if moving right move right
		else if(theRB.velocity.x > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
			healthCanvas.transform.localScale = new Vector3 (0.022f, 0.022f, 1);
		}
		//Ground state
		if (isGrounded) 
		{
			//resetting gravity to normal when the player is on the ground
			theRB.gravityScale = gravityUp;
			//if player presses jump while on ground, jump
			if (Input.GetKeyDown (jump)) {
				if (yComponent != 1) {
					Jump ();
				} else {
					theRB.GetComponent<BoxCollider2D> ().enabled = false;
					theRB.velocity = new Vector2(theRB.velocity.x, -moveSpeed);
					theRB.position = new Vector2(theRB.position.x, theRB.position.y -phaseHeight);
					theRB.GetComponent<BoxCollider2D> ().enabled = true;
					Debug.Log ("go down");
				}
			}
			//checking if sliding
			else if (isSliding) {
				if (Time.time < slideEnd && Input.GetKey (slide)) {
					Slide ();
				} else {
					isSliding = false;
					//Debug.Log("done sliding");
				}
			}
			//if player presses stand while on ground, dont move
			else if (Input.GetKey (stand)) {
				//holding palyer still when on ground and standing, y component should matter whether 0 or velocity.y
				theRB.velocity = new Vector2 (0, theRB.velocity.y);
				//look direction when standing
				if (xComponent > 0) {
					transform.localScale = new Vector3 (1, 1, 1);
				} else if (xComponent < 0) {
					transform.localScale = new Vector3 (-1, 1, 1);
					healthCanvas.transform.localScale = new Vector3 (-0.022f, 0.022f, 1f);
				}
				state = "standing";
				//Debug.Log ("Stand");
			
			//starting the slide
			} else if (landing && Input.GetKey(slide) && theRB.velocity.y <= 0) {
				isSliding = true;
				slideEnd = Time.time + slideLength;

				}
			//if the player is moving the analog stick, move according to the analog stick
			else if (Mathf.Abs(xComponent) > 0) {
				theRB.velocity = new Vector2(moveSpeed * xComponent, theRB.velocity.y);
				state = "walking";
			} else {
				//if the player isnt moving the analog stick, dont move
				theRB.velocity = new Vector2(0, theRB.velocity.y);
				state = "idling";
			}
		}

		//In Air State
		if (!isGrounded) 
		{
			state = "in air";
			isSliding = false;
			//adjusting gravity
			if(theRB.velocity.y > 0)
			{
				theRB.gravityScale = gravityUp;
			}
			else
			{
				theRB.gravityScale = gravityDown;
			}

			//letting go of jump stops players jump
			if (Input.GetKeyUp(jump) && theRB.velocity.y>0) 
			{
				theRB.velocity = new Vector2 (theRB.velocity.x, theRB.velocity.y/4);
				//Debug.Log ("let go of jump");

			}
			//look direction when in air
			if (xComponent > 0)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
			else if(xComponent < 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);
				healthCanvas.transform.localScale = new Vector3 (-0.022f, 0.022f, 1f);
			}
			//air move
			if (Mathf.Abs (xComponent) > 0) {

				//if player is moving slower than movespeed
				if (Mathf.Abs (theRB.velocity.x) < moveSpeed) {
					theRB.velocity = new Vector2 (theRB.velocity.x + (moveSpeed * xComponent * airSpeed), theRB.velocity.y);
					state = "moving in air";
				} else {
					//if plaer is trying to move opposite of current direction
					if (Mathf.Sign (xComponent) != Mathf.Sign (theRB.velocity.x)) {
						theRB.velocity = new Vector2 (theRB.velocity.x + (moveSpeed * xComponent * airSpeed), theRB.velocity.y);
						state = "moving in air";
					}
				
				}
			} 
			else
			{
				if (Mathf.Abs(theRB.velocity.x) > moveSpeed / 10) 
				{

					//if player moving left and not touching stick
					if (Mathf.Sign (theRB.velocity.x) < 0) 
					{
						theRB.velocity = new Vector2 (theRB.velocity.x + (airSpeed * moveSpeed), theRB.velocity.y);	
					}
					// if player moving right and not touching stick
					else if (Mathf.Sign (theRB.velocity.x) > 0) 
					{
						theRB.velocity = new Vector2 (theRB.velocity.x - (airSpeed * moveSpeed), theRB.velocity.y);	
					}
				}

			}

			if (Input.GetKeyDown(jump) && (jumpNumber < maxJumpNumber - 1)) 
			{
				Jump ();
			}

		}


		anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
		anim.SetBool("Grounded", isGrounded);

		//if player clicks shoot
		if (Input.GetKeyDown (shoot)) {
			Shoot ();
		}
			
			
		isGroundedOld = isGrounded;



	}
	void Shoot()
	{	

		Debug.Log ("suh");
		gunScript.Shoot();
		/*
		 laserGun.Play();
		//making an instance of fireball
		GameObject fireBallClone = (GameObject)Instantiate(fireBall, shootPoint.position, shootPoint.rotation);
		//facing projectile same direction as player
		fireBallClone.transform.localScale = transform.localScale;

		//Creating a projectile from the fireballs projectile class to call the initialize function
		Projectile fireball = fireBallClone.GetComponent<Projectile>();
		//calling initialize function so the fireball knows who shot it and what type of projectile it is
		fireball.Initialize(this, this.gameObject.tag, assignedProjectile);
		*/
		anim.SetTrigger("Shoot");
	}
	void Jump()
	{
		if (isGrounded) {
			theRB.velocity = new Vector2 (theRB.velocity.x, jumpForce);
			jumpNumber = 0;
			//Debug.Log ("Jump");
		}
		else
		{
			theRB.velocity = new Vector2 (xComponent*moveSpeed, jumpForce);
			jumpNumber++;
			//Debug.Log ("double jump");
		}


	}
	void Slide()
	{
		//Debug.Log ("Slide");
		//theRB.velocity = new Vector2 (theRB.velocity.y *slideForce* Mathf.Sign(theRB.velocity.x), 0);
		theRB.AddForce(new Vector2(-(theRB.velocity.y)*slideForce*Mathf.Sign(theRB.velocity.x),0), ForceMode2D.Impulse);


	}
	public void AssignGun(Gun newGun)
	{
		if (transform.GetComponentInChildren<Gun> () != null) 
		{
			Gun activeGun = transform.GetComponentInChildren<Gun> ();
			Destroy (activeGun.transform.gameObject);
		}

		this.assignedGun = newGun;
		gunInstance = Instantiate (assignedGun.gameObject, handPoint.position, handPoint.rotation, transform);

		gunScript = gunInstance.GetComponent<Gun> ();

		//gunInstance.transform.parent = this.transform;

	}
	/*void changeProjectile(ProjectileObject newProjectile, string playerTag)
	{

		assignedProjectile = newProjectile;
	
	}*/


}
