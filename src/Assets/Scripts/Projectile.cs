using UnityEngine;
using System.Collections;

public class Projectile : Collectible {

	private GameObject throwingPlayer = null;

	public enum projectileType {Plunger, Laxative};
	public projectileType objType = projectileType.Plunger;
	public float speed = 30f;
	public float pushback = 60f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void DoAction()
	{
		throwingPlayer = attachedPlayer.gameObject;
		Detach();

		isThrown = true;

		//Move dropped item
		if(rigidbody2D != null)
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce(throwingPlayer.rigidbody2D.velocity.normalized * speed);
		}
		else
			Debug.LogError("Collectible '" + gameObject.name + "' doesn't have a rigidbody");
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if( !isThrown || collider.gameObject == throwingPlayer)
			return;

		if(collider.CompareTag("Player") || collider.CompareTag("Finish"))
		{
			Debug.Log("projectile '" + gameObject.name + "' hit '" + collider.gameObject.name + "'");
			doEffect(collider.gameObject);
			GetComponent<SpriteRenderer>().enabled=false;
			Destroy(this);
		}
	}

	void doEffect(GameObject affectedObj)
	{
		if(affectedObj.CompareTag("Player"))
		{
			affectedObj.rigidbody2D.AddForce(rigidbody2D.velocity.normalized * pushback);

			switch(objType)
			{
				case projectileType.Laxative:
				break;

				case projectileType.Plunger:
				break;
			}
		}
	}
}
