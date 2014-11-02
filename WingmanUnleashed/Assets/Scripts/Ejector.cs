using UnityEngine;

public class Ejector : MonoBehaviour
{
	private GameObject wingman;

	void Start()
	{
		wingman = GameObject.Find("Wingman");
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == wingman)
		{
			if (wingman.GetComponent<Player>().getDetectionLevel() >= 1.0f)
			{
				Vector2 direction = Random.insideUnitCircle;
				GameObject.Find("Wingman").GetComponent<Rigidbody>().AddForce(new Vector3(direction.x * 1000.0f, 1000.0f, direction.y * 1000.0f));
				GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
			}
		}
	}
}
