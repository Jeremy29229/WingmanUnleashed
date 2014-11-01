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

	private GameObject gameover;

	private GameObject wingman;

	bool finishedC1 = false;
	bool finishedC2 = false;

	bool addedConfidence1 = false;
	bool addedConfidence2 = false;

	bool addedInt1 = false;
	bool addedInt2 = false;

	//bool finishedQuest = false;

	bool added = false;

	void Start()
	{
		client = GameObject.Find("Client");
		client.GetComponent<Conversation>().start = clientCons[0].start;
		target = GameObject.Find("Target");
		target.GetComponent<Conversation>().start = targetCons[0].start;
		inventory = GameObject.Find("Wingman").GetComponent<Inventory>();
		wingman = GameObject.Find("Wingman");
		gameover = GameObject.Find("GameOverScreen");
	}

	void Update()
	{
		if (!added)
		{
			inventory.AddItem(new InventoryItem("client1"));
			inventory.AddItem(new InventoryItem("client2"));
			inventory.AddItem(new InventoryItem("con1"));
			inventory.AddItem(new InventoryItem("con2"));
			inventory.AddItem(new InventoryItem("int1"));
			inventory.AddItem(new InventoryItem("int2"));
			added = true;
		}

		if (!finishedC1)
		{
			if (inventory.items.FirstOrDefault(x => x.Name == "client1") == null)
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

		if (!addedConfidence1)
		{
			if (inventory.items.FirstOrDefault(x => x.Name == "con1") == null)
			{
				addedConfidence1 = true;
				client.GetComponent<Client>().increaseConfidence(.5f);
			}
		}

		if (!addedConfidence2)
		{
			if (inventory.items.FirstOrDefault(x => x.Name == "con2") == null)
			{
				addedConfidence2 = true;
				client.GetComponent<Client>().increaseConfidence(.5f);
			}
		}

		if (!addedInt1)
		{
			if (inventory.items.FirstOrDefault(x => x.Name == "int1") == null)
			{
				addedInt1 = true;
				target.GetComponent<Target>().increaseInterest(.5f);
			}
		}

		if (!addedInt2)
		{
			if (inventory.items.FirstOrDefault(x => x.Name == "int2") == null)
			{
				addedInt2 = true;
				target.GetComponent<Target>().increaseInterest(.5f);
			}
		}

		if (addedInt1 && addedInt2 && addedConfidence1 && addedConfidence2)
		{
			//			gameover.GetComponent<GameOverScript>().ShowGameOverWin();
		}

		if (wingman.GetComponent<Player>().getDetectionLevel() >= 1.0f)
		{
			gameover.GetComponent<GameOverScript>().ShowGameOVerLose();
		}
	}
}
