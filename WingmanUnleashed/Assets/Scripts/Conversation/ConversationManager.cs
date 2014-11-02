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

	private Dialog[] response;
	private GameObject[] buttons;
	private Text[] textz;
	private Text npcText;
	private Text npcName;

	private Inventory inventory;
	private GameObject player;
	private Dialog last;

	void Start()
	{
		UI = gameObject.GetComponent<Canvas>();
		UI.enabled = false;
		cam = Camera_ThirdPerson.Instance;
		controller = Controller_ThirdPerson.Instance;

		player = GameObject.Find("Wingman");
		inventory = GameObject.Find("Wingman").GetComponent<Inventory>();

		response = new Dialog[4];

		buttons = new GameObject[4];
		buttons[0] = GameObject.Find("Dialog1Button");
		buttons[1] = GameObject.Find("Dialog2Button");
		buttons[2] = GameObject.Find("Dialog3Button");
		buttons[3] = GameObject.Find("Dialog4Button");

		textz = new Text[4];
		textz[0] = buttons[0].GetComponentInChildren<Text>();
		textz[1] = buttons[1].GetComponentInChildren<Text>();
		textz[2] = buttons[2].GetComponentInChildren<Text>();
		textz[3] = buttons[3].GetComponentInChildren<Text>();

		npcText = GameObject.Find("NPCText").GetComponent<Text>();
		npcName = GameObject.Find("NPCName").GetComponent<Text>();
	}

	public void ProcessDialog(Dialog d)
	{
		if (d != null)
		{
			last = d;
		}

		if (d == null)
		{
			UI.enabled = false;
			cam.IsInConversation = false;
			controller.IsInConversation = false;
		}
		else
		{
			UI.enabled = true;
			cam.IsInConversation = true;
			controller.IsInConversation = true;

			npcText.text = "\"" + d.npcText + "\"";
			npcName.text = d.npcName;

			for (int i = 0; i < buttons.Length; i++)
			{
				if (i >= d.responseText.Length)
				{
					buttons[i].SetActive(false);
				}
				else
				{
					buttons[i].SetActive(true);
					textz[i].text = d.responseText[i];
				}

				if (i < d.responseObject.Length)
				{
					response[i] = d.responseObject[i];
				}
				else
				{
					response[i] = null;
				}

				if (i < d.disguiseName.Length && d.disguiseName[i] != null && d.disguiseName[i] != "")
				{
					if (player.GetComponent<Outfit>().outfitName != d.disguiseName[i])
					{
						buttons[i].SetActive(false);
					}
				}

				if (i < d.requiredItemName.Length && d.requiredItemName[i] != null && d.requiredItemName[i] != "")
				{
					if (inventory.items.FirstOrDefault(x => x.Name == d.requiredItemName[i]) == null || (inventory.items.FirstOrDefault(x => x.Name == d.requiredItemName[i]) != null && inventory.items.FirstOrDefault(x => x.Name == d.requiredItemName[i]).Amount < d.requiredItemAmount[i]))
					{

						buttons[i].SetActive(false);
					}
				}
			}
		}
	}

	public void ProcessDialog(int choice)
	{

		if (last != null && choice < last.requiredItemName.Length && last.requiredItemName[choice] != null && last.requiredItemName[choice] != "")
		{

			inventory.items.Remove(inventory.items.FirstOrDefault(x => x.Name == last.requiredItemName[choice]));
		}

		ProcessDialog(response[choice]);
	}
}
