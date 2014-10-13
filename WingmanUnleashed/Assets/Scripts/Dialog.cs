﻿using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour
{
	void Start()
	{
		npcName = gameObject.GetComponent<Interactable>().InteractableName;
	}

	public string npcName;

	public string npcText;

	public string[] responseText;

	public Dialog[] responseObject;

	public string[] requiredItemName;

	public int[] requiredItemAmount;

	public string[] disguiseName;

	//public InventoryItem t;
}
