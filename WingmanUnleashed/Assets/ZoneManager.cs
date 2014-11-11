using UnityEngine;

public class ZoneManager : MonoBehaviour
{
	public string ZoneName = "";
	public bool EnableZoneFading = false;
	public float ZoneFadeTime = 10.0f;
	public bool EnabledZoneAtStart = false;
	public string PlayerObjectName = "Wingman";
	
	private GameObject player;

	private bool hasCountDownStarted = false;
	private float currentTime = 0.0f;
	private GameObject[] zoneObjects;

	void Start()
	{
		zoneObjects = GameObject.FindGameObjectsWithTag("Zone" + ZoneName);

		player = GameObject.Find(PlayerObjectName);

		SetupAllZoneObject(EnabledZoneAtStart);
	}

	void Update()
	{
		if (hasCountDownStarted)
		{
			currentTime += Time.deltaTime;
			if (currentTime >= ZoneFadeTime)
			{
				SetupAllZoneObject(false);
				hasCountDownStarted = false;
				currentTime = 0.0f;
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == player)
		{
			hasCountDownStarted = false;
			currentTime = 0.0f;

			SetupAllZoneObject(true);
		}
	}

	void OnTriggerExit(Collider c)
	{
		print(c.gameObject.name);
		if (c.gameObject == player)
		{
			if (EnableZoneFading)
			{
				hasCountDownStarted = true;
				currentTime = 0.0f;
			}
			else
			{
				SetupAllZoneObject(false);
			}
		}
	}

	private void SetupAllZoneObject(bool isActive)
	{
		foreach (var obj in zoneObjects)
		{
			print(isActive + " " + obj);
			obj.transform.parent.gameObject.SetActive(isActive);
		}
	}
}
