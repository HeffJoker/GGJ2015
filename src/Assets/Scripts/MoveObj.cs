/// <summary>
///	Title: Movement Controller
/// By:Matt Bridges
/// Purpose: Player input movement controller for Global Game Jam 2015
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObj : MonoBehaviour {
	private Vector2 dir;							//Input values converted to vector2
	private GameObject thisObject;					//Object reference to parent object
	
	public float speed = 5;							//Movement speed value
	public bool moveHoriz = true;					//Move horizonal only?
	public bool moveVert = false;					//Move vertical only?
	public bool moveOmni = false;					//Move omnidirectional only?
	public bool moveDiag = false;					//Move diagonally only?
	
	
	
	// Use this for initialization
	void Start () {
		thisObject = this.gameObject;				//Reference the current object
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		MovementHandler (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		
		
	}
	void MovementHandler(float inX, float inY)
	{
		
		dir = new Vector2 (inX, inY);
		
		if (moveHoriz)
			MoveHorizontal (dir);
		if (moveVert)
			MoveVertical (dir);
		if (moveOmni)
			MoveOmni (dir);
		if (moveDiag)
			MoveDiag (dir);
	}
	void MoveHorizontal(Vector2 dir)
	{
		//Move object 
		Vector2 move = new Vector2 (dir.x, 0);
		thisObject.rigidbody2D.velocity = move * speed;
	}
	void MoveVertical(Vector2 dir)
	{
		//Move object 
		Vector2 move = new Vector2 (0, dir.y);
		thisObject.rigidbody2D.velocity = move * speed;
	}
	void MoveOmni(Vector2 dir)
	{
		//Move object 
		thisObject.rigidbody2D.velocity = dir * speed;
	}
	void MoveDiag(Vector2 dir)
	{
		int dirX = 0;
		int dirY = 0;
		
		//Check Joystick X position
		if(dir.x> 0)
			dirX = 1;
		if(dir.x< 0)
			dirX = -1;
		//Check Joystick Y position
		if(dir.y> 0)
			dirY = 1;
		if(dir.y< 0)
			dirY = -1;
		//Check Joystick Centered
		if(dir.y== 0 && dir.x == 0 || dir.x==0 || dir.y==0)
		{
			dirX = 0;
			dirY = 0;
		}
		
		//Move object 
		Vector2 move = new Vector2 (dirX, dirY);
		thisObject.rigidbody2D.velocity = move * speed;
	}

}
