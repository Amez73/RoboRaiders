using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {


	public GameObject ammoObject;
	private Projectile ammoProjectile;
	public GunStats gunStats;
	private GameObject myPlayer;
	private PlayerController myPlayerController;
	private Transform shootPoint;

	private GameObject ammoInstance;
	private Rigidbody2D theRB;

	private float localFireRate;
	private float localShotSpeed;

	float timeToFire = 0;


	// Use this for initialization
	void Start () {

		myPlayer = transform.parent.gameObject;
		ammoProjectile = ammoObject.GetComponent<Projectile>();
		Debug.Log (ammoProjectile.selectedProjectile.description);
		localFireRate = ammoProjectile.selectedProjectile.fireRate;
		localShotSpeed = ammoProjectile.selectedProjectile.shotSpeed;
		shootPoint = transform.Find("shootPoint");
		//GameObject myPlayer = this.transform.parent.gameObject;
		//GameObject myGameObject = this.transform.gameObject;
		//PlayerController myPlayerController = myPlayer.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Shoot()
	{
		if (localFireRate == 0) {

			Summon();
			Throw ();
			//create a bullet
		}

		else
		{
			if (Time.time > timeToFire) 
			{
				timeToFire = Time.time + 1 / localFireRate;
				Summon ();
				Throw ();
			}
		}

	}

	private void Summon(){
		Debug.Log ("Shooting!");
		ammoInstance = (GameObject)Instantiate(ammoObject, shootPoint.position, shootPoint.rotation);
		Projectile projectileScript = ammoInstance.GetComponent<Projectile>();
		projectileScript.AssignOwner(myPlayer.tag);
		theRB = ammoInstance.GetComponent<Rigidbody2D>();
	
	}
	private void Throw(){
		
		//apply velocity along normalized vector between shootpoint and transform

		theRB.velocity = new Vector2 (localShotSpeed*myPlayer.transform.localScale.x,0f);
		ammoInstance.transform.localScale = myPlayer.transform.localScale;
	
	}
}
