using UnityEngine;
using System.Collections;

public class Distraction : MonoBehaviour {
    public float radius;
    public float time;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
	}
}
