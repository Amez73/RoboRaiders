using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {


	public ProjectileObject myProjectile;
	private GameObject myPlayer;
	private PlayerController myPlayerController;


	// Use this for initialization
	void Start () {
		//GameObject myPlayer = this.transform.parent.gameObject;
		//GameObject myGameObject = this.transform.gameObject;
		//PlayerController myPlayerController = myPlayer.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Shoot()
	{
		Debug.Log ("Pew");
		//create the projectile
		GameObject fireBallClone = (GameObject)Instantiate(myProjectile, new Vector2(0f, 0f), Quaternion.identity);
		
	}
}
