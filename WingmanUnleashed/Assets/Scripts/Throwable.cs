using UnityEngine;

public class Throwable : Equipment
{
	GameObject item;
	GameObject wingman;
	public Throwable(string name)
	{
		item = (GameObject)Object.Instantiate(GameObject.Find(name));
		item.AddComponent<Rigidbody>();
		item.GetComponent<Rigidbody>().mass = 1.0f;
		item.GetComponent<Rigidbody>().useGravity = true;
		item.GetComponent<Rigidbody>().isKinematic = false;
		item.GetComponent<Rigidbody>().drag = 0.5f;
		item.GetComponent<Rigidbody>().angularDrag = 0.2f;
		item.AddComponent<BoxCollider>();
		wingman = GameObject.Find("Wingman");
	}
	public override void Use()
	{
		item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		item.transform.position = wingman.transform.position + new Vector3(0, 1, 0) + wingman.GetComponent<Controller_ThirdPerson>().player.transform.forward;
		item.GetComponent<Rigidbody>().AddForce(new Vector3(0, 150.0f, 0) + wingman.GetComponent<Controller_ThirdPerson>().player.transform.forward * 200.0f);
	}
}