using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {


	public GameObject myProjectile;
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
	public void Shoot(Transform shootPosition)
	{
		Debug.Log ("Pew");
		//create the projectile
		GameObject fireBallClone = (GameObject)Instantiate(myProjectile, shootPosition.position, shootPosition.rotation);
		
	}
}
