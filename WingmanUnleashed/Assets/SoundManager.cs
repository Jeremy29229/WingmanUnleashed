using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip Collect;
    public AudioClip SmallSuccess;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySoundAt(string soundName, Vector3 position)
    {
        AudioClip theSound = null;
        if (soundName == "cashGrab") theSound = Collect;
        if (soundName == "SmallSuccess") theSound = SmallSuccess;
        AudioSource.PlayClipAtPoint(theSound, position);
    }
}
