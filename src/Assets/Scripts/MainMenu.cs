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
		Application.LoadLevel ("main_scene");
	}
	public void ExitGame()
	{
		Application.Quit ();
	}
}
