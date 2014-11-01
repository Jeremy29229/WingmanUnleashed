using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversable : MonoBehaviour, IInteractable
{
	private Conversation conversation;
	private ConversationManager cm;
	private Player Wingman;
	private bool isChatBubbleDisplayed = false;
	private bool wasChatBubbleDisplayed = false;
	private Canvas chatBubbleDisplay;

	void Start()
	{
		Wingman = GameObject.Find("Wingman").GetComponent<Player>();
		conversation = gameObject.GetComponent<Conversation>();
		cm = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
		chatBubbleDisplay = GetComponentInChildren<Canvas>();
		chatBubbleDisplay.enabled = false;
	}

	void Update()
	{
		isChatBubbleDisplayed = Wingman.wingmanVisionActive;

		if (isChatBubbleDisplayed != wasChatBubbleDisplayed)
		{
			chatBubbleDisplay.enabled = isChatBubbleDisplayed;
			wasChatBubbleDisplayed = isChatBubbleDisplayed;
		}
	}

	public void InteractWith()
	{
		cm.ProcessDialog(conversation.start);
	}
}
