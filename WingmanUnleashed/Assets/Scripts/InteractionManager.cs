using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
	public List<GameObject> Interactables;
	public string PlayerObjectName = "CharacterBasic";
	private GameObject player;
	private Canvas UI;

	// Use this for initialization
	void Start()
	{
		UI = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
		player = GameObject.Find(PlayerObjectName);
	}

	// Update is called once per frame
	void Update()
	{
		GameObject closest = null;
		float minDistance = float.MaxValue;

		foreach (var v in Interactables)
		{
			if (v == null)
			{
				Interactables.Remove(v);
				break;
			}
			else
			{
				float currentDistance = Vector3.Distance(player.transform.position, v.transform.position);
				if (currentDistance < minDistance)
				{
					minDistance = currentDistance;
					closest = v;
				}
			}
		}

		if (closest == null)
		{
			UI.enabled = false;
		}

		else
		{
			closest.GetComponent<Interactable>().updateGUIText();
			closest.GetComponent<Interactable>().InteractionUpdate();

		}
	}
}
