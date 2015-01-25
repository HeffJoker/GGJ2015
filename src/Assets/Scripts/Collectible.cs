using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	public float carryTimeout = 2f;
	
	private PlayerObject attachedPlayer = null;
	private bool isCarried = false;
	
	public bool IsBeingCarried 
	{
		get { return isCarried; }
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
		GetComponent<CircleCollider2D>().enabled = false;
		return true;
	}
	
	public void Detach()
	{
		//Transform oldObj = transform.parent.parent;
		transform.parent = getTopmostTransform(transform); // transform.parent.parent.parent; 
		transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f);
		//rigidbody2D.AddForce(transform.position.normalized * 50f);
		isCarried = false;
		GetComponent<CircleCollider2D>().enabled = true;
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
