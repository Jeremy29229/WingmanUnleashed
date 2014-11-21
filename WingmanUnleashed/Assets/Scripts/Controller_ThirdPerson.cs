using UnityEngine;
using System.Collections;

public class Controller_ThirdPerson : MonoBehaviour
{
	public GameObject player;
	public bool flightmode = false;
	public Vector3 velocity;
	Vector3 acceleration;
	Vector3 lift;
	float windResistance = 0.5f;
	public AudioSource windSound;
	float jumpHeight = 300.0f;
	float inAirCount = 0.0f;
	bool coverBlown = false;

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
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = true;
		rig.velocity = new Vector3(0, 0, 0);
		windSound.Stop();
		player.transform.GetComponent<WingmanAnimator>().ExitTPose();
		player.transform.GetChild(3).transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().enabled = false;
	}
	public void flightmodeOn()
	{
		Camera_ThirdPerson.Instance.usingFlightCamera = true;		Camera_ThirdPerson.Instance.distanceSmoothing = 0.04f;
		if (gameObject.GetComponent<Player>().wingmanVisionActive) gameObject.GetComponent<Player>().deactivateWingmanVision();
		acceleration = new Vector3(0.0f, -9.81f, 0.0f);
		lift = new Vector3(0.0f, 0.0f, 0.0f);
		flightmode = true;
		player.transform.Rotate(new Vector3(1, 0, 0), 90);
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = false;
		windSound.Play();
		player.transform.GetComponent<WingmanAnimator>().EnterTPose();
		player.transform.GetChild(3).transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().enabled = true;
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
			if (flightmode)
			{
				float airspeed = velocity.magnitude;
				player.transform.Rotate(new Vector3(1, 0, 0), -90.0f, Space.Self);
				windResistance = airspeed / 100.0f;
				lift = new Vector3(0.0f, 1.0f, 0.0f);
				lift = player.transform.rotation * lift;
				lift = lift * Mathf.Abs(Vector3.Dot(lift, Vector3.up)) * (airspeed / 2.0f);
				if (lift.y < 0.0f) lift = lift * -1.0f;
				Vector3 netforce = acceleration + lift;
				velocity += netforce * Time.deltaTime;
				velocity *= (1 - (windResistance * Time.deltaTime));
				player.transform.position += velocity * Time.deltaTime;
				float correctionForce = Quaternion.Angle(player.transform.rotation, Quaternion.LookRotation(velocity, Vector3.up)) * (airspeed / 30.0f);
				player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation,Quaternion.LookRotation(velocity, Vector3.up), correctionForce * Time.deltaTime);
				player.transform.Rotate(new Vector3(1, 0, 0), 90.0f, Space.Self);
				windSound.volume = Mathf.Pow((airspeed / 30.0f), 4);
				if (Input.GetKey(KeyCode.Q))
				{
					player.transform.Rotate(new Vector3(0, 0, 1), 1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.E))
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
				if (Input.GetKey(KeyCode.D))
				{
					player.transform.Rotate(new Vector3(0, 1, 0), -1.0f, Space.Self);
				}
				if (Input.GetKey(KeyCode.A))
				{
					player.transform.Rotate(new Vector3(0, 1, 0), 1.0f, Space.Self);
				}
				if (airspeed > 5.0f)
				{
					if (Grounded()) flightmodeOff();
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
						if (Input.GetKeyDown(KeyCode.C))
						{
							player.transform.GetComponent<WingmanAnimator>().ResetToIdle();
							player.transform.GetComponent<WingmanAnimator>().StartDancingGangnam();
							if (gameObject.GetComponent<Player>().numDetectors > 0) coverBlown = true;
						}
						if (Input.GetKeyUp(KeyCode.C))
						{
							player.transform.GetComponent<WingmanAnimator>().StopDancingGangnam();
							coverBlown = false;
						}
						if (!Input.GetKey(KeyCode.C))
						{
							if (Input.GetKeyDown(KeyCode.W))
							{
								player.transform.GetComponent<WingmanAnimator>().StartWalking();
							}
							if (Input.GetKeyDown(KeyCode.A))
							{
								player.transform.GetComponent<WingmanAnimator>().StartStrafingLeft();
							}
							if (Input.GetKeyDown(KeyCode.S))
							{
								player.transform.GetComponent<WingmanAnimator>().StartWalking();
							}
							if (Input.GetKeyDown(KeyCode.D))
							{
								player.transform.GetComponent<WingmanAnimator>().StartStrafingRight();
							}
							if (Input.GetKeyUp(KeyCode.W))
							{
								player.transform.GetComponent<WingmanAnimator>().StopWalking();
							}
							if (Input.GetKeyUp(KeyCode.A))
							{
								player.transform.GetComponent<WingmanAnimator>().StopStrafingLeft();
							}
							if (Input.GetKeyUp(KeyCode.S))
							{
								player.transform.GetComponent<WingmanAnimator>().StopWalking();
							}
							if (Input.GetKeyUp(KeyCode.D))
							{
								player.transform.GetComponent<WingmanAnimator>().StopStrafingRight();
							}

							// moveing
							horizontal = Input.GetAxisRaw("Horizontal");
							vertical = Input.GetAxisRaw("Vertical");
							Motor_ThirdPerson.Instance.MovementVector = new Vector3(horizontal, 0.0f, vertical);

							if (Input.GetKeyDown(KeyCode.Space))
							{
								if (CanJump())
								{
									Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
									rig.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
								}
								else if (!Grounded()) flightmodeOn();
							}
							Motor_ThirdPerson.Instance.UpdateMotor();

						}
						else if(!coverBlown)gameObject.GetComponent<Player>().decreaseDetection(0.1f);
					}
					else if(!player.transform.GetComponent<WingmanAnimator>().IsIdle())
					{
						player.transform.GetComponent<WingmanAnimator>().ResetToIdle();
					}
				}
				if (InAir()) flightmodeOn();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1)||(player.transform.position.y<=0&&player.transform.position.y>-5))
			{
				player.transform.position = new Vector3(1446.1f, 241.58f, 555.77f);
				flightmodeOff();
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				player.transform.position = new Vector3(1716.2f, -21.563f, 207.66f);
				flightmodeOff();
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				player.transform.position = new Vector3(1328.158f, 1000.9615f, 162.7299f);
				flightmodeOff();
			}
		}
	}

	void OnCollisionEnter(Collision c)
	{
		if (flightmode && velocity.magnitude<=5.0f)
		{
			flightmodeOff();
		}
	}

	private bool CanJump()
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		if (Physics.Linecast(player.transform.position + new Vector3(0.0f, 0.2f, 0.0f), player.transform.position - new Vector3(0.0f, 0.2f, 0.0f), out hit))
		{
			if (!hit.collider.isTrigger) result = true;
		}
		return result;
	}
	private bool Grounded()
	{
		bool result = false;
		RaycastHit hit;// = new RaycastHit();
		Debug.DrawLine(player.transform.position + player.transform.rotation * new Vector3(0.0f, 1.1f, 0.0f), player.transform.position - new Vector3(0.0f, 1.50f, 0.0f), Color.red, 1000);

		if (Physics.Linecast(player.transform.position + player.transform.rotation * new Vector3(0.0f, 1.1f, 0.0f), player.transform.position - new Vector3(0.0f, 1.50f, 0.0f), out hit))
		{
			if (!hit.collider.isTrigger) result = true;
		}
		return result;
	}
	private bool InAir()
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		if (Physics.Linecast(player.transform.position + new Vector3(0.0f, 0.2f, 0.0f), player.transform.position - new Vector3(0.0f, 10.0f, 0.0f), out hit) && !hit.collider.isTrigger)
		{
			inAirCount = 0.0f;
		}
		else
		{
			inAirCount += Time.deltaTime;
			if (inAirCount > 1.5f)
			{
				result = true;
				inAirCount = 0.0f;
			}
		}
		return result;
	}

	private bool inputTaken()
	{
		return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.C) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl));
	}
}
