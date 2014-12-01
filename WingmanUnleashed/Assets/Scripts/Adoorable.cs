using UnityEngine;

public class Adoorable : MonoBehaviour, IInteractable
{
	public GameObject ConnectingDoor;
	public Vector3 PlayerFromDoorOffset = new Vector3(0, -1.3f, 1);
	public bool RequiresOutfit = false;
	public string OutfitName = "bouncer";

	private GameObject wingman;

	void Start()
	{
		wingman = GameObject.Find("Wingman");
	}

	void Update()
	{
		GetComponent<Interactable>().IsCurrentlyInteractable = (ConnectingDoor != null);
		
		if (GetComponent<Interactable>().IsCurrentlyInteractable)
		{
			if (RequiresOutfit && wingman.GetComponent<Outfit>().outfitName != OutfitName)
			{
				GetComponent<Interactable>().IsCurrentlyInteractable = false;
				GetComponent<Interactable>().AdditionalInformation = "<color=red>(requires " + OutfitName + " suit)</color>";
			}
			else
			{
				GetComponent<Interactable>().AdditionalInformation = "";
			}
		}
		else
		{
			GetComponent<Interactable>().AdditionalInformation = "(locked)";
		}
	}

	public void InteractWith()
	{
		if (GetComponent<Interactable>().IsCurrentlyInteractable && (!RequiresOutfit || wingman.GetComponent<Outfit>().outfitName == OutfitName))
		{
			wingman.transform.position = ConnectingDoor.transform.position + ConnectingDoor.GetComponent<Adoorable>().PlayerFromDoorOffset;
			wingman.GetComponent<Controller_ThirdPerson>().flightmodeOff();
		}
	}
}
