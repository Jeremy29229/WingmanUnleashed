﻿using UnityEngine;
using System.Collections;

public class VisionDetection : MonoBehaviour
{
	private GameObject wingMan;
	public string disguiseGroup;
	public bool playerInRange = false;
	public bool IsPlayInRangeAndVisable = false;
	public float BaseDetectionRate = 0.1f;
	public Vector3 npcOffset = new Vector3(0, 1.5f, 0);
	public Vector3 playerOffset = new Vector3(0, 1.5f, 0);
	public bool CanIncreaseDetection;

	// Use this for initialization
	void Start()
	{
		wingMan = GameObject.Find("Wingman");
		playerInRange = false;
		CanIncreaseDetection = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (wingMan.GetComponent<Outfit>().outfitName == disguiseGroup)
		{
			CanIncreaseDetection = ((BaseDetectionRate - wingMan.GetComponent<Outfit>().supression) > 0f);
		}
		else
		{
			CanIncreaseDetection = ((BaseDetectionRate - wingMan.GetComponent<Outfit>().supression * 0.5f) > 0f);
		}

		Debug.DrawRay(transform.position + npcOffset, ((wingMan.transform.position) - (transform.position + npcOffset) + playerOffset), Color.cyan);
		if (playerInRange)
		{
			var layerMask = 1 << 8;
			RaycastHit hit;
			if (Physics.Raycast(transform.position + npcOffset, ((wingMan.transform.position) - (transform.position + npcOffset) + playerOffset), out hit, 1000000, layerMask))
			{
				//print (hit.collider.gameObject.transform.parent.gameObject.name);
				//print(hit.collider.gameObject.name);
				//Player p = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Player>();

				if (hit.transform.gameObject == wingMan)
				{
					// In Range and visible
					if (!IsPlayInRangeAndVisable)
					{
						wingMan.GetComponent<Player>().numDetectors++;
					}

					
					IsPlayInRangeAndVisable = true;
					if (wingMan.GetComponent<Outfit>().outfitName == disguiseGroup)
					{
						wingMan.GetComponent<Player>().increaseDetection(BaseDetectionRate - wingMan.GetComponent<Outfit>().supression);
					}
					else
					{
						wingMan.GetComponent<Player>().increaseDetection(BaseDetectionRate - wingMan.GetComponent<Outfit>().supression * 0.5f);
					}

					//	print(wingMan.GetComponent<Player>().getDetectionLevel());
				}
				else
				{
					if (IsPlayInRangeAndVisable)
					{
						wingMan.GetComponent<Player>().numDetectors--;
					}
					IsPlayInRangeAndVisable = false;
				}
			}
			else
			{
				if (IsPlayInRangeAndVisable)
				{
					wingMan.GetComponent<Player>().numDetectors--;
				}
				IsPlayInRangeAndVisable = false;
			}
		}
		else
		{
			if (IsPlayInRangeAndVisable)
			{
				wingMan.GetComponent<Player>().numDetectors--;
			}
			IsPlayInRangeAndVisable = false;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.Equals(wingMan))
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.Equals(wingMan))
		{
			playerInRange = false;
		}
	}

	public bool getPlayerInRange()
	{
		return playerInRange;
	}

}
