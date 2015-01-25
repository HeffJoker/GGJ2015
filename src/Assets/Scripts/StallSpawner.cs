using UnityEngine;
using System.Collections;

public class StallSpawner : MonoBehaviour {

	public float controlTimeout = 2f;
	public float spawnTimeout = 1f;
	public float spawnForce = 1000f;
	public Vector2 spawnDir = Vector2.zero;
	public int PlayerIndex = 0;
	public WinMenu winMenu;
	public PauseMenu pauseMenu;

	private PlayerObject playerObj;

	// Use this for initialization
	void Start () {
		PlayerManager manager = GameObject.FindObjectOfType<PlayerManager>();

		if(manager == null)
			Debug.LogError("Could not find PlayerManager object!!");
		else
		{
			if(PlayerIndex < manager.Players.Count && PlayerIndex >= 0)
			{
				playerObj = manager.Players[PlayerIndex];
				playerObj.gameObject.SetActive(true);
				StartSpawn();
			}
			else
				Debug.LogError(string.Format("Could not find a player for index = {0}", PlayerIndex));
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
