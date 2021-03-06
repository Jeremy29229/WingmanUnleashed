﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
	public List<InventoryItem> items;
	public KeyCode InventoryPrintKey;
	public KeyCode InventoryDisplayKey;

	private bool inventoryVisible;
	private MouseManager mouseManager;
	private InteractionManager interactionManager;
	private ConversationManager conversationManger;
	private ObjectiveDisplayScript objectiveManager;

	private Camera_ThirdPerson cam;
	private Controller_ThirdPerson controller;
	private Scrollbar inventoryScrollbar;

	private Text noItemsText;

	void Start()
	{
		cam = Camera_ThirdPerson.Instance;
		controller = Controller_ThirdPerson.Instance;

		items = new List<InventoryItem>();
		inventoryVisible = false;
		GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = false;
		mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
		interactionManager = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
		objectiveManager = GameObject.Find("ObjectiveCanvas").GetComponent<ObjectiveDisplayScript>();
		conversationManger = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
		inventoryScrollbar = GameObject.Find("ItemsScroll").GetComponent<Scrollbar>();
		noItemsText = GameObject.Find("NoItemsText").GetComponent<Text>();
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
		if (Input.GetKeyDown(KeyCode.Q))
		{
			CloseInventory();
		}
	}

	public void DisplayInventory()
	{
		inventoryVisible = !inventoryVisible;
		if (inventoryVisible)
		{
			objectiveManager.Close();
			conversationManger.Close();
			interactionManager.Hide();
			GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = true;
			mouseManager.IsMouseLocked = false;
			cam.IsInConversation = true;
			controller.IsInConversation = true;
			inventoryScrollbar.value = 1.0f;
			Time.timeScale = 0.0f;
		}
		else
		{
			interactionManager.Show();
			GameObject.Find("InventoryCanvas").GetComponent<Canvas>().enabled = false;
			mouseManager.IsMouseLocked = true;
			cam.IsInConversation = false;
			controller.IsInConversation = false;
			Time.timeScale = 1.0f;
		}
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
		if (items.Count < 1)
		{
			noItemsText.enabled = false;
		}

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

		if (items.Count < 1)
		{
			noItemsText.enabled = false;
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

		if (items.Count < 1)
		{
			noItemsText.enabled = false;
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

			if (items.Count < 1)
			{
				noItemsText.enabled = true;
			}
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

			if (items.Count < 1)
			{
				noItemsText.enabled = true;
			}
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
