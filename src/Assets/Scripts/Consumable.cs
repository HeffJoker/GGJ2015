using UnityEngine;
using System.Collections;

public class Consumable : Collectible {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void DoAction()
	{
		if(IsBeingCarried)
		{
			Detach();
		}
	}
}
