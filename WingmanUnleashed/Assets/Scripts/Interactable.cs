using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows a game object to be interacted with. GameObject must also have another script component that implements IIteractable.
/// </summary>
public class Interactable : MonoBehaviour
{
	public float InteractionRadius = 1.0f;
	public KeyCode InteractionKey = KeyCode.E;
	public bool IsRepeatable = false;
	public bool IsActive = true;
	public string PlayerObjectName = "CharacterBasic";
	public string Action;
	public string InteractableName;
	private IInteractable behavior;
	private GameObject Player;
	private Canvas UI;

	void Start()
	{
		UI = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
		UI.enabled = false;

		Player = GameObject.Find(PlayerObjectName);

		if (Player == null)
		{
			throw new UnassignedReferenceException("Player must be defined");
		}

		behavior = (IInteractable)gameObject.GetComponent("IInteractable");

		if (behavior == null)
		{
			throw new UnassignedReferenceException("A script that implements IInteractable must be a component in the same GameOject as this script.");
		}

		GameObject.Find("InteractionManager").GetComponent<InteractionManager>().Interactables.Add(gameObject);
	}

	public void InteractionUpdate()
	{
		if (Vector3.Distance(Player.transform.position, gameObject.transform.position) <= InteractionRadius)
		{
			UI.enabled = true;
			if (Input.GetKeyDown(InteractionKey))
			{
				behavior.InteractWith();
			}
		}
		else
		{
			UI.enabled = false;
		}
	}

	public void updateGUIText()
	{
		UI.GetComponentInChildren<Text>().text = "Press " + InteractionKey.ToString() + " to " + Action + " " + InteractableName;
	}
}
