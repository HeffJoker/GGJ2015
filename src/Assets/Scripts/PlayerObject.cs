using UnityEngine;
using System.Collections;
using InControl;

public class PlayerObject : MonoBehaviour {

	public float knockbackForce = 20;
	public Transform MountPos;
	public SlapObject slapObj;

	private MoveObj mover;
	private Transform goalTrans;

	// Use this for initialization
	void Start () {
		mover = GetComponent<MoveObj>();
	}
	
	// Update is called once per frame
	void Update () {

		HandleSlap();
		HandleMirror();
	}

	void HandleSlap()
	{
		if(mover.controlIndex < 0 || mover.controlIndex >= InputManager.Devices.Count)
			return;

		InputDevice input = InputManager.Devices[mover.controlIndex];

		if(input.AnyButton.WasPressed)
			slapObj.DoSlap();
	}

	void HandleMirror()
	{
		Vector2 dir = mover.MoveDir.normalized;
		float dirVal = transform.localScale.x;

		if(dir.x > 0)
			dirVal = Mathf.Abs(dirVal);
		else if(dir.x < 0)
		{
			if(dirVal > 0)
				dirVal = -dirVal;
		}
		
		transform.localScale = new Vector3(dirVal, transform.localScale.y, transform.localScale.z);
	}

	void HandleKnockback(Vector3 dir)
	{
		dir.Normalize();
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(dir * knockbackForce);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("GoalObj"))
		{
			GoalObject goalObj = collider.GetComponent<GoalObject>();

			if(goalObj.IsBeingCarried)
				return;

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

			goalObj.IsBeingCarried = true;
			goalTrans = collider.transform;
		}
		else if(collider.CompareTag("SlapObj"))
		{
			if(goalTrans != null)
				goalTrans.parent = transform.parent;

			HandleKnockback(transform.position - collider.transform.position);
		}
	}
}
