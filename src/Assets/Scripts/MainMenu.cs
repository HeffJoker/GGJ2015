using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void StartGame()
	{
		Application.LoadLevel ("character_select_scene");
	}

	public void ShowCredits()
	{
		Application.LoadLevel("credits_scene");
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
