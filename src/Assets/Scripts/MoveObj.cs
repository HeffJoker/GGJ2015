/// <summary>
///	Title: Movement Controller
/// By:Matt Bridges
/// Purpose: Player input movement controller for Global Game Jam 2015
/// </summary>
using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveObj : MonoBehaviour {
	private Vector2 dir;							//Input values converted to vector2
	private GameObject thisObject;					//Object reference to parent object
	
	public float speed = 5;							//Movement speed value
	public bool moveHoriz = true;					//Move horizonal only?
	public bool moveVert = false;					//Move vertical only?
	public bool moveOmni = false;					//Move omnidirectional only?
	public bool moveDiag = false;					//Move diagonally only?
	public int controlIndex = 0;
	

	public Vector2 MoveDir
	{
		get { return dir; }
	}

	// Use this for initialization
	void Start () {
		thisObject = this.gameObject;				//Reference the current object
		if(controlIndex < 0)
			Debug.LogError(string.Format("Control index (val = {0}) cannot be less than 0!!", controlIndex));
		else if(controlIndex >= InputManager.Devices.Count)
			Debug.LogError(string.Format("Control index (val = {0}) exceeds number of controllers plugged in!", controlIndex));
	}
	
	// Update is called once per frame
	void Update () {

		if(controlIndex < 0 || controlIndex >= InputManager.Devices.Count)
			return;

		InputDevice input = InputManager.Devices[controlIndex];

		MovementHandler (input.LeftStickX, input.LeftStickY); //LeftStickX, input.LeftStickY);
		
		
	}
	void MovementHandler(float inX, float inY)
	{
		
		dir = new Vector2 (inX, inY);
		
		if(moveHoriz && moveVert)
		{
			if(Mathf.Abs(inX)>Mathf.Abs(inY))
				MoveHorizontal(dir);
			else
				MoveVertical(dir);
		}
		else
		{
			if (moveHoriz)
				MoveHorizontal (dir);
			if (moveVert)
				MoveVertical (dir);
		}
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
		
		//Check Joystick X position - Force to 1 for diagonal
		if(dir.x> 0)
			dirX = 1;
		if(dir.x< 0)
			dirX = -1;

		//Check Joystick Y position - Force to 1 for diagonal
		if(dir.y> 0)
			dirY = 1;
		if(dir.y< 0)
			dirY = -1;

		//Check Joystick Centered
		if(dir.y== 0 && dir.x == 0)
		{
			dirX = 0;
			dirY = 0;
		}
		else
		{
			if(dirY > 0 && dirX==0) //Default diagonal for Up is Up/Right
				dirX = 1;
			else if (dirY < 0 && dirX==0) //Default diagonals for Down is Down/Left
				dirX = -1;
			else if(dirX > 0 && dirY==0) //Default diagonals for Right is Right/Down
				dirY = -1;
			else if(dirX < 0 && dirY==0) //Default diagonals for Left is Left/Up
				dirY = 1;
		}
		
		//Move object 
		Vector2 move = new Vector2 (dirX, dirY);
		thisObject.rigidbody2D.velocity = move * speed;
	}

}
