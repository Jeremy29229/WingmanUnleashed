using UnityEngine;
using System.Collections;

public class FlightControlRig : MonoBehaviour {
	public GameObject player;
	bool flightmode = false;
	Vector3 velocity;
	Vector3 acceleration;
	Vector3 lift;
	float windResistance=1.15f;

	// Use this for initialization
	void Start () {
		
	}

	void flightmodeOff()
	{
		acceleration = new Vector3(0.0f,0.0f,0.0f);
		lift = new Vector3(0.0f,0.0f,0.0f);
		velocity = new Vector3(0.0f,0.0f,0.0f);
		flightmode = false;
		player.transform.GetChild(0).transform.localRotation=Quaternion.identity;
		BoxCollider coll = (BoxCollider)player.GetComponent("BoxCollider");
		coll.center = new Vector3(0.0f,0.9f,0.0f);
		coll.size = new Vector3(1.5f,1.8f,0.4f);
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = true;
	}
	void flightmodeOn()
	{
		acceleration = new Vector3(0.0f,-9.81f,0.0f);
		lift = new Vector3(0.0f,0.0f,0.0f);
		velocity = new Vector3(0.0f,0.0f,0.0f);
		flightmode = true;
		player.transform.GetChild(0).transform.Rotate(new Vector3(1,0,0),90);
		BoxCollider coll = (BoxCollider)player.GetComponent("BoxCollider");
		coll.center = new Vector3(0.0f,0.0f,0.9f);
		coll.size = new Vector3(1.5f,0.4f,1.8f);
		Rigidbody rig = (Rigidbody)player.GetComponent("Rigidbody");
		rig.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(flightmode)
			{
				flightmodeOff();
			}
			else
			{
				flightmodeOn();
			}
		}

		if(flightmode)
		{
			float airspeed = velocity.magnitude;
			lift = new Vector3(0.0f,airspeed/2.0f,airspeed);
			lift = player.transform.rotation * lift;
			Vector3 netforce = acceleration+lift;
			velocity+=netforce*Time.deltaTime;
			velocity*=(1-(windResistance*Time.deltaTime));
			print (velocity);
			player.transform.position+=velocity*Time.deltaTime;

			if(Input.GetKey(KeyCode.Q))
			{
				player.transform.Rotate(new Vector3(0,0,1),1.0f,Space.Self);
				//player.transform.GetChild(0).transform.Rotate(new Vector3(0,1,0),2);
			}
			if(Input.GetKey(KeyCode.E))
			{
				player.transform.Rotate(new Vector3(0,0,1),-1.0f,Space.Self);
				//player.transform.GetChild(0).transform.Rotate(new Vector3(0,1,0),-2);
			}
			if(Input.GetKey(KeyCode.W))
			{
				player.transform.Rotate(new Vector3(1,0,0),1.0f,Space.Self);
			}
			if(Input.GetKey(KeyCode.S))
			{
				player.transform.Rotate(new Vector3(1,0,0),-1.0f,Space.Self);
			}
			if(Input.GetKey(KeyCode.A))
			{
				player.transform.Rotate(new Vector3(0,1,0),-1.0f,Space.Self);
			}
			if(Input.GetKey(KeyCode.D))
			{
				player.transform.Rotate(new Vector3(0,1,0),1.0f,Space.Self);
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			flightmodeOff();
			player.transform.position = new Vector3(1328.158f,999.9615f,165.7299f);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			flightmodeOff();
			player.transform.position = new Vector3(240.0185f,865.9557f,1163.175f);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			flightmodeOff();
			player.transform.position = new Vector3(1814.116f,719.9808f,1764.895f);
		}
	}
}
