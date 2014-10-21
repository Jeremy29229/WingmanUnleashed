using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
	public string PlayerObjectName = "Wingman";
	private Inventory inventory;
	public Sprite inventorySprite;
	public int SellValue = 0;
	public bool IsKeepableItem = false;

	void Start()
	{
		inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		GetComponent<Interactable>().AdditionalInformation = "($" + SellValue + ")";
	}

	void IInteractable.InteractWith()
	{
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundAt("cashGrab", gameObject.transform.position);
		inventory.AddItem(gameObject.GetComponent<Interactable>().InteractableName, inventorySprite);
		Destroy(gameObject);
	}
}
