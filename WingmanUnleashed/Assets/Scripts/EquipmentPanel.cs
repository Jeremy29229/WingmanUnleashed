using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentPanel : MonoBehaviour
{
    private Inventory inventory;
	GameObject item;
	// Use this for initialization
	void Start()
	{
        inventory = GameObject.Find("Wingman").GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Use();
		}
	}

	public void Equip(GameObject gobject, Sprite image)
	{
		gameObject.transform.FindChild("EquipImage").GetComponent<Image>().sprite = image;
        item = gobject;

	}

	public void Unequip()
	{
        gameObject.transform.FindChild("EquipImage").GetComponent<Image>().sprite = GameObject.Find("ItemDisplay").transform.FindChild("ItemImage").GetComponent<Image>().sprite;
        Destroy(item);
	}

	public void Use()
	{
        if (item != null && item.GetComponent<Throwable>() != null)
        {
            item.GetComponent<Throwable>().Use();
            if (inventory.RemoveItem(item))
            {
                Unequip();
            }
        }
	}
}
