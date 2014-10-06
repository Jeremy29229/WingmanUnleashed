using UnityEngine;
using System.Collections;

/// <summary>
/// Behavior that work with 
/// </summary>
public class Collectable : MonoBehaviour, IInteractable
{
	public GameObject Player;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void IInteractable.InteractWith()
	{
		Player.GetComponent<Inventory>().AddItem(gameObject.GetComponent<Interactable>().InteractableName);
		Destroy(gameObject);
	}
}
