using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	public float carryTimeout = 1f;	

	protected bool isThrown = false;
	protected PlayerObject attachedPlayer = null;
	private bool isCarried = false;

	public bool isBeingThrown
	{
		get {return isThrown;}
	}

	public bool IsBeingCarried 
	{
		get { return isCarried; }
	}

	// Use this for initialization
	void Start () {
	}
	
	public bool Attach(PlayerObject player)
	{
		if(attachedPlayer == player)
			return false;
		
		attachedPlayer = player;
		if(player.MountPos == null)
		{
			transform.position = transform.position;
			transform.parent = transform;
		}
		else
		{
			transform.position = player.MountPos.position;
			transform.parent = player.MountPos;
		}
		
		isCarried = true;
		GetComponent<Collider2D>().enabled = false;

		if(rigidbody2D != null)
			rigidbody2D.isKinematic = true;

		return true;
	}
	
	public void Detach()
	{
		//Transform oldObj = transform.parent.parent;
		transform.parent = getTopmostTransform(transform); // transform.parent.parent.parent; 
		isCarried = false;
		GetComponent<Collider2D>().enabled = true;
		Invoke("DetachFromPlayer", carryTimeout);
	}

	Transform getTopmostTransform(Transform trans)
	{
		if(trans.parent != null)
			return getTopmostTransform(trans.parent);
		else
			return trans.parent;
	}

	public virtual void DoAction()
	{}

	void DetachFromPlayer()
	{
		attachedPlayer = null;
	}
}
