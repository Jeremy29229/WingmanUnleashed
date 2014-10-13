using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class JobManager : MonoBehaviour
{
	private GameObject client;
	public Conversation[] clientCons;

	private GameObject target;
	public Conversation[] targetCons;

	private Inventory inventory;

	bool finishedC1 = false;
	bool finishedC2 = false;

	bool added = false;

	void Start()
	{
		client = GameObject.Find("Client");
		client.GetComponent<Conversation>().start = clientCons[0].start;
		target = GameObject.Find("Target");
		target.GetComponent<Conversation>().start = targetCons[0].start;
		inventory = GameObject.Find("Wingman").GetComponent<Inventory>();
	}

	void Update()
	{
		if (!added)
		{
			inventory.AddItem(new InventoryItem("client1"));
			inventory.AddItem(new InventoryItem("client2"));
			added = true;
		}

		if (!finishedC1)
		{
			if(inventory.items.FirstOrDefault(x => x.Name == "client1") == null)
			{
				finishedC1 = true;
				client.GetComponent<Conversation>().start = clientCons[1].start;
				target.GetComponent<Conversation>().start = targetCons[1].start;
			}
		}

		if (!finishedC2)
		{
			if (inventory.items.FirstOrDefault(x => x.Name == "client2") == null)
			{
				finishedC2 = true;
				client.GetComponent<Conversation>().start = clientCons[2].start;
			}
		}
	}
}
