using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

	public string lvl1;
	public KeyCode LeaveShopButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//going to shop between rounds
		if (Input.GetKeyDown(LeaveShopButton)) {
			SceneManager.LoadScene(lvl1);
		}
	}
	public void Buy (ProjectileObject chosenProjectile, string purchasingPlayerTag)
	{

		GameObject purchasingPlayer = GameObject.FindGameObjectWithTag (purchasingPlayerTag);
		PlayerController purchasingPlayerController = purchasingPlayer.GetComponent<PlayerController>();

		switch (purchasingPlayerTag) 
		{
		case "Player 2":
			if (GameManager.p2Scrap >= chosenProjectile.cost && purchasingPlayerController.assignedProjectile != chosenProjectile) 
			{
				GameManager.p2Scrap -= chosenProjectile.cost;
				Debug.Log (GameManager.p2Scrap);
				//purchasingPlayerController.assignedProjectile = chosenProjectile;
				GameManager.p2Projectile = chosenProjectile;
				purchasingPlayerController.assignedProjectile = GameManager.p2Projectile;
				Debug.Log (GameManager.p1Projectile);
			

			}
			else{
				Debug.Log("Not Enough Cash or Already Owned");
			}
			break;

		case "Player 1":
			if (GameManager.p1Scrap >= chosenProjectile.cost && purchasingPlayerController.assignedProjectile != chosenProjectile) 
			{
				GameManager.p1Scrap -= chosenProjectile.cost;
				Debug.Log (GameManager.p1Scrap);
				//purchasingPlayerController.assignedProjectile = chosenProjectile;
				Debug.Log (GameManager.p2Projectile);
				GameManager.p1Projectile = chosenProjectile;
				purchasingPlayerController.assignedProjectile = GameManager.p1Projectile;


			}
			else{
				Debug.Log("Not Enough Cash or Already Owned");
			}
			break;

		default:
			Debug.Log ("player not in switch cases");
			break;

		}
	}
}
