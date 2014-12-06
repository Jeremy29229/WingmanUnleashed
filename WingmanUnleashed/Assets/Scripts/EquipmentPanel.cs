using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentPanel : MonoBehaviour
{
	private Inventory inventory;
	public GameObject item;
	GameObject itemsDisplay;

	void Start()
	{
		inventory = GameObject.Find("Wingman").GetComponent<Inventory>();
		itemsDisplay = GameObject.Find("ItemsDisplay");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Use();
		}

		if (gameObject.transform.FindChild("EquipImage").GetComponent<Image>().sprite == null && itemsDisplay.transform.childCount > 0)
		{
			itemsDisplay.transform.GetChild(0).GetComponent<InventoryButton>().equip();
		}

		//if (itemsDisplay.transform.childCount == 0 || item != null && item.GetComponent<Throwable>() == null)
		//{
		//	Unequip();
		//}
	}

	public void Equip(GameObject gobject, Sprite image)
	{
		gameObject.transform.FindChild("EquipImage").GetComponent<Image>().sprite = image;
		item = gobject;

	}

	public void Unequip()
	{
		gameObject.transform.FindChild("EquipImage").GetComponent<Image>().sprite = null;
	}

	public void Use()
	{
		if (item != null && item.GetComponent<Throwable>() != null)
		{
			item.GetComponent<Throwable>().Use();
			if (inventory.RemoveItem(item))
			{
				Unequip();
			}
		}
	}
}
