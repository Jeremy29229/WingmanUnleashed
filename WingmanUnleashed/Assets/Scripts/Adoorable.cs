using UnityEngine;

public class Adoorable : MonoBehaviour, IInteractable
{
	public Vector3 newPositionOffset;
	private GameObject wingman;

	void Start()
	{
		wingman = GameObject.Find("Wingman");
	}

	public void InteractWith()
	{
		wingman.transform.position = transform.position + newPositionOffset;
	}
}
