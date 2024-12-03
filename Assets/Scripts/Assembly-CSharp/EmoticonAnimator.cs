using System;
using UnityEngine;

public class EmoticonAnimator : MonoBehaviour
{
	public EmoticonHelper[] playerEmoticon;

	public Emoticons emoticonUI;

	private static EmoticonAnimator _Instance;

	private string[] currentAnimation = new string[5];

	private int currentPlayer = -1;

	private int duration = -1;

	public Texture alphaImage;

	public Texture[] A;

	public Texture[] B;

	public Texture[] C;

	public Texture[] D;

	public Texture[] E;

	public static EmoticonAnimator Instance => _Instance;

	private void Awake()
	{
		if (_Instance == null)
		{
			_Instance = this;
		}
	}

	public void A_ClickEvent()
	{
		ButtonEvent("A");
	}

	public void B_ClickEvent()
	{
		ButtonEvent("B");
	}

	public void C_ClickEvent()
	{
		ButtonEvent("C");
	}

	public void D_ClickEvent()
	{
		ButtonEvent("D");
	}

	public void E_ClickEvent()
	{
		ButtonEvent("E");
	}

	public void ButtonEvent(string buttonName)
	{
		emoticonUI.CloseEmoticons();
		currentPlayer = Array.FindIndex(Multiplayer.playerList, (PlayerList t) => t.PlayerId == CONTROLLER.userID);
		switch (buttonName)
		{
		case "A":
			duration = 10;
			break;
		case "B":
			duration = 10;
			break;
		case "C":
			duration = 10;
			break;
		case "D":
			duration = 10;
			break;
		case "E":
			duration = 25;
			break;
		}
		currentAnimation[currentPlayer] = buttonName;
		AnimateEmoticon(CONTROLLER.userID, currentAnimation[currentPlayer], duration);
		ServerManager.Instance.SendEmoticon(buttonName);
	}

	public void AnimateEmoticon(int player, string emoticon, int duration)
	{
		switch (emoticon)
		{
		case "A":
			duration = 10;
			break;
		case "B":
			duration = 10;
			break;
		case "C":
			duration = 10;
			break;
		case "D":
			duration = 10;
			break;
		case "E":
			duration = 25;
			break;
		}
		player = Array.FindIndex(Multiplayer.playerList, (PlayerList t) => t.PlayerId == player);
		playerEmoticon[player].PlayAnimation(emoticon, duration);
	}

	public Texture getTexture(string name, int index)
	{
		Texture result = alphaImage;
		switch (name)
		{
		case "A":
			result = A[index];
			break;
		case "B":
			result = B[index];
			break;
		case "C":
			result = C[index];
			break;
		case "D":
			result = D[index];
			break;
		case "E":
			result = E[index];
			break;
		}
		return result;
	}
}
