using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversable : MonoBehaviour, IInteractable
{
	private Conversation conversation;
	private ConversationManager cm;
	private GameObject Player;

	void Start()
	{
		Player = GameObject.Find("Wingman");
		conversation = gameObject.GetComponent<Conversation>();
		cm = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
	}

	public void InteractWith()
	{
		cm.ProcessDialog(conversation.start);
	}
}
