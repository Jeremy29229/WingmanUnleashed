using UnityEngine;

public class Concealer : MonoBehaviour
{
	void OnTriggerEnter(Collider c)
	{
		Detectable d = c.gameObject.GetComponent("Detectable") as Detectable;
		if (d != null)
		{
			d.DetectionValue--;
		}
	}

	void OnTriggerExit(Collider c)
	{
		Detectable d = c.gameObject.GetComponent("Detectable") as Detectable;
		if (d != null)
		{
			d.DetectionValue++;
		}
	}
}
