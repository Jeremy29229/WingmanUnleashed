using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
	private Canvas HUD;
	private Image detectionBar;
	private Image eye;
	public bool wingmanVisionActive;
	public int numDetectors;
	private GameObject[] WMVLights;
	private string currentObjective;
	public AudioSource DetectionSound;
	private float detectionLevel;
	public float TimeTilUnlocked = 10.0f;
	private bool lockDetection = false;
	private float timeLocked = 0.0f;

	// Use this for initialization
	void Start()
	{
		detectionLevel = 0f;
		//HUD = (Canvas)GameObject.Find("HUD").GetComponent(typeof(Canvas));
		detectionBar = (Image)GameObject.Find("DetectionBar").GetComponent(typeof(Image));
		eye = (Image)GameObject.Find("Eye").GetComponent(typeof(Image));
		WMVLights = GameObject.FindGameObjectsWithTag("WMVLight");
	}

	// Update is called once per frame
	void Update()
	{
		//        Debug.Log("detectionLevel: " + detectionLevel);
		if (detectionLevel >= 1.0f)
		{
			if (lockDetection)
			{
				timeLocked += Time.deltaTime;
				if (timeLocked >= TimeTilUnlocked)
				{
					lockDetection = false;
					detectionLevel = 0.95f;
				}
				else
				{
                    detectionLevel = 1.0f;
				}
			}
			else
			{
				detectionLevel = 1.0f;
				lockDetection = true;
				timeLocked = 0.0f;
			}
		}

		if (detectionLevel > 0.0f && !lockDetection)
		{
			detectionLevel -= 0.01f * Time.deltaTime;
			DetectionSound.volume = detectionLevel;
			DetectionSound.pitch = detectionLevel * 2;
			if (detectionLevel <= 0.0f) DetectionSound.Stop();
		}

		if (numDetectors <= 0 && DetectionSound.isPlaying) DetectionSound.Stop();

		detectionBar.fillAmount = detectionLevel;
		detectionBar.color = Color.red * (detectionLevel + 0.2f);
		eye.color = Color.red * (detectionLevel + 0.2f);

		if (Input.GetKeyDown(KeyCode.V) && !((Controller_ThirdPerson)gameObject.GetComponent("Controller_ThirdPerson")).flightmode)
		{
			if (wingmanVisionActive)
			{
				deactivateWingmanVision();
			}
			else
			{
				activateWingmanVision();
			}
		}

	}

	public void increaseDetection(float amount)
	{
		if (!DetectionSound.isPlaying) DetectionSound.Play();
		detectionLevel += amount * Time.deltaTime;
		DetectionSound.volume = detectionLevel;
	}

	public void increaseDetectionFlat(float amount)
	{
		if (detectionLevel <= 0.0f) DetectionSound.Play();
		detectionLevel += amount;
        DetectionSound.volume = detectionLevel;
	}

    public void decreaseDetection(float amount)
    {
        if (detectionLevel > 0.0f && !lockDetection)
        {
            detectionLevel -= amount * Time.deltaTime;
            DetectionSound.volume = detectionLevel;
        }
    }

	public float getDetectionLevel()
	{
		return detectionLevel;
	}

	public void activateWingmanVision()
	{
		foreach (GameObject o in WMVLights)
		{
			Light temp = o.GetComponent<Light>();
			temp.enabled = true;
		}
		RenderSettings.ambientLight = new Color(0.28f, 0.76f, 0.75f, 1);
		RenderSettings.fogDensity = 0.08f;
		wingmanVisionActive = true;
	}

	public void deactivateWingmanVision()
	{
		foreach (GameObject o in WMVLights)
		{
			Light temp = o.GetComponent<Light>();
			temp.enabled = false;
		}
		RenderSettings.ambientLight = new Color(.12f, .12f, .12f, 1);
		RenderSettings.fogDensity = 0.002f;
		wingmanVisionActive = false;
	}
}
