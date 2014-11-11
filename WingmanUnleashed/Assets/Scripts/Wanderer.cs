using UnityEngine;
using System.Collections;

public class Wanderer : MonoBehaviour
{
	private Vector3 basePosition;
	private Vector3 targetPosition;
	public float range = 0.5f;
	public float speed = 1.0f;

	void Start()
	{
		gameObject.GetComponent<NavMeshAgent>().acceleration = speed;
		basePosition = gameObject.transform.position;
		targetPosition = basePosition + new Vector3((Random.value * (2 * range)) - range, 0, (Random.value * (2 * range)) - range);
		gameObject.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
		if (gameObject.GetComponent<CharacterAnimator>() != null)
		{
			if (Random.Range(0, 100) >= 50) gameObject.GetComponent<CharacterAnimator>().StartDancingGangnam();
			else gameObject.GetComponent<CharacterAnimator>().StartDancingSamba();
		}
	}

	void Update()
	{
		if (range > 0)
		{
			Vector3 distance = gameObject.transform.position - targetPosition;
			if (distance.magnitude < 1)
			{
				targetPosition = basePosition + new Vector3((Random.value * (2 * range)) - range, 0, (Random.value * (2 * range)) - range);
				gameObject.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
			}
		}
	}
}
