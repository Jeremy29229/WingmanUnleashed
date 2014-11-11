using UnityEngine;

public class Adoorable : MonoBehaviour, IInteractable
{
	public Vector3 newPositionOffset;
	public GameObject ConnectingDoor;
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
		wingman.transform.position = ConnectingDoor.transform.position + newPositionOffset;
		//print(wingman.transform.position);
		wingman.GetComponent<Controller_ThirdPerson>().flightmodeOff();
	}
}
