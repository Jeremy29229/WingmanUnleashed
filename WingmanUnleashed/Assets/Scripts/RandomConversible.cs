using UnityEngine;

public class RandomConversible : MonoBehaviour, IInteractable
{
	private Correspondence correspondence;
	private ConversationManager cm;
	private Player Wingman;
	private bool isChatBubbleDisplayed = false;
	private bool wasChatBubbleDisplayed = false;
	private Canvas chatBubbleDisplay;
	private MouseManager mouseManager;

	void Start()
	{
		Wingman = GameObject.Find("Wingman").GetComponent<Player>();
		correspondence = gameObject.GetComponent<Correspondence>();
		cm = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
		chatBubbleDisplay = GetComponentInChildren<Canvas>();
		chatBubbleDisplay.enabled = false;
		mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();

		if (correspondence.Conversations.Length == 0)
		{
			GetComponent<Interactable>().IsActive = false;
		}
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
		if (correspondence.Conversations.Length > 0)
		{
			int selection = Random.Range(0, correspondence.Conversations.Length);
			cm.ProcessDialog(correspondence.Conversations[selection].Beginning);
		}
	}
}
