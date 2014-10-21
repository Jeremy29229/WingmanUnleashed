using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Billboard : MonoBehaviour
{
	Camera cam;

	void Start()
	{
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	void Update()
	{
		//transform.rotation = new Quaternion(cam.transform.rotation.x, transform.rotation.y, cam.transform.rotation.z, transform.rotation.w);
		transform.rotation = cam.transform.rotation;
	}
}
