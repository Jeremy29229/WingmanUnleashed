using UnityEngine;
using System.Collections;

public class BouncerAI : MonoBehaviour
{
	public float patrolSpeed = 2.0f;
	public float pursueSpeed = 4.0f;
	public float patrolPointWaitTime = 1.0f;
	public float pauseWaitTime = 1.0f;
	public float timeSearching = 3.0f;
	public float stopDistanceFromPlayer = 0.2f;
	public Transform[] waypoints;

	private CharacterAnimator animation;
	private VisionDetection detection;
	private NavMeshAgent nav;
	private Transform playerWingman;
	private Player player;
	private float patrolPointWait_Timer;
	private int currentWaypoint;
	private float stopZone;
	private float maxStopToLookLevel;
	private float maxAwareLevel;
	private float maxPursueLevel;
	private bool searching;
	private bool lastPositionKnown;
	private float totalTimeSearching;
	private float totalTimePausing;
	private Vector3 lastKnownPlayerPosition;
	private Vector3 distractionPos;
	private bool checkingDistraction;
	private float DEFAULT_STOP = 0.0f;

	void Start()
	{
		animation = GetComponent<CharacterAnimator>();
		detection = GetComponentInChildren<VisionDetection>();
		nav = GetComponent<NavMeshAgent>();
		playerWingman = GameObject.Find("Wingman").transform;
		player = GameObject.Find("Wingman").GetComponent<Player>() as Player;
		stopZone = 0.6f;
		currentWaypoint = 0;
		maxStopToLookLevel = 0.4f;
		maxAwareLevel = 0.7f;
		maxPursueLevel = 1.0f;
		searching = false;
		lastPositionKnown = false;
		totalTimeSearching = 0.0f;
		lastKnownPlayerPosition = Vector3.zero;
		checkingDistraction = false;
	}

	void LateUpdate()
	{
		float currentDetectionLevel = player.getDetectionLevel();

		if (Input.GetKeyDown(KeyCode.P))
		{
			DistractionManager.Instance.AddDistraction(10.0f, 10.0f, playerWingman.position);
		}

		if (currentDetectionLevel <= maxStopToLookLevel)
		{
			Patrolling();
		}
		else if (currentDetectionLevel > maxStopToLookLevel && currentDetectionLevel <= maxAwareLevel)
		{
			Aware();
		}
		else if (currentDetectionLevel > maxAwareLevel && currentDetectionLevel <= maxPursueLevel)
		{
			Pursuing();
		}
		else if (currentDetectionLevel > maxPursueLevel)
		{
			FullPursuit();
		}
	}

	public void FullPursuit()
	{
		nav.speed = pursueSpeed;
		nav.stoppingDistance = DEFAULT_STOP;

		if (detection.IsPlayInRangeAndVisable) //Pursuing player
		{
			if (!animation.IsWalking())
			{
				animation.StartWalking();
			}
			nav.stoppingDistance = stopDistanceFromPlayer;
			nav.destination = playerWingman.position;
			transform.LookAt(playerWingman);
			lastPositionKnown = false;
			searching = false;
			totalTimePausing = 0.0f;
			totalTimeSearching = 0.0f;

			Vector3 distanceVector = playerWingman.position - transform.position;
			if (distanceVector.magnitude <= stopDistanceFromPlayer)
			{
				if (animation.IsWalking())
				{
					animation.StopWalking();
				}
			}
		}
		else if (!detection.IsPlayInRangeAndVisable && !lastPositionKnown) //Lost sight of player
		{
			nav.stoppingDistance = DEFAULT_STOP;
			lastKnownPlayerPosition = playerWingman.position;
			lastPositionKnown = true;
			searching = false;
			totalTimePausing = 0.0f;
			totalTimeSearching = 0.0f;
		}
		else if (!detection.IsPlayInRangeAndVisable && lastPositionKnown) //Investigating last player position
		{
			if (searching) //Searching around the last known position
			{
				if (totalTimeSearching <= 0.0f)
				{
					if (!animation.IsWalking())
					{
						animation.StartWalking();
					}
					float yPos = transform.position.y;
					Vector3 currentPos = lastKnownPlayerPosition + (Random.insideUnitSphere * 5);
					currentPos = new Vector3(currentPos.x, yPos, currentPos.z);
					nav.stoppingDistance = DEFAULT_STOP;
					nav.destination = currentPos;
				}

				if (animation.IsWalking())
				{
					animation.StopWalking();
				}
				totalTimeSearching += Time.deltaTime;
				if (totalTimeSearching >= timeSearching)
				{
					totalTimeSearching = 0.0f;
				}
			}
			else if (!searching)
			{
				if (!animation.IsWalking())
				{
					animation.StartWalking();
				}
				Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
				if (distanceVector.magnitude <= stopZone && !searching)
				{
					if (animation.IsWalking())
					{
						animation.StopWalking();
					}
					totalTimePausing += Time.deltaTime;
					if (totalTimePausing >= pauseWaitTime)
					{
						totalTimePausing = 0.0f;
						totalTimeSearching = 0.0f;
						searching = true;
					}
				}
				nav.stoppingDistance = DEFAULT_STOP;
				nav.destination = lastKnownPlayerPosition;
			}
		}
	}

	public void Pursuing()
	{
		if (!detection.IsPlayInRangeAndVisable)
		{
			if (searching)
			{
				if (!animation.IsWalking())
				{
					animation.StartWalking();
				}
				nav.speed = pursueSpeed;
				nav.stoppingDistance = DEFAULT_STOP;
				nav.destination = lastKnownPlayerPosition;

				Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
				if (distanceVector.magnitude <= stopZone)
				{
					if (animation.IsWalking())
					{
						animation.StopWalking();
					}
					totalTimeSearching += Time.deltaTime;
					if (totalTimeSearching >= timeSearching)
					{
						searching = false;
						totalTimeSearching = 0.0f;
						lastKnownPlayerPosition = Vector3.zero;
					}
				}
			}
			else
			{
				OnPatrol();
			}
		}
		else
		{
			if (!animation.IsWalking())
			{
				animation.StartWalking();
			}
			searching = true;
			totalTimeSearching = 0.0f;
			lastKnownPlayerPosition = playerWingman.position;
			transform.LookAt(playerWingman);

			nav.speed = pursueSpeed;
			nav.stoppingDistance = stopDistanceFromPlayer;
			nav.destination = lastKnownPlayerPosition;

			Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
			if (distanceVector.magnitude <= stopDistanceFromPlayer)
			{
				if (animation.IsWalking())
				{
					animation.StopWalking();
				}
			}
		}
	}

	public void Aware()
	{
		nav.stoppingDistance = DEFAULT_STOP;
		if (!detection.IsPlayInRangeAndVisable)
		{
			OnPatrol();
		}
		else
		{
			PlayerDetected();
			transform.LookAt(playerWingman);
		}
	}

	public void Patrolling()
	{
		nav.stoppingDistance = DEFAULT_STOP;
		if (!detection.IsPlayInRangeAndVisable)
		{
			OnPatrol();
		}
		else
		{
			PlayerDetected();
		}
	}

	public void PlayerDetected()
	{
		animation.StopWalking();
		nav.Stop();
	}

	private void FollowPath()
	{
		if (!animation.IsWalking())
		{
			animation.StartWalking();
		}
		nav.speed = patrolSpeed;
		if (waypoints[currentWaypoint] != null)
		{
			Vector3 wayPointVector = waypoints[currentWaypoint].position;
			Vector3 distanceVector = wayPointVector - transform.position;

			if (distanceVector.magnitude <= stopZone)
			{
				if (animation.IsWalking())
				{
					animation.StopWalking();
				}
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

	private void OnPatrol()
	{
		distractionPos = DistractionManager.Instance.CheckForDistractions(transform.position);

		if (distractionPos == new Vector3(-1.0f, -1.0f, -1.0f))
		{
			checkingDistraction = false;
			nav.stoppingDistance = DEFAULT_STOP;
			FollowPath();
		}
		else if (distractionPos != new Vector3(-1.0f, -1.0f, -1.0f) && !checkingDistraction)
		{
			checkingDistraction = true;
			totalTimePausing = 0.0f;
		}
		else if (checkingDistraction)
		{
			nav.destination = distractionPos;
		}
	}
}




//Debug.LogError(distractionPos.x + ", " + distractionPos.y + ", " + distractionPos.z);