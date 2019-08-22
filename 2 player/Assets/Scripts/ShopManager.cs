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
	public void Buy (Gun chosenGun, string purchasingPlayerTag)
	{


		GameObject purchasingPlayer = GameObject.FindGameObjectWithTag(purchasingPlayerTag);
		PlayerController purchasingPlayerController = purchasingPlayer.GetComponent<PlayerController>();

		switch (purchasingPlayerTag) 
		{
		case "Player 2":
			if (GameManager.p2Scrap >= chosenGun.gunStats.cost && purchasingPlayerController.assignedProjectile != chosenGun) 
			{
				GameManager.p2Scrap -= chosenGun.gunStats.cost;
				Debug.Log (GameManager.p2Scrap);
				//purchasingPlayerController.assignedProjectile = chosenGun;
				//GameManager.p2Projectile = chosenGun;
				//purchasingPlayerController.assignedProjectile = GameManager.p2Projectile;
				//Debug.Log (GameManager.p1Projectile);
				purchasingPlayerController.AssignGun (chosenGun);
			

			}
			else{
				Debug.Log("Not Enough Cash or Already Owned");
				Debug.Log (GameManager.p2Scrap);
			}
			break;

		case "Player 1":
			if (GameManager.p1Scrap >= chosenGun.gunStats.cost && purchasingPlayerController.assignedProjectile != chosenGun) 
			{
				GameManager.p1Scrap -= chosenGun.gunStats.cost;
				Debug.Log (GameManager.p1Scrap);
				//purchasingPlayerController.assignedProjectile = chosenGun;
				//Debug.Log (GameManager.p2Projectile);
				//GameManager.p1Projectile = chosenGun;
				//purchasingPlayerController.assignedProjectile = GameManager.p1Projectile;
				purchasingPlayerController.AssignGun (chosenGun);


			}
			else{
				Debug.Log("Not Enough Cash or Already Owned");
				Debug.Log (GameManager.p1Scrap);
			}
			break;

		default:
			Debug.Log ("player not in switch cases");
			break;

		}
	}
}
