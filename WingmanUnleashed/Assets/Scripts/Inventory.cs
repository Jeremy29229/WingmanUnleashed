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
	}

    public void DisplayInventory()
    {
        inventoryVisible = !inventoryVisible;
        GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = inventoryVisible;
    }

	public void AddItem(string name, Sprite inventoryImage, int amount = 1)
	{
		var potentialItem = items.FirstOrDefault(x => x.Name == name);
		if (potentialItem == null)
		{
			items.Add(new InventoryItem(name, amount));
            GameObject.Find("InventoryDisplay").GetComponent<InventoryDisplayScript>().AddItem(name, amount, inventoryImage);
		}
		else
		{
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
