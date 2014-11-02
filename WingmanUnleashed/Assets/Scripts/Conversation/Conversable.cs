using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversable : MonoBehaviour, IInteractable
{
	private Correspondence correspondence;
	private ConversationManager cm;
	private Player Wingman;
	private bool isChatBubbleDisplayed = false;
	private bool wasChatBubbleDisplayed = false;
	private Canvas chatBubbleDisplay;

	void Start()
	{
		Wingman = GameObject.Find("Wingman").GetComponent<Player>();
		correspondence = gameObject.GetComponent<Correspondence>();
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
		cm.ProcessDialog(correspondence.Current.Beginning);
	}
}
