using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
    public AudioSource player;
    public AudioClip wingman;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(AudioClip song)
    {
        player.Stop();
        player.clip = song;
        player.Play();
    }

    public void PlayDefault()
    {
        player.Stop();
        player.clip = wingman;
        player.Play();
    }
}
