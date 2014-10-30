using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour, IInteractable
{
	public string PlayerObjectName = "Wingman";
	private Player wingman;
	private Inventory inventory;
	public Sprite inventorySprite;
	public int SellValue = 0;
	public bool IsKeepableItem = false;
	public bool IsImportantItem = false;
	bool lastImportantItemState = false;

	void Start()
	{
		GetComponentInChildren<Canvas>().enabled = IsImportantItem;
		lastImportantItemState = IsImportantItem;

		inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		wingman = GameObject.Find(PlayerObjectName).GetComponent<Player>();
		GetComponent<Interactable>().AdditionalInformation = "($" + SellValue + ")";
	}

	void Update()
	{
		if (IsImportantItem != lastImportantItemState)
		{
			GetComponentInChildren<Canvas>().enabled = IsImportantItem;
			lastImportantItemState = IsImportantItem;
		}
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
