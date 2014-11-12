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
    public GameObject throwPoint;
    public GameObject throwDirection;
    public float ThrowingForce = 1000.0f;
    public bool insideBuilding;
    public bool constrictRange = false;
    public float rangeConstriction = 10.0f;
    public Vector3 kickPosition = new Vector3(1449.3f, 244.4f, 546.0f);


	private BouncerAnimator Characteranimation;
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
    private bool carryingWingman;

	void Start()
	{
		Characteranimation = GetComponent<BouncerAnimator>();
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
        carryingWingman = false;
	}

	void LateUpdate()
	{
		float currentDetectionLevel = player.getDetectionLevel();
        bool withingRange = true;
        if (constrictRange)
        {
            Vector3 bouncerToPlayerRange = transform.position - playerWingman.position;
            withingRange = bouncerToPlayerRange.magnitude <= rangeConstriction;
        }

        if (withingRange)
        {
            if (carryingWingman)
            {
                Carrying();
            }
            else if (currentDetectionLevel <= maxStopToLookLevel || playerWingman.gameObject.GetComponent<Outfit>().outfitName == "bouncer")
            {
                Patrolling();
            }
            else if (currentDetectionLevel > maxStopToLookLevel && currentDetectionLevel <= maxAwareLevel)
            {
                Aware();
            }
            else if (currentDetectionLevel > maxAwareLevel && currentDetectionLevel < maxPursueLevel)
            {
                Pursuing();
            }
            else if (currentDetectionLevel >= maxPursueLevel)
            {
                FullPursuit();
            }
        }
        else
        {
            Patrolling();
        }

	}

    public void Carrying()
    {
        Vector3 distanceVector = throwPoint.transform.position - transform.position;
        if (distanceVector.magnitude <= stopDistanceFromPlayer)
        {
            carryingWingman = false;
            Characteranimation.FinishThrow();
            playerWingman.Rotate(transform.forward, -90);
            Camera.main.GetComponent<Camera_ThirdPerson>().rotationSmoothing = 0.8f;
            //playerWingman.eulerAngles = new Vector3(playerWingman.eulerAngles.x, playerWingman.eulerAngles.y, 0.0f);
            Vector3 direction = throwDirection.transform.position - throwPoint.transform.position;
            playerWingman.gameObject.GetComponent<Rigidbody>().AddForce((direction + Vector3.up) * ThrowingForce);
            playerWingman.gameObject.GetComponent<Controller_ThirdPerson>().flightmodeOn();
            
        }
        else
        {
            playerWingman.position = gameObject.transform.position + new Vector3(0f, 1.9f, 0f) - playerWingman.up;
            playerWingman.forward = transform.forward;
            playerWingman.Rotate(transform.forward, 90);
            //playerWingman.eulerAngles = new Vector3(playerWingman.eulerAngles.x, playerWingman.eulerAngles.y, 90.0f);
            nav.destination = throwPoint.transform.position;
            Characteranimation.fixWeight();
        }
    }

	public void FullPursuit()
	{
		nav.speed = pursueSpeed;
		nav.stoppingDistance = DEFAULT_STOP;

		if (detection.IsPlayInRangeAndVisable) //Pursuing player
		{
			if (!Characteranimation.IsWalking())
			{
				Characteranimation.StartWalking();
			}
			nav.stoppingDistance = stopDistanceFromPlayer;
			nav.destination = playerWingman.position;
			transform.LookAt(playerWingman);
			transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
			lastPositionKnown = false;
			searching = false;
			totalTimePausing = 0.0f;
			totalTimeSearching = 0.0f;

			Vector3 distanceVector = playerWingman.position - transform.position;
			if (distanceVector.magnitude <= stopDistanceFromPlayer)
			{
                //if (Characteranimation.IsWalking())
                //{
                //    Characteranimation.StopWalking();
                //}
                if (insideBuilding)
                {
                    KickOutDoor();
                }
                else
                {
                    Grab();
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
					if (!Characteranimation.IsWalking())
					{
						Characteranimation.StartWalking();
					}
					float yPos = transform.position.y;
					Vector3 currentPos = lastKnownPlayerPosition + (Random.insideUnitSphere * 5);
					currentPos = new Vector3(currentPos.x, yPos, currentPos.z);
					nav.stoppingDistance = DEFAULT_STOP;
					nav.destination = currentPos;
				}

				if (Characteranimation.IsWalking())
				{
					Characteranimation.StopWalking();
				}
				totalTimeSearching += Time.deltaTime;
				if (totalTimeSearching >= timeSearching)
				{
					totalTimeSearching = 0.0f;
				}
			}
			else if (!searching)
			{
				if (!Characteranimation.IsWalking())
				{
					Characteranimation.StartWalking();
				}
				Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
				if (distanceVector.magnitude <= stopZone && !searching)
				{
					if (Characteranimation.IsWalking())
					{
						Characteranimation.StopWalking();
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

    private void KickOutDoor()
    {
        playerWingman.position = kickPosition;
    }

    private void Grab()
    {
        carryingWingman = true;
        Camera.main.GetComponent<Camera_ThirdPerson>().rotationSmoothing = 0.2f;
        Characteranimation.StartThrow();
        playerWingman.position = gameObject.transform.position + new Vector3(0f, 1.8f, 0f);
        //playerWingman.Rotate(transform.forward, 90);
        nav.destination = throwPoint.transform.position;
    }

	public void Pursuing()
	{
		if (!detection.IsPlayInRangeAndVisable)
		{
			if (searching)
			{
				if (!Characteranimation.IsWalking())
				{
					Characteranimation.StartWalking();
				}
				nav.speed = pursueSpeed;
				nav.stoppingDistance = DEFAULT_STOP;
				nav.destination = lastKnownPlayerPosition;

				Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
				if (distanceVector.magnitude <= stopZone)
				{
					if (Characteranimation.IsWalking())
					{
						Characteranimation.StopWalking();
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
			if (!Characteranimation.IsWalking())
			{
				Characteranimation.StartWalking();
			}
			searching = true;
			totalTimeSearching = 0.0f;
			lastKnownPlayerPosition = playerWingman.position;
			transform.LookAt(playerWingman);
			transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

			nav.speed = pursueSpeed;
			nav.stoppingDistance = stopDistanceFromPlayer;
			nav.destination = lastKnownPlayerPosition;

			Vector3 distanceVector = lastKnownPlayerPosition - transform.position;
			if (distanceVector.magnitude <= stopDistanceFromPlayer)
			{
				if (Characteranimation.IsWalking())
				{
					Characteranimation.StopWalking();
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
			transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
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
		Characteranimation.StopWalking();
		nav.Stop();
	}

	private void FollowPath()
	{
		if (!Characteranimation.IsWalking())
		{
			Characteranimation.StartWalking();
		}
		nav.speed = patrolSpeed;
		if (waypoints[currentWaypoint] != null)
		{
			Vector3 wayPointVector = waypoints[currentWaypoint].position;
			Vector3 distanceVector = wayPointVector - transform.position;

			if (distanceVector.magnitude <= stopZone)
			{
				if (Characteranimation.IsWalking())
				{
					Characteranimation.StopWalking();
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