using UnityEngine;
using System.Collections;

public class BouncerAI : MonoBehaviour
{
	public float patrolSpeed = 2.0f;
	public float patrolPointWaitTime = 1.0f;
	public Transform[] waypoints;

	private VisionDetection detection;
	private NavMeshAgent nav;
	private Transform playerWingman;
	private float patrolPointWait_Timer;
	private int currentWaypoint;
	private float stopZone;

	void Awake()
	{
		detection = GetComponentInChildren<VisionDetection>();
		nav = GetComponent<NavMeshAgent>();
		playerWingman = GameObject.Find("Wingman").transform;
		stopZone = 0.6f;
		currentWaypoint = 0;
	}

	void LateUpdate()
	{
		if (!detection.getPlayerInRange())
		{
			Patrolling();
		}
		else
		{
			PlayerDetected();
		}
		
	}

	public void PlayerDetected()
	{
		nav.Stop();
	}

	public void Patrolling()
	{
		nav.speed = patrolSpeed;
		if (waypoints[currentWaypoint] != null)
		{
			Vector3 wayPointVector = waypoints[currentWaypoint].position;
			Vector3 distanceVector = wayPointVector - transform.position;

			if (distanceVector.magnitude <= stopZone)
			{
				patrolPointWait_Timer += Time.deltaTime;

				if (patrolPointWait_Timer >= patrolPointWaitTime)
				{
					if (currentWaypoint == waypoints.Length - 1)
					{
						currentWaypoint = 0;
					}
					else
					{
						currentWaypoint++;
					}

					patrolPointWait_Timer = 0.0f;
				}
			}

			nav.destination = waypoints[currentWaypoint].position;
		}
	}
}
