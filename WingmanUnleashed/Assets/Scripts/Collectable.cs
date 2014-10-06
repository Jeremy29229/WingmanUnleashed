using UnityEngine;
using System.Collections;

/// <summary>
/// Behavior that work with 
/// </summary>
public class Collectable : MonoBehaviour, IInteractable
{
	public string PlayerObjectName = "CharacterBasic";
	private Inventory inventory;

	// Use this for initialization
	void Start()
	{
		inventory = GameObject.Find(PlayerObjectName).GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void IInteractable.InteractWith()
	{
		inventory.AddItem(gameObject.GetComponent<Interactable>().InteractableName);
		Destroy(gameObject);
	}
}
