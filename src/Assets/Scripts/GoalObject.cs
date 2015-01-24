using UnityEngine;
using System.Collections;

public class GoalObject : MonoBehaviour {

	public float carryTimeout = 2f;

	private PlayerObject lastPlayer = null;
	private bool isCarried = false;

	public bool IsBeingCarried 
	{
		get { return isCarried; }
	}

	public bool Attach(PlayerObject player)
	{
		if(lastPlayer == player)
			return false;

		lastPlayer = player;
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
		return true;
	}

	public void Detach()
	{
		transform.parent = transform.parent.parent.parent; 
		isCarried = false;
		Invoke("DetachFromPlayer", carryTimeout);
	}

	void DetachFromPlayer()
	{
		lastPlayer = null;
	}
}
