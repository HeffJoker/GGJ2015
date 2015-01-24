using UnityEngine;
using System.Collections;

public class PlayerObject : MonoBehaviour {

	public Transform MountPos;

	private MoveObj mover;

	// Use this for initialization
	void Start () {
		mover = GetComponent<MoveObj>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 dir = mover.MoveDir.normalized;
		float dirVal = transform.localScale.x;

		if(dir.x > 0)
			dirVal = Mathf.Abs(dirVal); //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		else
		{
			if(dirVal > 0)
				dirVal = -dirVal;
		}

		transform.localScale = new Vector3(dirVal, transform.localScale.y, transform.localScale.z); //.x = dir.x;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("GoalObj"))
		{
			if(MountPos == null)
			{
				collider.transform.position = transform.position;
				collider.transform.parent = transform;
			}
			else
			{
				collider.transform.position = MountPos.position;
				collider.transform.parent = MountPos;
			}
		}
	}
}
