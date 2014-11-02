using UnityEngine;

public class Ejector : MonoBehaviour
{
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
			Vector2 direction = Random.insideUnitCircle;
			GameObject.Find("Wingman").GetComponent<Rigidbody>().AddForce(new Vector3(direction.x * 1000.0f, 1000.0f, direction.y * 1000.0f));
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
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
