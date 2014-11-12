using UnityEngine;
using System.Collections;

public class RooftopJankMagic : MonoBehaviour {
    public GameObject Client;
    public GameObject Target;
    public GameObject Wingman;
    public GameObject ClientWaypoint;
    public GameObject TargetWaypoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Client.GetComponent<Commandable>().stopFollowing();
        Target.GetComponent<Commandable>().stopFollowing();
        Client.GetComponent<Commandable>().teleport(ClientWaypoint.transform.position);
        Client.GetComponent<Commandable>().teleport(TargetWaypoint.transform.position);

    }

    void OnTriggerExit(Collider other)
    {

    }

}
