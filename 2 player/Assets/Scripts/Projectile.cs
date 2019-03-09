using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour {

   // public float ballSpeed;

    private Rigidbody2D theRB;

    public GameObject sparkEffect;
	public float xShotComponent;
	public float yShotComponent;
	public string damageDealerTag;
	public string damageTakerTag;
	//public int projectileDamage;
	public ProjectileObject selectedProjectile;

	public float theta;
	public float shotAngle;
	public float shotX;
	public float shotY;
	public float shotAngleD;




	public float magnitude;

    // Use this for initialization
	public void Initialize (PlayerController playercontroller,string playerTag, ProjectileObject assignedProjectile) {

		damageDealerTag = playerTag;
		//setting up the rigid body within script
        theRB = GetComponent<Rigidbody2D>();
		//Getting the left stick controller position from parent gameobject(the player)
		//PlayerController playercontroller = GetComponentInParent<PlayerController> ();
		xShotComponent = playercontroller.xComponent;
		yShotComponent = -playercontroller.yComponent;

		//finding the magnitude of the x and y vector component addition
		magnitude = Mathf.Sqrt (xShotComponent*xShotComponent + yShotComponent*yShotComponent);

		selectedProjectile = assignedProjectile;

		theta = Mathf.Atan (yShotComponent / xShotComponent);
		shotAngle = theta + Mathf.Deg2Rad * Random.Range (-selectedProjectile.spread, selectedProjectile.spread);
		shotAngleD = shotAngle * Mathf.Rad2Deg;


		if (xShotComponent > 0 && yShotComponent > 0) 
		{
			//we are in Q1
			shotY = magnitude * Mathf.Sin(shotAngle);
			shotX = magnitude * Mathf.Cos (shotAngle); 
		}
		else if (xShotComponent < 0 && yShotComponent > 0) 
		{
			//we are in Q2
			shotY = magnitude * -Mathf.Sin(shotAngle);
			shotX = magnitude * -Mathf.Cos (shotAngle); 
		}
		else if (xShotComponent < 0 && yShotComponent < 0) 
		{
			//we are in Q3
			shotY = magnitude * -Mathf.Sin(shotAngle);
			shotX = magnitude * -Mathf.Cos (shotAngle); 
		}
		else if (xShotComponent > 0 && yShotComponent < 0) 
		{
			//we are in Q4
			shotY = magnitude * Mathf.Sin(shotAngle);
			shotX = magnitude * Mathf.Cos (shotAngle); 
		}








		if (Mathf.Abs(xShotComponent) > 0 && Mathf.Abs(yShotComponent) > 0) {
			
			theRB.velocity = new Vector2 (selectedProjectile.shotSpeed * shotX/magnitude, shotY/magnitude * selectedProjectile.shotSpeed);
			//theRB.velocity = new Vector2 (selectedProjectile.shotSpeed * (xShotComponent / (Mathf.Abs (magnitude))), (-yShotComponent / (Mathf.Abs (magnitude))) * selectedProjectile.shotSpeed);
		}
		else if (Mathf.Abs(xShotComponent) > 0 && Mathf.Abs(yShotComponent) == 0)
		{
			theRB.velocity = new Vector2 (selectedProjectile.shotSpeed * transform.localScale.x, Random.Range(-selectedProjectile.spread, selectedProjectile.spread));

		}
		else if (Mathf.Abs(xShotComponent) == 0 && Mathf.Abs(yShotComponent) > 0)
		{
			theRB.velocity = new Vector2 (Random.Range(-selectedProjectile.spread, selectedProjectile.spread), yShotComponent*selectedProjectile.shotSpeed);
		}
		else
		{
			
			theRB.velocity = new Vector2 (selectedProjectile.shotSpeed * transform.localScale.x, Random.Range(-selectedProjectile.spread, selectedProjectile.spread));
		}
		GetComponent<DestroyOverTime> ().lifeTime = selectedProjectile.shotSpeed/((1/selectedProjectile.range)*100); 



	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerEnter2D(Collider2D other)
    {
		/*if (other.tag == "Player 1")
		{
			FindObjectOfType<GameManager>().HurtP1 ();
			damageTakerTag = other.tag;
		}
		if (other.tag == "Player 2")
		{
			FindObjectOfType<GameManager>().HurtP2 ();
			damageTakerTag = other.tag;
		
		}*/


		if (SceneManager.GetActiveScene ().name != "Shop") {
			damageTakerTag = other.tag;


			GameManager gameManager = FindObjectOfType<GameManager> ();
			gameManager.onDamage (selectedProjectile.damage, damageDealerTag, damageTakerTag);

			Instantiate (sparkEffect, transform.position, transform.rotation);

			Destroy (gameObject);
		} else if (SceneManager.GetActiveScene ().name == "Shop") {
		
			//ShopManager shopManager = FindObjectOfType<ShopManager> ();
		
		}
    }
}
