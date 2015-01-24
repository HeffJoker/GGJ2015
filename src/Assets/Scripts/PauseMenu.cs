﻿using UnityEngine;
using System.Collections;

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

		if(Input.GetButtonDown("PauseGame"))
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
		if(Input.GetButtonDown ("MainMenu")&& isPaused)
			ReturnToMenu();


	}
	public void ReturnToMenu()
	{
		isPaused = false;
		Time.timeScale = 1;
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