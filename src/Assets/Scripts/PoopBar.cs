using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PoopBar : MonoBehaviour {
	private RectTransform bar;
	private float maxWidth;
	private float curWidth=0;
	private PoopContoller pc;
	private GameObject player;

	public float maxPoop=100;
	public float curPoop=50;
	public string playerName;
	// Use this for initialization
	void Start () {
		bar = GetComponent<RectTransform> ();
		player = GameObject.Find (playerName);
		pc = player.GetComponent<PoopContoller> ();
		maxWidth = bar.rect.width;
	}
	
	// Update is called once per frame
	void Update () {

		curPoop = pc.GetPoop();
		curWidth = (curPoop/maxPoop)*maxWidth;

	
		if (curPoop < 0)
			curPoop = 0;
		if (curPoop >= maxPoop)
			curPoop = maxPoop;
		if (curWidth>maxWidth)
			curWidth=maxWidth;
		bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,curWidth);
	}
}
