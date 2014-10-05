using UnityEngine;
using System.Collections;

public class Concealer : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

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
