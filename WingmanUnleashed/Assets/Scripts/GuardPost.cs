using UnityEngine;
using System.Collections;

public class GuardPost : MonoBehaviour {

    BouncerAI bouncer;
    public GameObject lookTarget;
    public GameObject post;
    float degreesPerFrame = 0.1f;

	// Use this for initialization
	void Start () {
        bouncer = gameObject.GetComponent<BouncerAI>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public bool AtPost()
    {
        bool result = false;

        float dist = Vector3.Distance(gameObject.transform.position,post.transform.position);
        result = (dist < 1);

        return result;
    }

    public bool AlreadyFacing() {
        bool result = false;

        Vector3 direction = gameObject.transform.forward;
        float dist = Vector3.Angle(direction, lookTarget.transform.position - gameObject.transform.position);
        result = (dist < 10);

        return result;
    }

    public void lookTowards() {
        Quaternion direction = gameObject.transform.rotation;
        Quaternion target = Quaternion.LookRotation(lookTarget.transform.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Slerp(direction, target, degreesPerFrame);
    }
}
