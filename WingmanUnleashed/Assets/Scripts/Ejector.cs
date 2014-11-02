using UnityEngine;

public class Ejector : MonoBehaviour
{
	public float ThrowingForce = 1000.0f;

	private GameObject wingman;
	private bool IsWingmanInRange = false;

	void Start()
	{
		wingman = GameObject.Find("Wingman");
	}

	void Update()
	{
		if (IsWingmanInRange && wingman.GetComponent<Player>().getDetectionLevel() >= 1.0f)
		{
			GameObject.Find("Wingman").GetComponent<Rigidbody>().AddForce(transform.parent.forward.normalized.x * ThrowingForce, ThrowingForce, transform.parent.forward.normalized.z * ThrowingForce); 
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
			GameObject.Find("Detection").audio.Stop();
			IsWingmanInRange = false;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == wingman)
		{
			IsWingmanInRange = true;
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (c.gameObject == wingman)
		{
			IsWingmanInRange = false;
		}
	}
}
