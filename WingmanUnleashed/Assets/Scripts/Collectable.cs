using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour, IInteractable
{
	public string PlayerObjectName = "Wingman";
	private Player wingman;
	//private Inventory inventory;
	public Sprite inventorySprite;
	public int SellValue = 0;
	public bool IsKeepableItem = false;
	public bool IsImportantItem = false;
	private bool isItemImportanceDisplayed = false;
	private bool wasItemImportanceDisplayed = false;
	private Canvas itemImportanceDisplay;

	void Start()
	{
		//inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
		wingman = GameObject.Find(PlayerObjectName).GetComponent<Player>();
		itemImportanceDisplay = GetComponentInChildren<Canvas>();
		itemImportanceDisplay.enabled = false;
		GetComponentInChildren<Interactable>().AdditionalInformation = "($" + SellValue + ")";

	}

	void Update()
	{
		isItemImportanceDisplayed = (wingman.wingmanVisionActive && IsImportantItem);

		if (isItemImportanceDisplayed != wasItemImportanceDisplayed)
		{
			print("changing display state");
			wasItemImportanceDisplayed = isItemImportanceDisplayed;
			GetComponentInChildren<Canvas>().enabled = isItemImportanceDisplayed;

		}
	}

	void IInteractable.InteractWith()
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundAt("cashGrab", gameObject.transform.position);

        inventory.AddItem(gameObject.GetComponent<Interactable>().InteractableName, gameObject.name, inventorySprite);

		if (wingman.numDetectors > 0)
		{
			wingman.increaseDetectionFlat(0.3f);
		}
		Destroy(gameObject);
	}
}
