using UnityEngine;
using System.Collections;

public class PlayerObject : MonoBehaviour {

	public Transform MountPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("GoalObj"))
		{
			if(MountPos == null)
			{
				collider.transform.position = transform.position;
				collider.transform.parent = transform;
			}
			else
			{
				collider.transform.position = MountPos.position;
				collider.transform.parent = MountPos;
			}
		}
	}
}
