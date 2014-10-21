using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
	public string PlayerObjectName = "Wingman";
	private Player wingman;
	private Inventory inventory;
	public Sprite inventorySprite;
	public int SellValue = 0;
	public bool IsKeepableItem = false;

	void Start()
	{
		inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		wingman = GameObject.Find(PlayerObjectName).GetComponent<Player>();
		GetComponent<Interactable>().AdditionalInformation = "($" + SellValue + ")";
	}

	void IInteractable.InteractWith()
	{
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundAt("cashGrab", gameObject.transform.position);
		inventory.AddItem(gameObject.GetComponent<Interactable>().InteractableName, inventorySprite);
		if (wingman.numDetectors > 0)
		{
			wingman.increaseDetectionFlat(0.3f);
		}
		Destroy(gameObject);
	}
}
