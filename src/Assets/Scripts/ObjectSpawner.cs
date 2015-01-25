using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
	private GameObject leftBound,rightBound,topBound,bottomBound;
	private float padding = 2;
	public Transform[] pickUps= new Transform[4];
	// Use this for initialization
	void Start () {
	
		leftBound = GameObject.Find ("left");
		rightBound = GameObject.Find ("right");
		topBound = GameObject.Find ("top");
		bottomBound = GameObject.Find ("bottom");


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.A))
			SpawnObject();
	}

	Vector2 RandomLoc()
	{
		float xLoc = Random.Range ((leftBound.transform.position.x + padding), (rightBound.transform.position.x - padding));
		float yLoc = Random.Range ((topBound.transform.position.y - padding), (bottomBound.transform.position.y + padding));
		Vector2 loc = new Vector2 (xLoc, yLoc);
		return loc;
	}
	Transform RandomObj()
	{
		int rand = Random.Range (0, pickUps.Length);
		return pickUps [rand];
	}

	void SpawnObject()
	{
		Vector2 spawnLoc=RandomLoc();
		Transform obj = RandomObj ();
		Instantiate (obj, spawnLoc, Quaternion.identity);


	}

}
