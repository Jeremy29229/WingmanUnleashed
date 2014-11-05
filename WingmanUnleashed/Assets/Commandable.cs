using UnityEngine;
using System.Collections;

public class Commandable : MonoBehaviour {
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private Vector3 currentDestination;
    private bool destinationReached;
    private GameObject leader;
    private CharacterAnimator animator;
    bool following;
    bool willReturn;
	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        startPosition = gameObject.transform.position;
        animator = gameObject.GetComponent<CharacterAnimator>();
        destinationReached = true;
        following = false;
        willReturn = false;
        //followCharacter(GameObject.Find("Wingman")); //Test: follow Wingman
        //sendToLocation(new Vector3(1471.9f, 241.59f, 554.71f)); //Test: go to target
        //visitLocation(new Vector3(1471.9f, 241.59f, 554.71f)); //Test: go to target and return

	}
	
	// Update is called once per frame
	void Update () {
        if (following)
        {
            sendToLocation(leader.transform.position);
        }
        if (!destinationReached)
        {
            if (Vector3.Distance(gameObject.transform.position, currentDestination) < 1)
            {
                animator.ResetToIdle();
                destinationReached = true;
                agent.Stop();
            }
            if (currentDestination == startPosition)
            {
                willReturn = false;
            } 
        }
        else if (willReturn)
        {
            sendToStartPosition();
        }
       
	}

    public void sendToLocation(Vector3 location)
    {
        animator.StartWalking();
        currentDestination = location;
        agent.SetDestination(currentDestination);
        destinationReached = false;
    }

    public void sendToStartPosition()
    {
        sendToLocation(startPosition);
    }

    public void visitLocation(Vector3 location)
    {
        sendToLocation(location);
        willReturn = true;
    }

    public void followCharacter(GameObject character)
    {
        leader = character;
        following = true;
    }

    public void stopFollowing()
    {
        following = false;
    }

    //Not yet implemented
    //public void retrieveObject(GameObject thing)
    //{
    //    sendToLocation(thing.transform.position);

    //}
}
