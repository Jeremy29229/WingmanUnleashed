using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private Canvas HUD;
	private float detectionLevel;

	// Use this for initialization
	void Start () {
		detectionLevel = 0f;
        //HUD = (Canvas)GameObject.Find("HUD").GetType;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void increaseDetection()
	{
		detectionLevel += 0.01f;
	}

	public float getDetectionLevel()
	{
		return detectionLevel;
	}
}
