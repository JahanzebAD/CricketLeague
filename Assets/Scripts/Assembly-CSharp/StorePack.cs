using UnityEngine;

[CreateAssetMenu(fileName = "New pack", menuName = "Store packs")]
public class StorePack : ScriptableObject
{
	public string ticketAmount;

	public string ticketPrice;

	public Sprite ticketSprite;
}
