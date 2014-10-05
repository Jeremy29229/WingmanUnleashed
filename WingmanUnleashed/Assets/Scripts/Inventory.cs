using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
	public List<InventoryItem> items;
	public KeyCode InventoryPrintKey;

	// Use this for initialization
	void Start()
	{
		items = new List<InventoryItem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(InventoryPrintKey))
		{
			Debug.Log(ToString());
		}

	}

	public void AddItem(string name, int amount = 1)
	{
		var potentialItem = items.FirstOrDefault(x => x.Name == name);
		if (potentialItem == null)
		{
			items.Add(new InventoryItem(name, amount));
		}
		else
		{
			potentialItem.Amount += amount;
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
