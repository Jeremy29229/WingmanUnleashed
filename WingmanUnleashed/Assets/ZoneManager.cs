using UnityEngine;

public class ZoneManager : MonoBehaviour
{
	public string ZoneName = "";
	public bool EnableZoneFading = false;
	public float ZoneFadeTime = 10.0f;
	public bool EnabledZoneAtStart = false;
	public string PlayerObjectName = "Wingman";
	public GameObject LaserLocatator;
	private ParticleSystem[] originalParticles;
	private ParticleSystem[] flyingParticles;
	
	private GameObject player;

	private bool hasCountDownStarted = false;
	private float currentTime = 0.0f;
	private GameObject[] zoneObjects;
	private PartyMachineScript pms;

	void Start()
	{
		zoneObjects = GameObject.FindGameObjectsWithTag("Zone" + ZoneName);

		player = GameObject.Find(PlayerObjectName);

		SetupAllZoneObject(EnabledZoneAtStart);

		if (LaserLocatator != null)
		{
			pms = LaserLocatator.transform.parent.GetComponent<PartyMachineScript>();
			originalParticles = pms.PartyEffects;
			flyingParticles = new ParticleSystem[1];
			flyingParticles[0] = LaserLocatator.particleSystem;
			pms.PartyEffects = flyingParticles;
			LaserLocatator.particleSystem.startSize = 40.0f;
		}
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
			if (LaserLocatator != null)
			{
				pms.PartyEffects = originalParticles;
				LaserLocatator.particleSystem.startSize = 2.0f;
			}

			hasCountDownStarted = false;
			currentTime = 0.0f;

			SetupAllZoneObject(true);
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (c.gameObject == player)
		{
			if (LaserLocatator != null)
			{
				pms.PartyEffects = flyingParticles;
				LaserLocatator.particleSystem.startSize = 40.0f;
			}

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
			obj.transform.parent.gameObject.SetActive(isActive);
		}
	}
}
