using UnityEngine;
using System.Collections;

public class StallSpawner : MonoBehaviour {

	public float controlTimeout = 2f;
	public float spawnTimeout = 1f;
	public float spawnForce = 1000f;
	public Vector2 spawnDir = Vector2.zero;
	public PlayerObject playerObj;
	public WinMenu winMenu;
	public PauseMenu pauseMenu;

	// Use this for initialization
	void Start () {
		StartSpawn();
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
