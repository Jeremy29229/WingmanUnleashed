using UnityEngine;
using System.Collections;

public class Controller_ThirdPerson : MonoBehaviour
{
	public GameObject player;
	public bool flightmode = false;
	Vector3 velocity;
	Vector3 acceleration;
	Vector3 lift;
	float windResistance = 0.5f;
    public AudioSource windSound;
    float jumpHeight = 200.0f;

	public bool IsInConversation = false;

	private float horizontal;
	private float vertical;

	private Vector3 MovementVector { get; set; }

	public static Controller_ThirdPerson Instance;

	public void flightmodeOff()
	{
		Camera_ThirdPerson.Instance.usingFlightCamera = false;
        Camera_ThirdPerson.Instance.distanceSmoothing = 0.1f;
		acceleration = new Vector3(0.0f, 0.0f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		velocity = new Vector3(0.0f, 0.0f, 0.0f);
		flightmode = false;
		player.transform.rotation = new Quaternion(0.0f, player.transform.rotation.y, 0.0f, player.transform.rotation.w);
		player.transform.GetChild(1).transform.localRotation = Quaternion.identity;
        player.transform.GetChild(1).transform.localPosition = new Vector3(0,0,0);
        CapsuleCollider coll = (CapsuleCollider)player.GetComponent("CapsuleCollider");
        coll.direction = 1;
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = true;

        windSound.Stop();
	}
	public void flightmodeOn()
	{
		Camera_ThirdPerson.Instance.usingFlightCamera = true;
        Camera_ThirdPerson.Instance.distanceSmoothing = 0.04f;
        if (gameObject.GetComponent<Player>().wingmanVisionActive) gameObject.GetComponent<Player>().deactivateWingmanVision();
		acceleration = new Vector3(0.0f, -9.81f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		//velocity = new Vector3(0.0f, 16.5f, 0.0f) + player.transform.forward*5.5f;
		flightmode = true;
		player.transform.GetChild(1).transform.Rotate(new Vector3(1, 0, 0), 90);
        player.transform.GetChild(1).transform.localPosition = new Vector3(0.0f, 0.97f,-0.97f);
        CapsuleCollider coll = (CapsuleCollider)player.GetComponent("CapsuleCollider");
        coll.direction = 2;
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = false;
        windSound.Play();
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
			//if (Input.GetKeyDown(KeyCode.Space))
			//{
			//	if (flightmode)
			//	{
			//		flightmodeOff();
			//	}
			//	else
			//	{
			//		flightmodeOn();
			//	}
			//}

			if (flightmode)
			{
				float airspeed = velocity.magnitude;
                windResistance = airspeed / 100.0f;
				lift = new Vector3(0.0f, 1.0f, 0.0f);
				lift = player.transform.rotation * lift;
                lift = lift * Mathf.Abs(Vector3.Dot(lift, Vector3.up)) * (airspeed/2.0f);
                if (lift.y < 0.0f) lift = lift * -1.0f;
				Vector3 netforce = acceleration + lift;
				velocity += netforce * Time.deltaTime;
				velocity *= (1 - (windResistance * Time.deltaTime));
				player.transform.position += velocity * Time.deltaTime;

                float correctionForce = Quaternion.Angle(player.transform.rotation, Quaternion.LookRotation(velocity, Vector3.up))*(airspeed/30.0f);
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.LookRotation(velocity, Vector3.up), correctionForce * Time.deltaTime);

                windSound.volume = Mathf.Pow((airspeed / 30.0f),4);
				if (Input.GetKey(KeyCode.A))
				{
					player.transform.Rotate(new Vector3(0, 0, 1), 1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.D))
				{
					player.transform.Rotate(new Vector3(0, 0, 1), -1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.W))
				{
					player.transform.Rotate(new Vector3(1, 0, 0), 1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.S))
				{
					player.transform.Rotate(new Vector3(1, 0, 0), -1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.Q))
				{
					player.transform.Rotate(new Vector3(0, 1, 0), -1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.E))
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
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
                            rig.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
                        }
						Motor_ThirdPerson.Instance.UpdateMotor();
					}
                    
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				player.transform.position = new Vector3(1328.158f, 1010.9615f, 162.7299f);
                flightmodeOff();
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				player.transform.position = new Vector3(310.0185f, 890.5f, 1143.175f);
                flightmodeOff();
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				player.transform.position = new Vector3(1764.116f, 828.00f, 1764.895f);
                flightmodeOff();
			}
		}
	}

	private bool inputTaken()
	{
		return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space));
	}
}
