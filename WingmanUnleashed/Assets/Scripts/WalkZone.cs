using UnityEngine;
using System.Collections;

public class WalkZone : MonoBehaviour {
    public AudioClip music;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider c)
    {
        GameObject.Find("Wingman").GetComponent<Controller_ThirdPerson>().flightmodeOff();
        GameObject.Find("MusicManager").GetComponent<Music>().PlayMusic(music);
    }

    void OnTriggerExit(Collider c)
    {
        GameObject.Find("Wingman").GetComponent<Controller_ThirdPerson>().flightmodeOn();
        GameObject.Find("MusicManager").GetComponent<Music>().PlayDefault();
    }
}
