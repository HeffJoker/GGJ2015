using UnityEngine;
using System.Collections;
using InControl;

public class PlayerObject : MonoBehaviour {

	public float knockbackForce = 20;
	public float stunTimeout = 2f;
	public Transform MountPos;
	public SlapObject slapObj;
	public SlapObject fartObj;
	public string playerName = string.Empty;

	private MoveObj mover;
	private Collectible currItem;
	private bool canGrabItem = true;
	private bool inStall = false;
	private InputDevice inDevice;

	public bool InStall
	{
		get { return inStall; }
		set { inStall = value; }
	}

	public bool HasGoalObj
	{
		get 
		{ 
			if(currItem != null)
				return currItem.tag == "GoalObj"; 
			else 
				return false;
		}
	}

	public Collectible CurrItem
	{
		get { return currItem; }
	}

	public InputDevice InDevice
	{
		get { return inDevice; }
		set 
		{
			inDevice = value;
			mover.InDevice = value;
		}
	}

	// Use this for initialization
	void Awake () {
		mover = GetComponent<MoveObj>();
		mover.InDevice = InDevice;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		HandleSlap();
		HandleMirror();
	}

	void HandleSlap()
	{
		//if(mover.controlIndex < 0 || mover.controlIndex >= InputManager.Devices.Count)
		//	return;

		InputDevice input = InDevice; //InputManager.Devices[mover.controlIndex];

		if(input.AnyButton.WasPressed && !inStall)
		{
			if(currItem == null)
				slapObj.DoSlap();
			else
			{
				currItem.DoAction();
				currItem = null;
			}
		}
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
		if(collider.CompareTag("GoalObj") || collider.CompareTag("Projectile") || collider.CompareTag("Consumable"))
		{
			Collectible collectible = null;

			switch(collider.tag)
			{
				case "GoalObj":
					collectible = collider.GetComponent<GoalObject>();
				break;
				case "Projectile":
					collectible = collider.GetComponent<Projectile>();
				break;
				case "Consumable":
					collectible = collider.GetComponent<Consumable>();
				break;
			}

			if(collectible==null || collectible.IsBeingCarried || collectible.isBeingThrown || collectible == currItem)
				return;

			if(currItem != null)
			{
				currItem.Detach();
				currItem = null;
			}

			if(canGrabItem && collectible.Attach(this))
			{
				currItem = collectible;
				canGrabItem = false;
				StartCoroutine(itemGrabCooldown());
			}
		}
		else if(collider.CompareTag("SlapObj"))
		{
			if(currItem != null)
			{
				currItem.Detach();
				currItem = null;
			}

			if(mover.enabled)
			{
				DisableControl();
				Invoke("EnableControl", stunTimeout);
				HandleKnockback(transform.position - collider.transform.position);
			}
		}
	}

	IEnumerator itemGrabCooldown()
	{
		yield return new WaitForSeconds(0.75f);
		canGrabItem = true;
	}

	/*
	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.CompareTag("GoalObj") || collider.CompareTag("Collectible"))
		{
			Collectible goalObj = collider.GetComponent<Collectible>();
			
			if(goalObj.IsBeingCarried)
				return;
			
			if(goalObj.Attach(this))
				currItem = goalObj;
		}
	}
	*/
}
