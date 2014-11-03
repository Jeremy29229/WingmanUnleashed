using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveDisplayScript : MonoBehaviour {

    public GameObject ObjectiveBar;
    private MouseManager mouseManager;
    bool on;
	// Use this for initialization
	void Start () {
        mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
        gameObject.GetComponentInParent<Canvas>().enabled = false;
        AddObjective("Start", "Talk to the client."); //Test: Add demo objective
        AddObjective("Test", "Do the thing."); //Test: Add demo objective

        //RemoveObjective("Start");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.O))
        {
            if (on)
            {
                gameObject.GetComponent<Canvas>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Canvas>().enabled = true;
            }
            mouseManager.IsMouseLocked = on;
            on = !on;
            

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (on)
            {
                gameObject.GetComponent<Canvas>().enabled = false;
                mouseManager.IsMouseLocked = on;
                on = !on;
            }
        }
	}


    public void AddObjective(string name, string objectiveText)
    {
        var id = (GameObject)Instantiate(ObjectiveBar);
        id.name = name;
        id.transform.FindChild("Text").GetComponent<Text>().text = objectiveText;
        id.transform.SetParent(GameObject.Find("ScrollBounds").transform, false);
    }

    public void RemoveObjective(string name)
    {
        var id = GameObject.Find("ScrollBounds").transform.FindChild(name);
        Destroy(id.gameObject);
    }


}
