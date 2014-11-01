using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryDisplayScript : MonoBehaviour
{

	public GameObject ItemDisplay;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void AddItem(string name, string objectName, int amount, Sprite image)
	{
		var id = (GameObject)Instantiate(ItemDisplay);
		id.name = name + "Display";
		id.GetComponent<InventoryButton>().objectName = objectName;
		id.transform.FindChild("ItemName").GetComponent<Text>().text = name;
		id.transform.FindChild("ItemAmount").GetComponent<Text>().text = "" + amount;
		id.transform.FindChild("ItemImage").GetComponent<Image>().sprite = image;
		id.transform.SetParent(GameObject.Find("ItemsDisplay").transform, false);

	}
	public void RemoveItem(string name)
	{
		var id = GameObject.Find("ItemsDisplay").transform.FindChild(name + "Display");
		Destroy(id);
	}
	public void UpdateAmount(string name, int amount)
	{
		var id = GameObject.Find("ItemsDisplay").transform.FindChild(name + "Display");
		id.transform.FindChild("ItemAmount").GetComponent<Text>().text = "" + amount;
	}
}
