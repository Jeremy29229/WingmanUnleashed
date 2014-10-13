using UnityEngine;

public class Movement : MonoBehaviour
{
	private float speed = 1.0f;

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
