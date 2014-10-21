using UnityEngine;
using System.Collections;

public class WalkZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider c)
    {
        GameObject.Find("Wingman").GetComponent<Controller_ThirdPerson>().flightmodeOff();
    }

    void OnTriggerExit(Collider c)
    {
        GameObject.Find("Wingman").GetComponent<Controller_ThirdPerson>().flightmodeOn();
    }
}
