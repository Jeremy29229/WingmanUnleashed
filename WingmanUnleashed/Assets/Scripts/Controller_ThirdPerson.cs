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
	public bool IsInConversation = false;

	private float horizontal;
	private float vertical;

	private Vector3 MovementVector { get; set; }

	public static Controller_ThirdPerson Instance;

	void flightmodeOff()
	{
		Camera_ThirdPerson.Instance.usingFlightCamera = false;
		acceleration = new Vector3(0.0f, 0.0f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		flightmode = false;
		player.transform.rotation = new Quaternion(0.0f, player.transform.rotation.y, 0.0f, player.transform.rotation.w);
		player.transform.GetChild(1).transform.localRotation = Quaternion.identity;
        CapsuleCollider coll = (CapsuleCollider)player.GetComponent("CapsuleCollider");
		coll.center = new Vector3(0.0f, 0.97f, 0.0f);
        coll.direction = 1;
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = true;
	}
	void flightmodeOn()
	{
		Camera_ThirdPerson.Instance.usingFlightCamera = true;
		acceleration = new Vector3(0.0f, -9.81f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		flightmode = true;
		player.transform.GetChild(1).transform.Rotate(new Vector3(1, 0, 0), 90);
        CapsuleCollider coll = (CapsuleCollider)player.GetComponent("CapsuleCollider");
		coll.center = new Vector3(0.0f, 0.0f, 0.97f);
        coll.direction = 2;
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = false;
	}

	void Awake()
	{
		Instance = this;
		Camera_ThirdPerson.checkForCameraCreation();
	}

	void Update()
	{
		if (!IsInConversation)
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
				if (Camera.main == null)
				{
					Debug.LogError("There needs to be a main camera for the third-person camera");
				}
				else
				{
					if (inputTaken())
					{
						horizontal = Input.GetAxis("Horizontal");
						vertical = Input.GetAxis("Vertical");
						Motor_ThirdPerson.Instance.MovementVector = new Vector3(horizontal, 0.0f, vertical);
						Motor_ThirdPerson.Instance.UpdateMotor();
					}
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
	}

	private bool inputTaken()
	{
		return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
	}
}
