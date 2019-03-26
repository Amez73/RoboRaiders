using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {


	public GameObject ammoObject;
	private Projectile ammoProjectile;
	private GameObject myPlayer;
	private PlayerController myPlayerController;
	private Transform shootPoint;

	private float localFireRate;

	float timeToFire = 0;


	// Use this for initialization
	void Start () {

		ammoProjectile = ammoObject.GetComponent<Projectile>();
		Debug.Log (ammoProjectile.selectedProjectile.description);
		localFireRate = ammoProjectile.selectedProjectile.fireRate;
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
			//create a bullet
		}

		else
		{
			if (Time.time > timeToFire) 
			{
				timeToFire = Time.time + 1 / localFireRate;
				Summon ();
			}
		}

	}

	private void Summon(){
		Debug.Log ("Shooting!");
		GameObject fireBallClone = (GameObject)Instantiate(ammoObject, this.transform.position, this.transform.rotation);
		Debug.Log (transform.position);
	
	}
}
