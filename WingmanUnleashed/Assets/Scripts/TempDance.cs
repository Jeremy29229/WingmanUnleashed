using UnityEngine;

public class TempDance : MonoBehaviour
{

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.GetComponentInParent<NPCControlScript>() != null)
		{
			c.gameObject.GetComponentInParent<NPCControlScript>().ActivateWanderer();
		}
	}
}
