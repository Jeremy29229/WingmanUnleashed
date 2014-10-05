using UnityEngine;
using System.Collections;

public class Controller_ThirdPerson : MonoBehaviour 
{
	public float movementSpeed = 10.0f;
	private float horizontal;
	private float vertical;
	private CharacterController controller;

	private Vector3 MovementVector { get; set; }

	public static Controller_ThirdPerson Instance;

	void Awake() 
	{
		Instance = this;
		controller = GetComponent ("CharacterController") as CharacterController;
	}

	void Update() 
	{
		alignCharacter ();

		bool isPressed = false;

		if(inputTaken()) { isPressed = true; }

		if(isPressed)
		{
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");

			MovementVector = new Vector3 (horizontal, 0.0f, vertical);

			MovementVector = transform.TransformDirection (MovementVector);
			MovementVector = Vector3.Normalize (MovementVector);
			MovementVector = (MovementVector * movementSpeed) * Time.deltaTime;

			//controller.Move (movementVector);
			transform.position += MovementVector;
		}
	}

	private void alignCharacter()
	{
		if(MovementVector.x != 0 || MovementVector.z != 0)
		{
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
		}
	}

	private bool inputTaken()
	{
		return (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D));
	}


}
