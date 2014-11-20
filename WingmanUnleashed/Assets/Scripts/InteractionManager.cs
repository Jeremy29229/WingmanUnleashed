using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
	public List<GameObject> Interactables;
	public string PlayerObjectName = "Wingman";

	private GameObject player;
	private Canvas UI;
	private bool isShowing = true;

	void Start()
	{
		UI = (Canvas)GameObject.Find("InteractionGUI").GetComponent(typeof(Canvas));
		player = GameObject.Find(PlayerObjectName);
	}

	void Update()
	{
		GameObject closest = null;
		float minDistance = float.MaxValue;

		if (isShowing)
		{

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
					if (currentDistance < minDistance && v.GetComponent<Interactable>().IsActive)
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

	public void Hide()
	{
		isShowing = false;
		UI.enabled = false;
	}

	public void Show()
	{
		isShowing = true;
		UI.enabled = true;
	}
}
