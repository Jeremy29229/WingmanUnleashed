using UnityEngine;

public class InventoryItem
{
	public string Name;
    public string ObjectName;
	public int Amount;

	public InventoryItem(string name = "unknown", string objectName = "unknown", int amount = 1)
	{
		Name = name;
        ObjectName = objectName;
		Amount = amount;
	}

	public override string ToString()
	{
		return Name + ": " + Amount;
	}
}
