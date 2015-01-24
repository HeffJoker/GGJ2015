using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour {

	public Text promptText;
	public StallSpawner[] stalls;

	private Canvas menuObj;

	public void Show(string playerName)
	{
		promptText.text = string.Format("{0} wins!!", playerName);
		menuObj.enabled = true;
		Time.timeScale = 0;
	}

	// Use this for initialization
	void Awake () {
		menuObj = GetComponent<Canvas>();
		menuObj.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		if(!menuObj.enabled)
			return;

		foreach(InputDevice device in InputManager.Devices)
		{
			if(device.AnyButton.WasPressed)
			{
				menuObj.enabled = false;
				Time.timeScale = 1;
				Application.LoadLevel("main_scene");
				//foreach(StallSpawner stall in stalls)
				//	stall.StartSpawn();
			}
		}
	}
}
