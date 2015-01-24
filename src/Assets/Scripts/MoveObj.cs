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
	private float totalTime = 0;
	private Vector3 lastPos = Vector3.zero;

	public float speed = 5;							//Movement speed value
	public float curveSpeed = 5;					//Movement speed ONLY for wiggle.
	public float amplitude = 2;
	public bool moveHoriz = true;					//Move horizonal only?
	public bool moveVert = false;					//Move vertical only?
	public bool moveOmni = false;					//Move omnidirectional only?
	public bool moveDiag = false;					//Move diagonally only?
	public bool moveWiggle = false;					//Wiggle only?
	public int controlIndex = 0;
	public float pauseBetweenSteps = 0.2f;
	public bool isMoving = false;

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

		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(controlIndex < 0 || controlIndex >= InputManager.Devices.Count)
			return;

		InputDevice input = InputManager.Devices[controlIndex];

		MovementHandler (input.LeftStickX, input.LeftStickY); //LeftStickX, input.LeftStickY);
	}
	void MovementHandler(float inX, float inY)
	{
		//Check if player is already moving or not moving at all
		if(isMoving || (inX==0 && inY==0))
			return;

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

		if(moveWiggle)
			MoveWiggle(dir);

		//Re-enable movement after a step pause
		if(isMoving)
			StartCoroutine(ReenableMovement(pauseBetweenSteps));
	}
	IEnumerator ReenableMovement(float wait) {
		yield return new WaitForSeconds(wait);		
		isMoving = false;
	}
	void MoveHorizontal(Vector2 dir)
	{
		//Move object 
		Vector2 move = new Vector2 (dir.x, 0);
		thisObject.rigidbody2D.velocity = move * speed;
		isMoving = true;
	}
	void MoveVertical(Vector2 dir)
	{
		//Move object 
		Vector2 move = new Vector2 (0, dir.y);
		thisObject.rigidbody2D.velocity = move * speed;
		isMoving = true;
	}
	void MoveOmni(Vector2 dir)
	{
		//Move object 
		thisObject.rigidbody2D.velocity = dir * speed;
		isMoving = true;
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

		//Check for Up/Down/Left/Right
		if(dirY > 0 && dirX==0) 		//Default diagonal for Up is Up/Right
			dirX = 1;
		else if (dirY < 0 && dirX==0) 	//Default diagonals for Down is Down/Left
			dirX = -1;
		else if(dirX > 0 && dirY==0) 	//Default diagonals for Right is Right/Down
			dirY = -1;
		else if(dirX < 0 && dirY==0) 	//Default diagonals for Left is Left/Up
			dirY = 1;
		
		//Move object 
		Vector2 move = new Vector2 (dirX, dirY);
		thisObject.rigidbody2D.velocity = move * speed;
		isMoving = true;
	}

	void MoveWiggle(Vector2 dir)
	{
		lastPos = transform.position;
		totalTime += Time.deltaTime;// * curveSpeed;

		Vector3 vSin = new Vector3(amplitude * Mathf.Sin(totalTime * curveSpeed), amplitude * -Mathf.Sin(totalTime * curveSpeed), 0);
		Vector3 vLin = new Vector3(dir.x * speed, dir.y * speed, 0);
		 
		transform.position += (vSin + vLin) * Time.deltaTime;
	}

}
