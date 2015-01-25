using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class SlapObject : MonoBehaviour {

	public float disableTime = 0.5f;

	private SpriteRenderer sprite;
	private bool isSlapping = false;

	public void DoSlap()
	{
		if(isSlapping)
			return;

		isSlapping = true;
		collider2D.enabled = true; 
		if(sprite!=null)
			sprite.enabled = true;
	
		Invoke("DoDisable", disableTime);
	}

	void DoDisable()
	{
		collider2D.enabled = false;
		isSlapping = false;
		if(sprite!=null)
			sprite.enabled = false;

	}

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		DoDisable();
	}
}
