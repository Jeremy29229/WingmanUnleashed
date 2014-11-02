using UnityEngine;

public class Camera_ThirdPerson : MonoBehaviour
{
	public static Camera_ThirdPerson Instance;
	public Transform TargetObjectLookAt;
	public float cameraDistance = 5.0f;
	public float minDistance = 3.0f;
	public float maxDistance = 10.0f;
	public float distanceSmoothing = 0.5f;
	public float x_MouseSensitivity = 5.0f;
	public float y_MouseSensitivity = 5.0f;
	public float mouseWheelSensitivity = 5.0f;
	public float y_MinLimit = -40.0f;
	public float y_MaxLimit = 40.0f;
	public float x_Smooth = 0.05f;
	public float y_Smooth = 0.1f;
	public bool usingFlightCamera;
	public bool IsInConversation = false;

	private float mouseX;
	private float mouseY;
	private float x_Velocity;
	private float y_Velocity;
	private float z_Velocity;
	private float velocityDistance;
	private float startDistance;
	private float desiredDistance;
	private Vector3 desiredPosition;
	private Vector3 position;

	void Awake()
	{
		Instance = this;
		mouseX = 0.0f;
		mouseY = 0.0f;
		x_Velocity = 0.0f;
		y_Velocity = 0.0f;
		z_Velocity = 0.0f;
		velocityDistance = 0.0f;
		startDistance = 0.0f;
		desiredDistance = 0.0f;
		desiredPosition = Vector3.zero;
		position = Vector3.zero;
		usingFlightCamera = false;
	}

	void Start()
	{
		cameraDistance = Mathf.Clamp(cameraDistance, minDistance, maxDistance);
		startDistance = cameraDistance;
		Reset();
	}

	void LateUpdate()
	{
		if (!IsInConversation)
		{

			if (usingFlightCamera)
			{
				//Vector3 abovePlayer = TargetObjectLookAt.up * cameraDistance;
				//Vector3 behindPlayer = TargetObjectLookAt.forward * cameraDistance;
				Vector3 newPosition = TargetObjectLookAt.position;

				transform.position = Vector3.Lerp(transform.position, newPosition, distanceSmoothing);

				transform.LookAt(TargetObjectLookAt);
			}
			else
			{
				if (targetLookAtExists())
				{
					HandlePlayerInput();
					CalculateDesiredPosition();
					UpdatePosition();
				}
			}
		}
	}

	void HandlePlayerInput()
	{
		mouseX += Input.GetAxis("Mouse X") * x_MouseSensitivity;
		mouseY -= Input.GetAxis("Mouse Y") * y_MouseSensitivity;
		mouseY = InputHelper.ClampAngle(mouseY, y_MinLimit, y_MaxLimit);

		float scrollWheelDeadZone = 0.01f;
		float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
		if (outsideDeadZone(scrollWheelValue, scrollWheelDeadZone))
		{
			float newCameraDistance = cameraDistance - scrollWheelValue * mouseWheelSensitivity;
			desiredDistance = Mathf.Clamp(newCameraDistance, minDistance, maxDistance);
		}
	}

	void CalculateDesiredPosition()
	{
		cameraDistance = Mathf.SmoothDamp(cameraDistance, desiredDistance, ref velocityDistance, distanceSmoothing);
		desiredPosition = CalculateNewPosition(mouseY, mouseX, cameraDistance);
	}

	private Vector3 CalculateNewPosition(float x_Rotation, float y_Rotation, float distance)
	{
		Vector3 direction = new Vector3(0.0f, 0.0f, -distance);
		Quaternion rotation = Quaternion.Euler(x_Rotation, y_Rotation, 0.0f);
		Vector3 calculatedPosition = TargetObjectLookAt.position + rotation * direction;
		return calculatedPosition;
	}

	void UpdatePosition()
	{
		float pos_X = Mathf.SmoothDamp(position.x, desiredPosition.x, ref x_Velocity, x_Smooth);
		float pos_Y = Mathf.SmoothDamp(position.y, desiredPosition.y, ref y_Velocity, y_Smooth);
		float pos_Z = Mathf.SmoothDamp(position.z, desiredPosition.z, ref z_Velocity, x_Smooth);
		position = new Vector3(pos_X, pos_Y, pos_Z);

		transform.position = position;

		transform.LookAt(TargetObjectLookAt);
	}

	public void Reset()
	{
		mouseX = 0.0f;
		mouseY = 10.0f;
		desiredDistance = cameraDistance = startDistance;
	}





	public static void checkForCameraCreation()
	{
		GameObject gameCamera;

		if (mainCameraExists())
		{
			gameCamera = Camera.main.gameObject;
		}
		else
		{
			gameCamera = new GameObject("Main Camera");
			gameCamera.AddComponent<Camera>();
			gameCamera.tag = "MainCamera";
			Debug.LogError("A main camera does not exist in the scene. A temporary camera has been created at the world origin.");
		}

		setupThirdPersonScript(gameCamera);
	}

	private static void setupThirdPersonScript(GameObject createdGameCamera)
	{
		GameObject targetLookAtPoint = GameObject.Find("TargetLookAt") as GameObject;

		if (targetLookAtPoint == null)
		{
			targetLookAtPoint = new GameObject("TargetLookAt");
			targetLookAtPoint.tag = "TargetLookAt";
			targetLookAtPoint.transform.position = Vector3.zero;
			Debug.LogError("The scene is missing an object for the camera to use as the \"TargetObjectLookAt\" object. A temporary game object to use as the lookAt point has been created at the world origin.");
		}


		Camera_ThirdPerson mainCameraTP_Script = createdGameCamera.GetComponent("Camera_ThirdPerson") as Camera_ThirdPerson;

		if (mainCameraTP_Script == null)
		{
			createdGameCamera.AddComponent("Camera_ThirdPerson");

			mainCameraTP_Script = createdGameCamera.GetComponent("Camera_ThirdPerson") as Camera_ThirdPerson;
			mainCameraTP_Script.TargetObjectLookAt = targetLookAtPoint.transform;
			Debug.LogError("The current main camera does not have the \"Camera_ThirdPerson\" script. This script component has been added via script.");
		}
		else
		{
			if (mainCameraTP_Script.TargetObjectLookAt == null)
			{
				mainCameraTP_Script.TargetObjectLookAt = targetLookAtPoint.transform;
				//Debug.Log("The current main camera's \"TargetObjectLookAt\" variable was successfully assigned via script");
			}
		}
	}

	private static bool mainCameraExists()
	{
		return Camera.main != null;
	}

	private bool targetLookAtExists()
	{
		return TargetObjectLookAt != null;
	}

	private bool outsideDeadZone(float scrollWheelValue, float scrollWheelDeadZone)
	{
		return (scrollWheelValue < -scrollWheelDeadZone || scrollWheelValue > scrollWheelDeadZone);
	}
}
