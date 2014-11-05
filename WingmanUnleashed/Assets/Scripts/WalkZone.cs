using UnityEngine;
using System.Collections;

public class WalkZone : MonoBehaviour
{
	public AudioClip music;
	GameObject wingman;
	// Use this for initialization
	void Start()
	{
		wingman = GameObject.Find("Wingman");
	}

	// Update is called once per frame
	void Update()
	{

	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject == wingman)
        {
            GameObject.Find("MusicManager").GetComponent<Music>().PlayMusic(music);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject == wingman)
        {
            GameObject.Find("MusicManager").GetComponent<Music>().PlayDefault();
        }
    }
}
