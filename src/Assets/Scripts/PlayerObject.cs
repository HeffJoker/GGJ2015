using UnityEngine;
using System.Collections;
using InControl;

public class PlayerObject : MonoBehaviour {

	public float knockbackForce = 20;
	public float stunTimeout = 2f;
	public Transform MountPos;
	public SlapObject slapObj;
	public string playerName = string.Empty;

	private MoveObj mover;
	private GoalObject goalTrans;

	public bool HasGoalObj
	{
		get { return goalTrans != null; }
	}

	// Use this for initialization
	void Start () {
		mover = GetComponent<MoveObj>();
	}
	
	// Update is called once per frame
	void Update () {

		if(goalTrans == null)
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

	public void HandleKnockback(Vector3 dir)
	{
		dir.Normalize();
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(dir * knockbackForce);
	}

	public void DisableControl()
	{
		mover.enabled = false;
	}

	void EnableControl()
	{
		mover.enabled = true;
	}

	public void DisableCollision()
	{
		collider2D.enabled = false;
	}

	public void EnableCollision()
	{
		collider2D.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("GoalObj"))
		{
			GoalObject goalObj = collider.GetComponent<GoalObject>();

			if(goalObj.IsBeingCarried)
				return;

			if(goalObj.Attach(this))
				goalTrans = goalObj;
		}
		else if(collider.CompareTag("SlapObj"))
		{
			if(goalTrans != null)
			{
				goalTrans.Detach();
				goalTrans = null;
			}

			if(mover.enabled)
			{
				DisableControl();
				Invoke("EnableControl", stunTimeout);
				HandleKnockback(transform.position - collider.transform.position);
			}
		}
	}
}
