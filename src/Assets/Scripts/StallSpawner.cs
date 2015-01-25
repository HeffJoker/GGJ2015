using UnityEngine;
using System.Collections;
using InControl;

public class StallSpawner : MonoBehaviour {

	public float controlTimeout = 2f;
	public float spawnTimeout = 1f;
	public float spawnForce = 1000f;
	public Vector2 spawnDir = Vector2.zero;
	public int playerIndex = 0;
	public WinMenu winMenu;
	public PauseMenu pauseMenu;
	public PlayerObject debugPlayerObj;
	public bool ignorePlayerSelection = false;

	private PlayerObject playerObj;

	public PlayerObject PlayerObj
	{
		get { return playerObj; }
	}

	// Use this for initialization
	void Awake () {

		if(ignorePlayerSelection)
		{
			playerObj = debugPlayerObj;
			if(playerIndex >= InputManager.Devices.Count)
				playerObj.gameObject.SetActive(false);
			else
			{
				playerObj.InDevice = InputManager.Devices[playerIndex];
				StartSpawn();
			}
		}
		else
		{
			if(debugPlayerObj != null)
				debugPlayerObj.gameObject.SetActive(false);

			PlayerManager manager = GameObject.FindObjectOfType<PlayerManager>();

			if(manager == null)
				Debug.LogError("Could not find PlayerManager object!!");
			else
			{
				if(playerIndex < manager.Players.Count && playerIndex >= 0)
				{
					playerObj = manager.Players[playerIndex];
					playerObj.gameObject.SetActive(true);
					StartSpawn();
				}
				else
					Debug.LogError(string.Format("Could not find a player for index = {0}", playerIndex));
			}
		}
	}

	public void StartSpawn()
	{
		playerObj.transform.position = transform.position;
		playerObj.DisableCollision();
		Invoke("FinishSpawn", spawnTimeout);
	}

	private void FinishSpawn()
	{
		playerObj.rigidbody2D.velocity = Vector2.zero;
		playerObj.rigidbody2D.AddForce(spawnDir * spawnForce);
		playerObj.Invoke("EnableControl", controlTimeout);
		playerObj.Invoke("EnableCollision", controlTimeout);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Player"))
		{
			if(collider.gameObject == playerObj.gameObject && playerObj.HasGoalObj)
			{
				winMenu.Show(playerObj.name);
				pauseMenu.enabled = false;
			}
		}
	}
}
