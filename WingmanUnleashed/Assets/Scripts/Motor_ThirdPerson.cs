using UnityEngine;
using System.Collections;

public class Motor_ThirdPerson : MonoBehaviour
{
	public float movementSpeed = 10.0f;
	public static Motor_ThirdPerson Instance;

	public Vector3 MovementVector { get; set; }

	void Awake()
	{
		Instance = this;
	}

	public void UpdateMotor()
	{
		alignCharacter();

		MovementVector = transform.TransformDirection(MovementVector);
		MovementVector = Vector3.Normalize(MovementVector);
		MovementVector = (MovementVector * movementSpeed) * Time.deltaTime;
		rigidbody.position += MovementVector;
		MovementVector = new Vector3(0, 0, 0);
	}

	private void alignCharacter()
	{
		if (MovementVector.x != 0 || MovementVector.z != 0)
		{
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
												  Camera.main.transform.eulerAngles.y,
												  transform.eulerAngles.z);
		}
	}

	private void faceCharacterTowardsMovementDirection()
	{
		if (MovementVector.x != 0 || MovementVector.z != 0)
		{
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
												  Camera.main.transform.eulerAngles.y,
												  transform.eulerAngles.z);
		}
	}

}
