using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PoopBar : MonoBehaviour {
	private RectTransform bar;
	private float maxWidth;
	private float curWidth=0;

	public float maxPoop=100;
	public float curPoop=50;

	public bool ReachedMax
	{
		get { return curPoop >= maxPoop; }
	}

	// Use this for initialization
	void Awake () {
		bar = GetComponent<RectTransform> ();
		maxWidth = bar.rect.width;
		Increment(curPoop);
	}

	public void Increment(float val)
	{
		curPoop += val;
		curPoop = Mathf.Clamp(curPoop, 0f, maxPoop);
		curWidth = (curPoop/maxPoop)*maxWidth;
		bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,curWidth);
	}
}
