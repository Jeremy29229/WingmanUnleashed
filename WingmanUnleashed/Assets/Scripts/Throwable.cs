using UnityEngine;

public class Throwable : MonoBehaviour
{
	GameObject item;
	GameObject wingman;
    void Start()
    {
        wingman = GameObject.Find("Wingman");
    }

    void Update()
    {
        
    }

	public void Use()
	{
        item = (GameObject)Object.Instantiate(gameObject);
        if (item.GetComponent<Rigidbody>() == null)
        {
            item.AddComponent<Rigidbody>();
        }
        item.GetComponent<Rigidbody>().mass = 1.0f;
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<Rigidbody>().drag = 0.5f;
        item.GetComponent<Rigidbody>().angularDrag = 0.2f;
        if (item.GetComponent<BoxCollider>() == null)
        {
            item.AddComponent<BoxCollider>();
        }
        item.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
        if (item.GetComponent<Smashable>() == null)
        {
            item.AddComponent<Smashable>();
        }
		item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		item.transform.position = wingman.transform.position + new Vector3(0, 1, 0) + wingman.GetComponent<Controller_ThirdPerson>().player.transform.forward;
		item.GetComponent<Rigidbody>().AddForce(new Vector3(0, 250.0f, 0) + wingman.GetComponent<Controller_ThirdPerson>().player.transform.forward * 300.0f);
	}
}