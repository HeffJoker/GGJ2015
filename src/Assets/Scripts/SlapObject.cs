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
		sprite.enabled = true;

		Invoke("DoDisable", disableTime);
	}

	void DoDisable()
	{
		collider2D.enabled = false;
		sprite.enabled = false;
		isSlapping = false;
	}

	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		DoDisable();
	}
}
