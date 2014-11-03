using UnityEngine;
using System.Collections;

public class Smashable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        DistractionManager.Instance.AddDistraction(8.0f, 5.0f, gameObject.transform.position);
        SoundManager.Instance.PlaySoundAt("Shatter", gameObject.transform.position);
    }
}
