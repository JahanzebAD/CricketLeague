using UnityEngine;
using UnityEngine.UI;

public class HeaderButtonSpriteChangeTWO : MonoBehaviour
{
	public Image HelpImage;

	public Sprite HelpHoverSprite;

	public Sprite HelpNormalSprite;

	public Image SettingsImage;

	public Sprite SettingsHoverSprite;

	public Sprite SettingsNoramlSprite;

	public Image AboutImage;

	public Sprite AboutHoverSprite;

	public Sprite AboutNormalSprite;

	public void HomeButton()
	{
		SettingsImage.sprite = SettingsNoramlSprite;
		HelpImage.sprite = HelpNormalSprite;
		AboutImage.sprite = AboutNormalSprite;
	}

	public void SettingsButton()
	{
		SettingsImage.sprite = SettingsHoverSprite;
		HelpImage.sprite = HelpNormalSprite;
		AboutImage.sprite = AboutNormalSprite;
	}

	public void AboutButton()
	{
		AboutImage.sprite = AboutHoverSprite;
		SettingsImage.sprite = SettingsNoramlSprite;
		HelpImage.sprite = HelpNormalSprite;
	}

	public void HelpButton()
	{
		HelpImage.sprite = HelpHoverSprite;
		SettingsImage.sprite = SettingsNoramlSprite;
		AboutImage.sprite = AboutNormalSprite;
	}

	public void BackButton()
	{
		SettingsImage.sprite = SettingsNoramlSprite;
		HelpImage.sprite = HelpNormalSprite;
		AboutImage.sprite = AboutNormalSprite;
	}
}
