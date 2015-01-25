using UnityEngine;
using System.Collections;
using InControl;

public class PauseMenu : MonoBehaviour {
	 bool isPaused = false;
	Canvas canvas;
	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {

		SetVisability ();

		InputDevice input = InputManager.ActiveDevice;
		if(input.MenuWasPressed) //Input.GetButtonDown("PauseGame"))
		{
			if(!isPaused)
			{
				Time.timeScale = 0;
				isPaused=true;
			}
			else
			{
				Time.timeScale=1;
				isPaused=false;
			}
		}
		if(input.Action1 && isPaused)
			ReturnToMenu();


	}
	public void ReturnToMenu()
	{
		isPaused = false;
		Time.timeScale = 1;
		PlayerManager manager = FindObjectOfType<PlayerManager>();

		if(manager != null)
			Destroy(manager);
		Application.LoadLevel ("MainMenu_scene");
	}
	void SetVisability()
	{
		if(isPaused)
		{
			canvas.enabled = true;
		}
		else
			canvas.enabled = false;
	}
}
