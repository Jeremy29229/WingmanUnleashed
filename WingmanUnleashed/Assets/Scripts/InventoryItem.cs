using UnityEngine;

public class InventoryItem
{
	public string Name;
    public GameObject Gob;
	public int Amount;

	public InventoryItem(string name = "unknown", GameObject gobject = null, int amount = 1)
	{
		Name = name;
		Gob = gobject;
		Amount = amount;
	}

	public override string ToString()
	{
		return Name + ": " + Amount;
	}
}
