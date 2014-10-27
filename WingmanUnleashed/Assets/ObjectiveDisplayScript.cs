using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveDisplayScript : MonoBehaviour {

    public GameObject ObjectiveBar;
	// Use this for initialization
	void Start () {
        gameObject.GetComponentInParent<Canvas>().enabled = false;
        AddObjective("Start", "Talk to the client.");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            gameObject.GetComponent<Canvas>().enabled = false;
        }
	}

    public void AddObjective(string name, string objectiveText)
    {
        var id = (GameObject)Instantiate(ObjectiveBar);
        id.name = name + "Display";
        id.transform.FindChild("Text").GetComponent<Text>().text = objectiveText;
        id.transform.SetParent(GameObject.Find("ScrollBounds").transform, false);
    }

    public void RemoveObjective(string name)
    {
        var id = GameObject.Find("ObjectiveBar").transform.FindChild(name + "Display");
        Destroy(id);
    }


}
