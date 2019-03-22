using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {


	public GameObject Ammo;
	private ProjectileObject ammoStats;
	private GameObject myPlayer;
	private PlayerController myPlayerController;
	public Transform shootPosition;

	float timeToFire = 0;


	// Use this for initialization
	void Start () {

		ammoStats = Ammo.GetComponent<Projectile>().selectedProjectile;
		shootPosition = GetComponentInChildren<Transform> ();
		//GameObject myPlayer = this.transform.parent.gameObject;
		//GameObject myGameObject = this.transform.gameObject;
		//PlayerController myPlayerController = myPlayer.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Shoot()
	{

		if (ammoStats.fireRate == 0) {

			Summon();
			//create a bullet
		}

		else
		{
			if (Time.time > timeToFire) 
			{
				timeToFire = Time.time + 1 / ammoStats.fireRate;
				Shoot ();
			}
		}
		Debug.Log ("Pew");
		//create the projectile
		Summon();

	}

	private void Summon(){
	
		GameObject fireBallClone = (GameObject)Instantiate(Ammo, shootPosition.position, shootPosition.rotation);
	
	}
}
