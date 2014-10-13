using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConversationManager : MonoBehaviour
{
	private Canvas UI;
	private Camera_ThirdPerson camera;
	private Controller_ThirdPerson controller;

	private Dialog[] response;
	private GameObject[] buttons;
	private Text[] textz;
	private Text npcText;
	private Text npcName;

	void Start()
	{
		UI = gameObject.GetComponent<Canvas>();
		UI.enabled = false;
		camera = Camera_ThirdPerson.Instance;
		controller = Controller_ThirdPerson.Instance;

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
		if (d == null)
		{
			UI.enabled = false;
			camera.IsInConversation = false;
			controller.IsInConversation = false;
		}
		else
		{
			UI.enabled = true;
			camera.IsInConversation = true;
			controller.IsInConversation = true;
			
			npcText.text = d.npcText;
			npcName.text = d.npcName;

			for (int i = 0; i < buttons.Length; i++ )
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
			}
		}
	}

	public void ProcessDialog(int choice)
	{
		ProcessDialog(response[choice]);
	}
}
