using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {

	public string level1;
	public string level2;
	public string level3;
	public string level4;

	public EventSystem ES;
	private GameObject StoreSelected;

	void Start ()
	{
		StoreSelected = ES.firstSelectedGameObject;
	}

	void Update ()
	{
		if (ES.currentSelectedGameObject != StoreSelected) 
		{
			if (ES.currentSelectedGameObject == null) {
				ES.SetSelectedGameObject (StoreSelected);
			} 
			else 
			{
				StoreSelected = ES.currentSelectedGameObject;
			}
		}
	}
	public void Startlvl1()
	{
		GameManager.p1RoundWins = 0;
		GameManager.p2RoundWins = 0;
		GameManager.p1Scrap = 0;
		GameManager.p2Scrap = 0;
		//loadscene will be removed soon but theres a bug with set active scene
		SceneManager.LoadScene (level1);
	}
	public void Startlvl2()
	{
		SceneManager.LoadScene (level2);
	}
	public void Startlvl3()
	{
		SceneManager.LoadScene (level3);
	}
	public void Startlvl4()
	{
		SceneManager.LoadScene (level4);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
