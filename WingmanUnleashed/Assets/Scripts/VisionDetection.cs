using UnityEngine;
using System.Collections;

public class VisionDetection : MonoBehaviour {
	private GameObject wingMan;
	private bool playerInRange;
	public bool IsPlayInRangeAndVisable = false;
	public float SuitDectectionRate = 0.025f;
	public float WingSuitDectectionRate = 0.1f;
	public Vector3 npcOffset = new Vector3(0, 1.5f, 0);
	public Vector3 playerOffset = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		wingMan = GameObject.Find ("Wingman");
		playerInRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position + npcOffset, ((wingMan.transform.position) - (transform.position + npcOffset) + playerOffset), Color.cyan);
		if (playerInRange)
		{
			var layerMask = 1 << 8;
			RaycastHit hit;
			print("Player is in view range");
			if (Physics.Raycast(transform.position + npcOffset, ((wingMan.transform.position) - (transform.position + npcOffset)), out hit, 1000000, layerMask))
			{
				//print (hit.collider.gameObject.transform.parent.gameObject.name);
				print(hit.collider.gameObject.name);
				//Player p = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Player>();

				if (hit.transform.gameObject == wingMan)
				{
					// In Range and visible
					IsPlayInRangeAndVisable = true;
					if (wingMan.GetComponent<Outfit>().outfitName == "wingsuit") wingMan.GetComponent<Player>().increaseDetection(WingSuitDectectionRate);
					else wingMan.GetComponent<Player>().increaseDetection(SuitDectectionRate);
					print("VISIBLE");
					print(wingMan.GetComponent<Player>().getDetectionLevel());
				}
				else
				{
					IsPlayInRangeAndVisable = false;
				}
			}
			else
			{
				IsPlayInRangeAndVisable = false;
			}
		}
		else
		{
			IsPlayInRangeAndVisable = false;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.Equals(wingMan))
		{
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.Equals(wingMan))
		{
			playerInRange = false;
		}
	}

	public bool getPlayerInRange()
	{
		return playerInRange;
	}

}
