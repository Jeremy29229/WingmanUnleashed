using UnityEngine;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
	public List<GameObject> Interactables;
	public string PlayerObjectName = "Wingman";

	private GameObject player;
	private Canvas UI;

	void Start()
	{
		UI = (Canvas)GameObject.Find("InteractionGUI").GetComponent(typeof(Canvas));
		player = GameObject.Find(PlayerObjectName);
	}

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
