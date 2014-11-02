using UnityEngine;
using System.Collections.Generic;

public class DistractionManager : MonoBehaviour {

    List<GameObject> distractions;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    foreach(GameObject d in distractions)
        {
            if (d.transform.GetComponent<Distraction>().time <= 0.0f)
            {
                distractions.Remove(d);
                break;
            }
        }
	}

    void AddDistraction(float radius, float time, Vector3 position)
    {
        GameObject temp = (GameObject)Object.Instantiate(GameObject.Find("Distraction"));
        temp.transform.GetComponent<Distraction>().radius = radius;
        temp.transform.GetComponent<Distraction>().time = time;
        temp.transform.position = position;
        temp.transform.GetComponent<SphereCollider>().radius = radius;
        distractions.Add(temp);
    }

    Vector3 CheckForDistractions(Vector3 position)
    {
        foreach (GameObject d in distractions)
        {
            if (Vector3.Distance(d.transform.position,position) <= d.transform.GetComponent<Distraction>().radius)
            {
                return d.transform.position;
            }
        }
        return new Vector3(-1,-1,-1);
    }
}
