using UnityEngine;
using System.Collections;

public class BouncerAI : MonoBehaviour
{
	public float patrolSpeed = 2.0f;
	public float pursueSpeed = 4.0f;
	public float patrolPointWaitTime = 1.0f;
    public float pauseWaitTime = 1.0f;
    public float timeSearching = 3.0f;
	public Transform[] waypoints;

	private VisionDetection detection;
    private DistractionManager distractions;
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

	void Awake()
	{
		detection = GetComponentInChildren<VisionDetection>();
        GameObject distractionManager = GameObject.Find("DistractionManager");
        distractions = distractionManager.GetComponent<DistractionManager>();
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

		if (currentDetectionLevel >= 0.0f && currentDetectionLevel <= maxStopToLookLevel)
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
        if (detection.IsPlayInRangeAndVisable)
        {
            nav.destination = playerWingman.position;
            lastPositionKnown = false;
            searching = false;
            totalTimePausing = 0.0f;
            totalTimeSearching = 0.0f;
        }
        else if (!detection.IsPlayInRangeAndVisable && !lastPositionKnown)
        {
            lastKnownPlayerPosition = playerWingman.position;
            lastPositionKnown = true;
            searching = false;
            totalTimePausing = 0.0f;
            totalTimeSearching = 0.0f;
        }
        else if (!detection.IsPlayInRangeAndVisable && lastPositionKnown)
        {
            Vector3 currentPos = lastKnownPlayerPosition;

            Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
            if (distanceVector.magnitude <= stopZone && !searching)
            {
                totalTimePausing += Time.deltaTime;
                if (totalTimePausing >= pauseWaitTime)
                {
                    totalTimePausing = 0.0f;
                    totalTimeSearching = 0.0f;
                    searching = true;
                }
            }
            else if (searching)
            {
                if (totalTimeSearching <= 0.0f)
                {
                    float yPos = transform.position.y;
                    currentPos = transform.position + Random.insideUnitSphere;
                    currentPos = new Vector3(currentPos.x, yPos, currentPos.z);
                }

                totalTimeSearching += Time.deltaTime;
                if (totalTimeSearching >= timeSearching)
                {
                    totalTimeSearching = 0.0f;
                }
            }

            nav.destination = currentPos;
        }
	}

	public void Pursuing()
	{
        if (!detection.IsPlayInRangeAndVisable)
		{
            if (searching)
            {
                nav.speed = pursueSpeed;
                nav.destination = lastKnownPlayerPosition;

                Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
                if (distanceVector.magnitude <= stopZone)
                {
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
            searching = true;
            totalTimeSearching = 0.0f;
            lastKnownPlayerPosition = playerWingman.position;
            transform.LookAt(playerWingman);

            nav.speed = pursueSpeed;
            nav.destination = lastKnownPlayerPosition;
        }
	}

	public void Aware()
	{
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
		nav.Stop();
	}

	private void FollowPath()
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

    private void OnPatrol()
    {
        if (!checkingDistraction)
        {
            distractionPos = distractions.CheckForDistractions(transform.position);
        }

        if (distractionPos == new Vector3(-1.0f, -1.0f, -1.0f))
        {
            FollowPath();
        }
        else if (distractionPos != new Vector3(-1.0f, -1.0f, -1.0f) && !checkingDistraction)
        {
            checkingDistraction = true;
            totalTimePausing = 0.0f;
        }
        else if(checkingDistraction)
        {
            transform.LookAt(distractionPos);
            nav.destination = distractionPos;

            Vector3 distanceVector = distractionPos - transform.position;
            if (distanceVector.magnitude <= stopZone)
            {
                totalTimePausing += Time.deltaTime;
                if (totalTimePausing >= pauseWaitTime)
                {
                    checkingDistraction = false;
                    totalTimePausing = 0.0f;
                }
            }
        }
    }
}
