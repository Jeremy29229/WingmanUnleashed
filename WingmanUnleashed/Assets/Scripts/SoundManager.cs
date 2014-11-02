using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
	public AudioClip Collect;
	public AudioClip SmallSuccess;
	public AudioClip Caught;

	// Use this for initialization
	void Start()
	{
        Instance = this;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PlaySoundAt(string soundName, Vector3 position)
	{
		AudioClip theSound = null;
		if (soundName == "cashGrab") theSound = Collect;
		if (soundName == "SmallSuccess") theSound = SmallSuccess;
		if (soundName == "RecordScratch") theSound = Caught;
		AudioSource.PlayClipAtPoint(theSound, position);
	}

	public void PlaySound(string soundName)
	{
		AudioClip theSound = null;
		if (soundName == "cashGrab") theSound = Collect;
		if (soundName == "SmallSuccess") theSound = SmallSuccess;
		if (soundName == "RecordScratch") theSound = Caught;
		AudioSource.PlayClipAtPoint(theSound, new Vector3(0, 0, 0));
	}
}
