using UnityEngine;
using System.Collections;

public class Camera_ThirdPerson : MonoBehaviour 
{
	public float distanceBehindObject;
	public float distanceAboveObject;
	public Transform targetObject;
	public float movementSmoothing;

	private static Camera_ThirdPerson Instance;

	void Start () 
	{
		Instance = this;
	}
		
	void LateUpdate () 
	{
		Vector3 abovePlayer = targetObject.up * distanceAboveObject;
		Vector3 belowPlayer = targetObject.forward * distanceBehindObject;
		Vector3 newPosition = targetObject.position + abovePlayer - belowPlayer;

		transform.position = Vector3.Lerp (transform.position, newPosition, Time.deltaTime * movementSmoothing);

		transform.LookAt (targetObject);
	}
	

}
