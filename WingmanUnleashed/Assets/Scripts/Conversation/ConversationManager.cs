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
	private Commandable commandableClient;
	private Outfit outfit;

	private MouseManager mouseManager;
	private ObjectiveDisplayScript objectiveManager;

	void Start()
	{
		objectiveManager = GameObject.Find("ObjectiveCanvas").GetComponent<ObjectiveDisplayScript>();
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
		if (d != null && d.gameObject.GetComponent<Commandable>() != null)
		{
			commandableClient = d.gameObject.GetComponent<Commandable>();
		}
        
		if (GameObject.Find("Client") != null)
		{
			clientScript = GameObject.Find("Client").GetComponentInChildren<Client>();
		}
		else
		{
			clientScript = null;
		}

		if (GameObject.Find("Target"))
		{
			targetScript = GameObject.Find("Target").GetComponentInChildren<Target>();
		}
		else
		{
			targetScript = null;
		}
		
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
					if (item.takeItems)
					{
						inventory.RemoveItem(item.ItemName, item.Amount);
					}
				}
			}

			if (clientScript != null && choice.AddedConfidence != 0.0f)
			{
				if (choice.AddedConfidence > 0.0f)
				{
					clientScript.increaseConfidence(choice.AddedConfidence);
				}
				else
				{
					clientScript.decreaseConfidence(-choice.AddedConfidence);
				}
			}

			if(targetScript != null && choice.AddedInterested != 0.0f)
			{
				if (choice.AddedInterested > 0.0f)
				{
					targetScript.increaseInterest(choice.AddedInterested);
				}
				else
				{
					targetScript.decreaseInterest(-choice.AddedInterested);
				}
			}

			if (choice.AddedSuspicion != 0.0f)
			{
				player.AddComponent<Player>().increaseDetectionFlat(choice.AddedSuspicion);
			}

			if (choice.RequiredObjectives.Length != 0)
			{
				foreach (var objective in choice.RequiredObjectives)
				{
					if (objective.AddOnSelection)
					{
						objectiveManager.AddObjective(objective.ObjectiveName, objective.AddOnText);
					}

					if (objective.CompleteOnSelection)
					{
						var objectObject = GameObject.Find(objective.ObjectiveName);
						if (objectObject == null)
						{
							objectiveManager.AddObjective(objective.ObjectiveName, objective.AddOnText);
							objectObject = GameObject.Find(objective.ObjectiveName);
						}

						objectObject.GetComponent<ObjectiveInfo>().SetCompleted();

						objectObject.GetComponentInChildren<Text>().text = "(Completed) " + objectObject.GetComponentInChildren<Text>().text;
					}
				}
			}

			if (commandableClient != null && choice.visitAfterward)
			{
				commandableClient.visitLocation(choice.destinationGameObject.transform.position);
			}

            if (commandableClient != null && choice.followAfterward)
            {
                commandableClient.followCharacter(choice.destinationGameObject);
            }

            if (commandableClient != null && choice.sentToAfterward)
            {
                commandableClient.sendToLocation(choice.destinationGameObject.transform.position);
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
		bool hasObjectivesCompleted = true;

		foreach (var objective in d.RequiredObjectives)
		{
			var objectiveObject = GameObject.Find(objective.ObjectiveName);
			switch (objective.RequiredObjectiveState)
			{
				case ObjectState.DoesNotExist:
					if (objectiveObject != null)
					{
						hasObjectivesCompleted = false;
					}
					break;
				case ObjectState.Started:
					if (objectiveObject == null || objectiveObject.GetComponent<ObjectiveInfo>().IsComplete())
					{
						hasObjectivesCompleted = false;
					}
					break;
				case ObjectState.Completed:
					if (objectiveObject == null || !objectiveObject.GetComponent<ObjectiveInfo>().IsComplete())
					{
						hasObjectivesCompleted = false;
					}
					break;
			}
		}

		return hasObjectivesCompleted;
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
		return targetScript == null || d.RequiredInterested <= targetScript.GetInterest();
	}

	private bool HadRequiredConfidence(DialogResponse d)
	{
		return clientScript == null || d.RequiredConfidence <= clientScript.GetConfidence();
	}

}
