using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour, IIteractable
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

	void IIteractable.InteractWith()
	{
		Player.GetComponent<Inventory>().AddItem(gameObject.GetComponent<Interactable>().InteractableName);
		Destroy(gameObject);
	}
}
