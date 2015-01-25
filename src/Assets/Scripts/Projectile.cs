using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Player") || collider.CompareTag("Finish"))
		{
			GetComponent<SpriteRenderer>().enabled=false;
			Destroy(this);
		}
	}
}
