using UnityEngine;
using System.Collections;

public class Controller_ThirdPerson : MonoBehaviour
{
	public GameObject player;
	bool flightmode = false;
	Vector3 velocity;
	Vector3 acceleration;
	Vector3 lift;
	float windResistance = 1.15f;

	public float movementSpeed = 10.0f;
	private float horizontal;
	private float vertical;

	private Vector3 MovementVector { get; set; }

	public static Controller_ThirdPerson Instance;

	void flightmodeOff()
	{
		acceleration = new Vector3(0.0f, 0.0f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		flightmode = false;
		player.transform.rotation = new Quaternion(0.0f, player.transform.rotation.y, 0.0f, player.transform.rotation.w);
		player.transform.GetChild(0).transform.localRotation = Quaternion.identity;
		BoxCollider coll = (BoxCollider)player.GetComponent("BoxCollider");
		coll.center = new Vector3(0.0f, 0.9f, 0.0f);
		coll.size = new Vector3(1.5f, 1.8f, 0.4f);
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = true;
	}
	void flightmodeOn()
	{
		acceleration = new Vector3(0.0f, -9.81f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		flightmode = true;
		player.transform.GetChild(0).transform.Rotate(new Vector3(1, 0, 0), 90);
		BoxCollider coll = (BoxCollider)player.GetComponent("BoxCollider");
		coll.center = new Vector3(0.0f, 0.0f, 0.9f);
		coll.size = new Vector3(1.5f, 0.4f, 1.8f);
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = false;
	}

	void Awake()
	{
		Instance = this;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (flightmode)
			{
				flightmodeOff();
			}
			else
			{
				flightmodeOn();
			}
		}

		if (flightmode)
		{
			float airspeed = velocity.magnitude;
			lift = new Vector3(0.0f, airspeed / 2.0f, airspeed);
			lift = player.transform.rotation * lift;
			Vector3 netforce = acceleration + lift;
			velocity += netforce * Time.deltaTime;
			velocity *= (1 - (windResistance * Time.deltaTime));
			print(velocity);
			player.transform.position += velocity * Time.deltaTime;

			if (Input.GetKey(KeyCode.Q))
			{
				player.transform.Rotate(new Vector3(0, 0, 1), 1.0f, Space.Self);
				//player.transform.GetChild(0).transform.Rotate(new Vector3(0,1,0),2);
			}
			if (Input.GetKey(KeyCode.E))
			{
				player.transform.Rotate(new Vector3(0, 0, 1), -1.0f, Space.Self);
				//player.transform.GetChild(0).transform.Rotate(new Vector3(0,1,0),-2);
			}
			if (Input.GetKey(KeyCode.W))
			{
				player.transform.Rotate(new Vector3(1, 0, 0), 1.0f, Space.Self);
			}
			if (Input.GetKey(KeyCode.S))
			{
				player.transform.Rotate(new Vector3(1, 0, 0), -1.0f, Space.Self);
			}
			if (Input.GetKey(KeyCode.A))
			{
				player.transform.Rotate(new Vector3(0, 1, 0), -1.0f, Space.Self);
			}
			if (Input.GetKey(KeyCode.D))
			{
				player.transform.Rotate(new Vector3(0, 1, 0), 1.0f, Space.Self);
			}
		}
		else
		{
			alignCharacter();

			bool isPressed = false;

			if (inputTaken()) { isPressed = true; }

			if (isPressed)
			{
				horizontal = Input.GetAxis("Horizontal");
				vertical = Input.GetAxis("Vertical");

				MovementVector = new Vector3(horizontal, 0.0f, vertical);

				MovementVector = transform.TransformDirection(MovementVector);
				MovementVector = Vector3.Normalize(MovementVector);
				MovementVector = (MovementVector * movementSpeed) * Time.deltaTime;

				//controller.Move (movementVector);
				transform.position += MovementVector;
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			flightmodeOff();
			player.transform.position = new Vector3(1328.158f, 999.9615f, 165.7299f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			flightmodeOff();
			player.transform.position = new Vector3(240.0185f, 865.9557f, 1163.175f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			flightmodeOff();
			player.transform.position = new Vector3(1814.116f, 719.9808f, 1764.895f);
		}
	}

	private void alignCharacter()
	{
		if (MovementVector.x != 0 || MovementVector.z != 0)
		{
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
		}
	}

	private bool inputTaken()
	{
		return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
	}
}
