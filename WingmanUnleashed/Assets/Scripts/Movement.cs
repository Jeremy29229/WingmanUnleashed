using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

	// Use this for initialization
	private float speed = 1.0f;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey("w"))
		{
			gameObject.rigidbody.AddForce(0, 0, -speed);
		}

		if (Input.GetKey("a"))
		{
			gameObject.rigidbody.AddForce(speed, 0, 0);


		}
		if (Input.GetKey("s"))
		{
			gameObject.rigidbody.AddForce(0, 0, speed);

		}
		if (Input.GetKey("d"))
		{
			gameObject.rigidbody.AddForce(-speed, 0, 0);
		}
	}
}
