using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveDisplayScript : MonoBehaviour
{
	public GameObject ObjectiveBar;
    private GameObject ScrollBounds;
	private MouseManager mouseManager;
	bool on;

	// Use this for initialization
	void Start()
	{
        ScrollBounds = GameObject.Find("ScrollBounds");
		mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
		gameObject.GetComponentInParent<Canvas>().enabled = false;
		AddObjective("Start", "Talk to Bruce Ludolf McGinnis."); //Test: Add demo objective

	}

	// Update is called once per frame
	void Update()
	{
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
		id.transform.SetParent(ScrollBounds.transform, false);
	}

	public void RemoveObjective(string name)
	{
		var id = ScrollBounds.transform.FindChild(name);
		Destroy(id.gameObject);
	}

    public void RemoveAllObjectives()
    {
        while (ScrollBounds.transform.childCount > 0)
        {
            Destroy(ScrollBounds.transform.GetChild(0).gameObject);
        }
    }
}
