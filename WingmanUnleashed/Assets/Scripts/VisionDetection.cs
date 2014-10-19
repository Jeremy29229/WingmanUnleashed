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
		Debug.DrawRay(transform.position+ new Vector3(0,1.5f,0), ((wingMan.transform.position ) - transform.position), Color.cyan);
		if(playerInRange)
		{
            var layerMask = 1 << 8;
			RaycastHit hit;
			print("Player is in view range");
			if(Physics.Raycast(transform.position+ new Vector3(0,1.5f,0), ((wingMan.transform.position ) - transform.position), out hit, 1000000,layerMask))
			{
				//print (hit.collider.gameObject.transform.parent.gameObject.name);
				print(hit.collider.gameObject.name);
				//Player p = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Player>();

				if(hit.transform.gameObject == wingMan)
				{
					// In Range and visible
                    if (wingMan.GetComponent<Outfit>().outfitName=="wingsuit") wingMan.GetComponent<Player>().increaseDetection(0.1f);
                    else wingMan.GetComponent<Player>().increaseDetection(0.025f);
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

    public bool getPlayerInRange()
    {
        return playerInRange;
    }

}
