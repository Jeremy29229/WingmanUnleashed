using UnityEngine;
using System.Collections;

public class Motor_ThirdPerson : MonoBehaviour
{
	public float movementSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 2.0f;
	public static Motor_ThirdPerson Instance;
    bool isCrouching;

	public Vector3 MovementVector { get; set; }

	void Awake()
	{
		Instance = this;
        isCrouching = false;
	}

	public void UpdateMotor()
	{
		alignCharacter();
    
		MovementVector = transform.TransformDirection(MovementVector);
		MovementVector = Vector3.Normalize(MovementVector);
    
        float speedValue = 0.0f;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            transform.GetComponent<WingmanAnimator>().StartCrouching();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            transform.GetComponent<WingmanAnimator>().StopCrouching();
        }
    
        if (isCrouching)
        {
            speedValue = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speedValue = sprintSpeed;
        }
        else
        {
            speedValue = movementSpeed;
        }
        MovementVector = (MovementVector * speedValue) * Time.deltaTime;
		transform.position += MovementVector;
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
