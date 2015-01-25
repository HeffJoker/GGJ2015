using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerObject))]
public class PoopContoller : MonoBehaviour {
	public float curPoop;
	public float poopInc = .1f;
	public bool gameOver=false;
	public PoopBar poopBar;

	private PlayerObject player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerObject>();
		curPoop = 0;

	}

	public void StartPooping()
	{
		StartCoroutine ("IncreasePoop");
	}

	public void StopPooping()
	{
		StopCoroutine("IncreasePoop");
	}

	IEnumerator IncreasePoop()
	{
		while(!gameOver)
		{
			yield return new WaitForSeconds (.1f);
			poopBar.Increment(poopInc);

			if(poopBar.ReachedMax)
				; //player.Die(); // Do Game Over

			//curPoop+=poopInc;
		}
	}

	/*
	public float GetPoop()
	{
		return curPoop;
	}
	*/
}
