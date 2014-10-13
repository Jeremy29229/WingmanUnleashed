using UnityEngine;
using System.Collections;

public class Disguise : MonoBehaviour , IInteractable
{
	//public string PlayerObjectName = "Wingman";
	//private Outfit outfit;
	
	void Start()
	{
		//outfit = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
	}
	
	void IInteractable.InteractWith()
	{
		//outfit.AddItem(gameObject.GetComponent<Interactable>().InteractableName);
		//Destroy(gameObject);
	}
}