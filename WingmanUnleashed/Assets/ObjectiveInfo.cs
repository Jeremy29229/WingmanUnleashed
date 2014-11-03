using UnityEngine;
using System.Collections;

public class ObjectiveInfo : MonoBehaviour {

    private bool complete;
	// Use this for initialization
	void Start () {
        complete = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool IsComplete()
    {
        return complete;
    }

    public void SetCompleted()
    {
        complete = true;
    }
}
