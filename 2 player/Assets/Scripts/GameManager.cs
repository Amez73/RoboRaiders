using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public KeyCode menu;
	public KeyCode exit;
	public KeyCode toShop;

	public GameObject player1;
	public GameObject player2;

	[SerializeField]
	private int p1Health = 100;
	[SerializeField]
	private int p2Health = 100;

	public int p1MaxHealth = 100;
	public int p2MaxHealth = 100;

	public int roundsToWin = 7;

	public GameObject roundScreen;
	public GameObject GameOverScreen;


	public UnityEngine.UI.Slider p1Slider;
	public UnityEngine.UI.Slider p2Slider;

	public static int p1Scrap = 0;
	public static int p2Scrap = 0;

	public static ProjectileObject[] projectileList;

	[SerializeField]
	public static ProjectileObject p1Projectile;
	[SerializeField]
	public static ProjectileObject p2Projectile;
	public ProjectileObject p1StartProjectile;
	public ProjectileObject p2StartProjectile;

	public AudioSource hurtSound;
	public AudioSource thudSound;

	public string mainMenu;
	public string shop;


	public static int p1RoundWins = 0;
	public static int p2RoundWins = 0;

	private bool p1Died = false;
	private bool p2Died = false;

	private string winningPlayerRound;
	private string winningPlayerGame;

	public Text scoreText;
	public Text scrapText;
	public Text winnerText;

	// Use this for initialization
	void Start () {

		if(!p1Projectile || !p2Projectile)
		{
			Debug.Log("false was right");
			p1Projectile = p1StartProjectile;
			p2Projectile = p2StartProjectile;
		}

		p1Slider.maxValue = p1MaxHealth;
		p2Slider.maxValue = p2MaxHealth;

		p1Health = p1MaxHealth;
		p2Health = p2MaxHealth;

		p1Slider.value = p1MaxHealth;
		p2Slider.value = p2MaxHealth;

	}

	// Update is called once per frame
	void Update () {
		//checking if p1 is dead
		if (p1Health <= 0)
		{
			player1.SetActive (false);
			p1Died = true;
		}
		//checking if p2 is dead
		if (p2Health <= 0)
		{

			player2.SetActive (false);
			p2Died = true;
		}
		//reloading scene on button press
		if (Input.GetKeyDown (menu))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		//loading main menu scene
		if (Input.GetKeyDown (exit)) 
		{
			SceneManager.LoadScene(mainMenu);
		}
		if (roundScreen.activeSelf && Input.GetKeyDown(toShop)) 
		{
			SceneManager.LoadScene(shop);
		}
			
		if (!roundScreen.activeSelf && (p1Died || p2Died) ) {
			if (!p1Died && p2Died) 
			{
				winningPlayerRound = "Player 1";
				p1RoundWins += 1;
				//Debug.Log ("Player 1: " + p1RoundWins +"\n"+ "Player 2: " + p2RoundWins);
				p2Died = false;
				// garbage solution that stops from infinitely giving round points
				p2Health = 1;

			}
			if (p1Died && !p2Died) 
			{
				winningPlayerRound = "Player 2";
				p2RoundWins += 1;
				//Debug.Log ("Player 1: " + p1RoundWins +"\n"+ "Player 2: " + p2RoundWins);
				p1Died = false;
				// garbage solution that stops from infinitely giving round points
				p1Health = 1;

			}
			scoreText.text = "Player 1 Wins: " + p1RoundWins + "\n" + "Player 2 Wins: " + p2RoundWins;
			scrapText.text = "Player 1 Scrap: " + p1Scrap + "\n" + "Player 2 Scrap: " + p2Scrap;
			winnerText.text = winningPlayerRound + " Won!";
			roundScreen.SetActive (true);
		}
		if (p1RoundWins > roundsToWin-1 || p2RoundWins > roundsToWin-1) 
		{
			if (p1RoundWins > roundsToWin-1) {
				winningPlayerGame = "Player 1";
			}
			if (p2RoundWins > roundsToWin-1) {
				winningPlayerGame = "Player 2";
			}

			winnerText.text = winningPlayerGame + " Won the Game!";
			roundScreen.SetActive (false);
			GameOverScreen.SetActive (true);
		}

	}

	public void onDamage (int damageDealt, string damageDealerTag, string damageTakerTag)
	{
		//dealing damage to player
		if (damageDealerTag == damageTakerTag) {
			return;
		} else 
		{
			thudSound.Play ();
			if (damageTakerTag == "Player 1") {

				p1Health -= damageDealt;
				hurtSound.Play ();
				p1Slider.value = p1Health;

				//rewarding player with scrap for damage
				if (damageDealerTag == "Player 1") {
					p1Scrap += damageDealt * 10;
				}
				if (damageDealerTag == "Player 2") {
					p2Scrap += damageDealt * 10;
				}

			}
			if (damageTakerTag == "Player 2") {

				p2Health -= damageDealt;
				hurtSound.Play ();
				p2Slider.value = p2Health;

				//rewarding player with scrap for damage
				if (damageDealerTag == "Player 1") {
					p1Scrap += damageDealt * 10;
				}
				if (damageDealerTag == "Player 2") {
					p2Scrap += damageDealt * 10;
				}
			}
		}
			
			

		
	}
	/*
	public ProjectileObject assignProjectile (string playerTag)
	{
		if (playerTag == "Player 1") 
		{
			return p1Projectile;
		}
		if (playerTag == "Player 2") {
			return p2Projectile;
		} else 
		{	
			return projectileList[0];
		}

	}*/
}
