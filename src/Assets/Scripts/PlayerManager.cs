using UnityEngine;
using System.Collections.Generic;
using InControl;

public class PlayerManager : MonoBehaviour {

	public PlayerObject playerPrefab;
	public CharacterSelector[] selectors;
	public float TransitionTime = 5f;
	
	private List<InputDevice> devices = new List<InputDevice>();
	private List<PlayerObject> players = new List<PlayerObject>();
	private bool isTransitioning = false;


	public List<PlayerObject> Players
	{
		get { return players; }
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
		HandleCharSelect();
	}

	void HandleCharSelect()
	{
		InputDevice currDevice = InputManager.ActiveDevice;

		if(currDevice.MenuWasPressed)
		{
			if(!devices.Contains(currDevice))
			{
				int index = devices.Count;
				devices.Add(currDevice);
				selectors[index].DoEnable(currDevice, this, index);
				CreatePlayer(currDevice);
			}
		}	
	}

	public void CreatePlayer(InputDevice inDevice)
	{
		PlayerObject newPlayer = Instantiate(playerPrefab) as PlayerObject;
		newPlayer.transform.parent = transform;
		newPlayer.InDevice = inDevice;
		newPlayer.gameObject.SetActive(false);
		players.Add(newPlayer);
	}

	public void SetPlayerTexture(Sprite texture, int index)
	{
		SpriteRenderer sprite = players[index].GetComponent<SpriteRenderer>();
		sprite.sprite = texture;
	}

	public bool IsTextureUsed(Sprite texture)
	{
		SpriteRenderer sprite;
		foreach(PlayerObject player in players)
		{
			sprite = player.GetComponent<SpriteRenderer>();
			if(sprite.sprite == texture)
				return true;
		}

		return false;
	}

	public void StartCountDown()
	{	
		if(!isTransitioning && players.Count >= 2)
		{
			Invoke("MoveToMainScene", TransitionTime);
			isTransitioning = true;
		}
	}

	public void StopCountDown()
	{
		CancelInvoke("MoveToMainScene");
		isTransitioning = false;
	}

	private void MoveToMainScene()
	{
		Application.LoadLevel("main_scene");
	}
}
