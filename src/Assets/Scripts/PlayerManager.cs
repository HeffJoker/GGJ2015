using UnityEngine;
using System.Collections.Generic;
using InControl;

public class PlayerManager : MonoBehaviour {

	public PlayerObject playerPrefab;
	public CharacterSelector[] selectors;
	
	private List<InputDevice> devices = new List<InputDevice>();
	private List<PlayerObject> players = new List<PlayerObject>();


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
				CreatePlayer();
			}
		}	
	}

	public void CreatePlayer()
	{
		PlayerObject newPlayer = Instantiate(playerPrefab) as PlayerObject;
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
}
