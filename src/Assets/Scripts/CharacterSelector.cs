using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using InControl;

public class CharacterSelector : MonoBehaviour {

	public Image LeftArrow;
	public Image RightArrow;
	public Image PlayerImg;
	public Color DimColor;
	public Color HighlighColor;

	public Sprite[] textures;

	private bool isDone = false;
	private int texIndex = 0;
	private int playerIndex = -1;
	private InputDevice InputDev;
	private PlayerManager manager;


	public void DoEnable(InputDevice device, PlayerManager manager, int playerIndex)
	{
		List<Image> imgList = new List<Image>();
		imgList.AddRange(GetComponents<Image>());
		imgList.AddRange(GetComponentsInChildren<Image>());
		imgList.ForEach(x => x.color = HighlighColor);

		this.playerIndex = playerIndex;
		this.manager = manager;
		InputDev = device;
	}

	void Start()
	{
		DoSelect(false);
	}

	// Update is called once per frame
	void Update () {
	
		if(InputDev != null)
		{
			if(!isDone)
			{
				if(InputDev.DPadLeft.WasPressed || InputDev.LeftStick.Left.WasPressed)
				{
					texIndex = (texIndex >= textures.Length - 1 ? 0 : texIndex + 1);
					SwitchTexture();
				}
				else if(InputDev.DPadRight.WasPressed || InputDev.LeftStick.Right.WasPressed)
				{
					texIndex = (texIndex <= 0 ? textures.Length - 1 : texIndex - 1);
					SwitchTexture();
				}
			}

			if(InputDev.Action1.WasPressed && !manager.IsTextureUsed(PlayerImg.sprite))
				DoSelect(true);

			if(InputDev.Action2.WasPressed && isDone)
				DoDeselect();
		}
	}

	void SwitchTexture()
	{
		PlayerImg.sprite = textures[texIndex];

		if(manager.IsTextureUsed(PlayerImg.sprite))
			PlayerImg.color = DimColor;
		else
			PlayerImg.color = HighlighColor;
	}

	void DoSelect(bool done)
	{
		if(manager != null)
			manager.SetPlayerTexture(textures[texIndex], playerIndex);	

		List<Image> imgList = new List<Image>();
		imgList.AddRange(GetComponents<Image>());
		imgList.AddRange(GetComponentsInChildren<Image>());
		imgList.ForEach(x => x.color = DimColor);

		isDone = done;
	}

	void DoDeselect()
	{
		if(manager != null)
			manager.SetPlayerTexture(null, playerIndex);

		List<Image> imgList = new List<Image>();
		imgList.AddRange(GetComponents<Image>());
		imgList.AddRange(GetComponentsInChildren<Image>());
		imgList.ForEach(x => x.color = HighlighColor);
		
		isDone = false;
	}
}
