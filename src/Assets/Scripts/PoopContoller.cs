using UnityEngine;
using System.Collections;

public class PoopContoller : MonoBehaviour {
	public float curPoop;
	public float poopInc = .1f;
	public bool gameOver=false;
	// Use this for initialization
	void Start () {
		curPoop = 0;
		StartCoroutine ("IncreasePoop");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator IncreasePoop()
	{
		while(!gameOver)
		{
			yield return new WaitForSeconds (.1f);
			curPoop+=poopInc;
		}
	}

	public float GetPoop()
	{
		return curPoop;
	}
}
