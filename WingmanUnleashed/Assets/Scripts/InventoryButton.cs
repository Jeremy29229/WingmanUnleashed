using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
	public GameObject gobject;
	public EquipmentPanel ep;
	// Use this for initialization
	void Start()
	{
		ep = GameObject.Find("EquipmentPanel").GetComponent<EquipmentPanel>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void equip()
	{
		Sprite image = gameObject.transform.FindChild("ItemImage").GetComponent<Image>().sprite;
		ep.Equip(gobject, image);
	}

	void OnDestroy()
	{
		if (gobject == ep.item)
		{
			ep.Unequip();
		}
	}
}
