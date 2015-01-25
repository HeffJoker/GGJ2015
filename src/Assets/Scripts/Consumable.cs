using UnityEngine;
using System.Collections;

public class Consumable : Collectible {

	public enum itemTypes { Pepto, Burrito };
	public itemTypes itemType = itemTypes.Burrito;
	public float effectStrength = 1;

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
				if(attachedPlayer.fartObj!=null)
					attachedPlayer.fartObj.DoSlap();
			break;

			case (itemTypes.Pepto):
				//Reduce poop timer
				PoopBar poobar = attachedPlayer.GetComponent<PoopContoller>().poopBar;
				
			if(poobar.curPoop - effectStrength >= 0)
				poobar.curPoop -= effectStrength;
			else
				poobar.curPoop = 0;

			break;
		}
	}
}
