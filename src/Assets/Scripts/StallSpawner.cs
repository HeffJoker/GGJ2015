using UnityEngine;
using System.Collections;
using InControl;

public class StallSpawner : MonoBehaviour {

	public float controlTimeout = 2f;
	public float spawnTimeout = 1f;
	public float spawnForce = 1000f;
	public float gruntIncrement = 10f;
	public Vector2 spawnDir = Vector2.zero;
	public int playerIndex = 0;
	public WinMenu winMenu;
	public PauseMenu pauseMenu;
	public PoopBar poopBar;
	public PoopBar gruntBar;
	public PlayerObject debugPlayerObj;
	public bool ignorePlayerSelection = false;
	public GameObject startPos = null;

	private bool playerInStall = false;
	private PlayerObject playerObj;
	private PoopContoller pController;

	public PlayerObject PlayerObj
	{
		get { return playerObj; }
	}

	// Use this for initialization
	void Awake () {
		Time.timeScale = 1;
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

	public void Update()
	{
		if(playerInStall)
		{
			InputDevice inDevice = playerObj.InDevice;

			if(inDevice.AnyButton.WasPressed)
				gruntBar.Increment(gruntIncrement);
		}

		if(gruntBar.ReachedMax)
		{
			winMenu.Show(playerObj.name);
			pauseMenu.enabled = false;
		}
	}

	public void StartSpawn()
	{
		gruntBar.transform.parent.gameObject.SetActive(false);

		pController = playerObj.GetComponent<PoopContoller>();
		pController.poopBar = poopBar;

		if(startPos != null)
			playerObj.transform.position = startPos.transform.position;
		else
			Debug.LogError("No start pos set for " + playerObj.name );


		playerObj.DisableControl();
		playerObj.DisableCollision();

		if(playerObj.CurrItem != null)
		{
			Destroy(playerObj.CurrItem.gameObject);
		}

		Invoke("FinishSpawn", spawnTimeout);
	}

	private void FinishSpawn()
	{
		pController.StartPooping();
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
				playerInStall = true;
				playerObj.InStall = true;
				gruntBar.transform.parent.gameObject.SetActive(true);

				pController.StopPooping();
				pController.poopBar.gameObject.SetActive(false);
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.CompareTag("Player"))
		{
			if(collider.gameObject == playerObj.gameObject && playerObj.HasGoalObj)
			{
				playerInStall = false;
				playerObj.InStall = false;
				gruntBar.transform.parent.gameObject.SetActive(false);
				
				pController.StartPooping();
				pController.poopBar.gameObject.SetActive(true);
			}
		}
	}
}
