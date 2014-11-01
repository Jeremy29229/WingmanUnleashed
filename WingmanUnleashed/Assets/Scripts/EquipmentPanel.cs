using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentPanel : MonoBehaviour
{

	Equipment item;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Use();
		}
	}

	public void Equip(string name, Sprite image)
	{
		gameObject.transform.FindChild("EquipImage").GetComponent<Image>().sprite = image;
		if (name == "RocketJump") item = new RocketJump();
		else item = new Throwable(name);
	}

	public void Unequip()
	{
		item = null;
	}

	public void Use()
	{
		if (item != null) item.Use();
	}
}
