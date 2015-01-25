using UnityEngine;
using System.Collections;

public class GoalObject : Collectible {

	public override void DoAction()
	{
		if(IsBeingCarried)
			Detach();

		//Set dropped position
		//transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f);

		//Move dropped item
		if(rigidbody2D != null)
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.AddForce(transform.position.normalized * 10f);
		}
		else
			Debug.LogError("Collectible '" + gameObject.name + "' doesn't have a rigidbody");
	}
}
