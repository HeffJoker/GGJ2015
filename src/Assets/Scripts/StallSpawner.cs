using UnityEngine;
using System.Collections;

public class StallSpawner : MonoBehaviour {

	public PlayerObject playerPrefab;
	public 

	// Use this for initialization
	void Start () {
		DoSpawn();
	}

	public void DoSpawn()
	{
		Instantiate(playerPrefab, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("PlayerObj"))
			;
	}
}
