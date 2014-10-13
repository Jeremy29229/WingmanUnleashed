using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversable : MonoBehaviour, IInteractable
{
	private Conversation conversation;
	private GameObject Player;

	void start()
	{
		Player = GameObject.Find("CharacterBasic");
		conversation = gameObject.GetComponent<Conversation>();
	}

	public void InteractWith()
	{
		//throw new System.NotImplementedException();
	}
}
