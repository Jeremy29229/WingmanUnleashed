using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConversationManager : MonoBehaviour
{
	private Canvas UI;
	private Camera_ThirdPerson cam;
	private Controller_ThirdPerson controller;

	private GameObject[] buttons;
	private Text[] responseTextDisplay;
	private Text npcText;
	private Text npcName;

	private Inventory inventory;
	private GameObject player;
	private Dialog last;
	private int optionCount = 0;
	private bool ignoreSelection = false;

	private Target targetScript;
	private Client clientScript;
	private Outfit outfit;

	private MouseManager mouseManager;

	void Start()
	{
		mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();

		UI = gameObject.GetComponent<Canvas>();
		UI.enabled = false;
		cam = Camera_ThirdPerson.Instance;
		controller = Controller_ThirdPerson.Instance;

		player = GameObject.Find("Wingman");
		inventory = player.GetComponent<Inventory>();

		buttons = new GameObject[4];
		buttons[0] = GameObject.Find("Dialog1Button");
		buttons[1] = GameObject.Find("Dialog2Button");
		buttons[2] = GameObject.Find("Dialog3Button");
		buttons[3] = GameObject.Find("Dialog4Button");

		responseTextDisplay = new Text[4];
		responseTextDisplay[0] = buttons[0].GetComponentInChildren<Text>();
		responseTextDisplay[1] = buttons[1].GetComponentInChildren<Text>();
		responseTextDisplay[2] = buttons[2].GetComponentInChildren<Text>();
		responseTextDisplay[3] = buttons[3].GetComponentInChildren<Text>();

		npcText = GameObject.Find("NPCText").GetComponent<Text>();
		npcName = GameObject.Find("NPCName").GetComponent<Text>();

		clientScript = GameObject.Find("Client").GetComponent<Client>();
		targetScript = GameObject.Find("Target").GetComponent<Target>();
		outfit = player.GetComponent<Outfit>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			UI.enabled = false;
			cam.IsInConversation = false;
			controller.IsInConversation = false;
			mouseManager.IsMouseLocked = true;
			if (last != null)
			{
				last.gameObject.GetComponent<Interactable>().IsActive = true;
			}
		}
	}

	public void ProcessDialog(Dialog d)
	{
		if (d == null)
		{
			UI.enabled = false;
			cam.IsInConversation = false;
			controller.IsInConversation = false;
			mouseManager.IsMouseLocked = true;
			if (last != null)
			{
				last.gameObject.GetComponent<Interactable>().IsActive = true;
			}
		}
		else
		{
			optionCount = 0;
			last = d;
			UI.enabled = true;
			cam.IsInConversation = true;
			controller.IsInConversation = true;
			ignoreSelection = false;

			for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i].SetActive(false);
			}

			for (int i = 0; i < d.Responses.Length && i < buttons.Length; i++)
			{
				if (d.Responses[i] != null && IsDialogVisiable(d.Responses[i]))
				{
					optionCount++;
					buttons[i].SetActive(true);
					buttons[i].GetComponentInChildren<Text>().text = d.Responses[i].Text;
					npcText.text = "\"" + d.NPCDialog + "\"";
					npcName.text = d.GetNPCName();
				}
			}

			if (optionCount == 0)
			{
				buttons[0].SetActive(true);
				buttons[0].GetComponentInChildren<Text>().text ="<Leave>";
				npcText.text = "\"" + d.NPCDialog + "\"";
				npcName.text = d.GetNPCName();
				ignoreSelection = true;
			}
		}
	}

	public void ProcessDialog(int choiceIndex)
	{
		//add item removal

		DialogResponse choice = null;
		Dialog next = null;

		if (!ignoreSelection && choiceIndex < last.Responses.Length)
		{
			choice = last.Responses[choiceIndex];
			if (!ignoreSelection)
			{
				choice.NumTimesSelected++;
			}
			next = choice.Resulting;

			if (choice != null && choice.NewCurrent != null)
			{
				choice.gameObject.GetComponent<Correspondence>().Current = choice.NewCurrent;
			}

			if (choice != null && choice.Items != null)
			{
				foreach (var item in choice.Items)
				{
					inventory.RemoveItem(item.ItemName, item.Amount);
				}
			}

			if(choice.AddedConfidence != 0.0f)
			{
				clientScript.increaseConfidence(choice.AddedConfidence);
			}

			if(choice.AddedInterested != 0.0f)
			{
				targetScript.increaseInterest(choice.AddedInterested);
			}
		}

		ProcessDialog(next);
	}

	private bool IsDialogVisiable(DialogResponse d)
	{
		return d != null
			&& HasRequiredItems(d) 
			&& HasRequiredObjectives(d)
			&& HasCorrectSelectionState(d)
			&& HasRequiredDisguise(d)
			&& HasRequiredInterest(d) 
			&& HadRequiredConfidence(d);
	}

	private bool HasRequiredItems(DialogResponse d)
	{
		bool hasItems = true;

		if (d.Items != null)
		{
			foreach (var itemRequirement in d.Items)
			{
				var item = inventory.items.FirstOrDefault(x => x.Name == itemRequirement.ItemName);

				if (item == null || item.Amount < itemRequirement.Amount)
				{
					hasItems = false;
				}
			}
		}

		return hasItems;
	}

	private bool HasRequiredObjectives(DialogResponse d)
	{
		return true;
	}

	private bool HasCorrectSelectionState(DialogResponse d)
	{
		return !d.IsOneTimeOption || d.NumTimesSelected == 0;
	}

	private bool HasRequiredDisguise(DialogResponse d)
	{
		bool hasDisguise = d.DisguiseNames.Length < 1;

		foreach (string disguiseName in d.DisguiseNames)
		{
			if (disguiseName == outfit.outfitName)
			{
				hasDisguise = true;
				break;
			}
		}

		return hasDisguise;
	}

	private bool HasRequiredInterest(DialogResponse d)
	{
		return d.RequiredInterested <= targetScript.GetInterest();
	}

	private bool HadRequiredConfidence(DialogResponse d)
	{
		return d.RequiredConfidence <= clientScript.GetConfidence();
	}

}
