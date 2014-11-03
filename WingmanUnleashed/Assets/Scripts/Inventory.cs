using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
	public List<InventoryItem> items;
	public KeyCode InventoryPrintKey;
	public KeyCode InventoryDisplayKey;

	private bool inventoryVisible;

	void Start()
	{
		items = new List<InventoryItem>();
		inventoryVisible = false;
		GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(InventoryPrintKey))
		{
			Debug.Log(ToString());
		}
		if (Input.GetKeyDown(InventoryDisplayKey))
		{
			DisplayInventory();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CloseInventory();
		}
	}

	public void DisplayInventory()
	{
		inventoryVisible = !inventoryVisible;
		GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = inventoryVisible;
	}

	public void CloseInventory()
	{
		if (inventoryVisible)
		{
			DisplayInventory();
		}
	}

	public void AddItem(string name, GameObject gobject, Sprite inventoryImage, int amount = 1)
	{
		var potentialItem = items.FirstOrDefault(x => x.Name == name);
		if (potentialItem == null)
		{
			gobject.transform.position = new Vector3(0, 0, 0);
			items.Add(new InventoryItem(name, gobject, amount));
			GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().AddItem(name, gobject, amount, inventoryImage);
		}
		else
		{
			Destroy(gobject);
			potentialItem.Amount += amount;
			GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().UpdateAmount(name, potentialItem.Amount);
		}
	}

	public void AddItem(InventoryItem i)
	{
		var potentialItem = items.FirstOrDefault(x => x.Name == i.Name);
		if (potentialItem == null)
		{
			items.Add(i);
		}
		else
		{
			potentialItem.Amount += i.Amount;
		}
	}

	public bool RemoveItem(GameObject gobject, int amount = 1)
	{
		bool result = false;
		InventoryItem item = items.First<InventoryItem>(i => i.Gob == gobject);
		item.Amount -= amount;
		GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().UpdateAmount(item.Name, item.Amount);
		
		if(item.Amount<=0)
		{
			result = true;
			GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().RemoveItem(item.Name);
			Destroy(item.Gob);
			items.Remove(item);
		}
		return result;
	}

	public bool RemoveItem(string Name, int amount = 1)
	{
		bool result = false;
		InventoryItem item = items.First<InventoryItem>(i => i.Name == Name);
		item.Amount -= amount;
		GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().UpdateAmount(item.Name, item.Amount);

		if (item.Amount <= 0)
		{
			result = true;
			GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().RemoveItem(item.Name);
			Destroy(item.Gob);
			items.Remove(item);
		}
		return result;
	}

	public override string ToString()
	{
		string stuff = "";
		foreach (var i in items)
		{
			stuff += i.ToString() + ", ";
		}

		if (stuff == "")
		{
			stuff = "Inventory is empty";
		}
		else
		{
			stuff = stuff.Substring(0, stuff.Length - 2);
		}

		return stuff;
	}
}
