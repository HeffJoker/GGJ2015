using UnityEngine;
using System.Collections;

public class Consumable : Collectible {

	public enum itemTypes { Pepto, Burrito };
	public itemTypes itemType = itemTypes.Burrito;

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
			doEffect();
			GetComponent<SpriteRenderer>().enabled=false;
			Destroy(this);
		}
	}

	public void doEffect()
	{
		switch(itemType)
		{
			case (itemTypes.Burrito):
				//Big fart effect
			break;

			case (itemTypes.Pepto):
				//Reduce poop timer
			break;
		}
	}
}
