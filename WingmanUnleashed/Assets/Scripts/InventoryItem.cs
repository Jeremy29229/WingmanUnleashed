using UnityEngine;

public class InventoryItem
{
	public string Name;
	public int Amount;

	public InventoryItem(string name = "unknown", int amount = 1)
	{
		Name = name;
		Amount = amount;
	}

	public override string ToString()
	{
		return Name + ": " + Amount;
	}
}
