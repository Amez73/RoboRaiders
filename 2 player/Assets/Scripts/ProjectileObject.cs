using UnityEngine;
using System.Collections;

[System.Serializable]
public class ProjectileObject : ScriptableObject {

	public string weaponName = "Weapon Name Here";
	public int cost = 50;
	public string description;

	public float fireRate = .5f;
	public int damage = 10;
	public float range = 2;
	public float shotSpeed = 1;
	public float spread = 1;

}