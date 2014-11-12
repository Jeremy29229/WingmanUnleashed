using UnityEngine;

public class Adoorable : MonoBehaviour, IInteractable
{
	public Vector3 newPositionOffset;
	public GameObject ConnectingDoor;
	public bool RequireBouncerSuit = false;
	private GameObject wingman;

	void Start()
	{
		wingman = GameObject.Find("Wingman");
	}

	void Update()
	{
		if (wingman.GetComponent<Player>().getDetectionLevel() < 1.0f)
		{
			GetComponent<Interactable>().IsActive = true;
		}
		else
		{
			GetComponent<Interactable>().IsActive = false;
		}
	}

	public void InteractWith()
	{
		if (!RequireBouncerSuit || wingman.GetComponent<Outfit>().outfitName == "bouncer")
		{
			wingman.transform.position = ConnectingDoor.transform.position + newPositionOffset;
			wingman.GetComponent<Controller_ThirdPerson>().flightmodeOff();
		}
	}
}
