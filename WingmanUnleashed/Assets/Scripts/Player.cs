using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	private Canvas HUD;
	private Image detectionBar;
	private Image eye;
    public int numDetectors;
	
	private float detectionLevel;

	// Use this for initialization
	void Start () {
		detectionLevel = 0f;
		HUD = (Canvas)GameObject.Find("HUD").GetComponent(typeof(Canvas));
		detectionBar = (Image)GameObject.Find("DetectionBar").GetComponent(typeof(Image));
		eye = (Image)GameObject.Find("Eye").GetComponent(typeof(Image));
	}
	
	// Update is called once per frame
	void Update () {
		detectionLevel -= 0.01f * Time.deltaTime;
		detectionBar.fillAmount = detectionLevel;
		detectionBar.color = Color.red * (detectionLevel+ 0.2f);
		eye.color = Color.red * (detectionLevel + 0.2f);

	}

	public void increaseDetection(float amount)
	{
		detectionLevel += amount * Time.deltaTime;
		
	}

	public float getDetectionLevel()
	{
		return detectionLevel;
	}
}
