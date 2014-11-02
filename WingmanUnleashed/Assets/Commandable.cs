using UnityEngine;
using System.Collections;

public class Commandable : MonoBehaviour {
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private Vector3 currentDestination;
    private bool destinationReached;
	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        startPosition = gameObject.transform.position;
        destinationReached = true;
        //sendToLocation(new Vector3(1471.9f, 241.59f, 554.71f)); //Test: go to target
	}
	
	// Update is called once per frame
	void Update () {
        if (!destinationReached)
        {
            if (Vector3.Distance(gameObject.transform.position, currentDestination) < 1)
            {
                destinationReached = true;
                agent.SetDestination(gameObject.transform.position);
            }
        }
	}

    public void sendToLocation(Vector3 location)
    {
        currentDestination = location;
        agent.SetDestination(currentDestination);
        destinationReached = false;
    }

    public void sendToStartPosition()
    {
        sendToLocation(startPosition);
    }

    //Not yet implemented
    //public void retrieveObject(GameObject thing)
    //{
    //    sendToLocation(thing.transform.position);

    //}
}
