using UnityEngine;
using System.Collections;

public class VisionDetection : MonoBehaviour {
	private GameObject wingMan;
	private bool playerInRange;

	// Use this for initialization
	void Start () {
		wingMan = GameObject.Find ("Wingman");
		playerInRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position+ new Vector3(0,1.5f,0), ((wingMan.transform.position ) - transform.position), Color.cyan,10.0f, true);
		if(playerInRange)
		{
			RaycastHit hit;
			print("Player is in view range");
			if(Physics.Raycast(transform.position+ new Vector3(0,1.5f,0), ((wingMan.transform.position ) - transform.position), out hit, 1000000))
			{
				//print (hit.collider.gameObject.transform.parent.gameObject.name);
				//Player p = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Player>();

				if(hit.transform.gameObject == wingMan)
				{
					// In Range and visible
					wingMan.GetComponent<Player>().increaseDetection();
					print("VISIBLE");
					print (wingMan.GetComponent<Player>().getDetectionLevel());
				}
			}
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


}
