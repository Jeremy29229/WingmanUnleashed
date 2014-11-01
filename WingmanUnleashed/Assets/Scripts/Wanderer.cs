using UnityEngine;
using System.Collections;

public class Wanderer : MonoBehaviour
{
	private Vector3 basePosition;
	private Vector3 targetPosition;
	public float range = 5.0f;

	void Start()
	{
		basePosition = gameObject.transform.position;
		targetPosition = basePosition + new Vector3((Random.value * (2 * range)) - range, 0, (Random.value * (2 * range)) - range);
		//gameObject.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
	}

	void Update()
	{
		Vector3 distance = gameObject.transform.position - targetPosition;
		if (distance.magnitude < 1)
		{
			targetPosition = basePosition + new Vector3((Random.value * (2 * range)) - range, 0, (Random.value * (2 * range)) - range);
			gameObject.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
		}
	}
}
