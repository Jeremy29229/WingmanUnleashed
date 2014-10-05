using UnityEngine;
using System.Collections;

public class InventoryItem
{
	public string Name;
	public int Amount;

	public InventoryItem(string name = "unknown", int amount = 1)
	{
		Name = name;
		Amount = amount;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override string ToString()
	{
		return Name + ": " + Amount;
	}
}
