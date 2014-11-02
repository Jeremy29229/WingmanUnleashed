using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour
{
	public string NPCDialog;
	public DialogResponse[] Responses = new DialogResponse[4];
	private Interactable npc;
	
	void Start()
	{
		npc = (GetComponent<Interactable>()) ? GetComponent<Interactable>() : null;
	}

	public string GetNPCName()
	{
		return npc.InteractableName;
	}
}
