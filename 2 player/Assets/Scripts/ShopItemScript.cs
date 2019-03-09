﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemScript : MonoBehaviour {

	public ProjectileObject projectileData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Projectile") 
		{

			//Example
			//GameManager gameManager = FindObjectOfType<GameManager>();
			//gameManager.Buy(projectileData, other.GetComponent<Projectile>().damageDealerTag);

			Debug.Log ("Button Shot!");
			ShopManager shopManager = FindObjectOfType<ShopManager>();
			shopManager.Buy(projectileData, other.GetComponent<Projectile>().damageDealerTag);
		}
	}
}