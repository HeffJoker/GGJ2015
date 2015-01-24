using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour {

	public float MoveSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 direction = Vector2.zero;

		if(Input.GetKeyDown(KeyCode.A))
			direction = -Vector3.right;
		else if(Input.GetKeyDown(KeyCode.D))
			direction = Vector3.right;

		if(Input.GetKeyDown(KeyCode.S))
			direction = -Vector3.up;
		else if(Input.GetKeyDown(KeyCode.W))
			direction = Vector3.up;

		rigidbody2D.AddForce(direction * MoveSpeed);
	}
}
