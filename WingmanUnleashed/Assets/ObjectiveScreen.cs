using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveScreen : MonoBehaviour {

    private Canvas objectiveScreen;
    private object[] objectiveBars;


    private ArrayList objectives;
	// Use this for initialization
	void Start () {
        //objectiveText = (Canva)GameObject.Find("CurrentObjective").GetComponent(typeof(Text));
        objectiveScreen = this.gameObject.GetComponent<Canvas>();
        objectiveBars[0] = GameObject.Find("Objective1");
        objectiveBars[1] = GameObject.Find("Objective2");
        objectiveBars[2] = GameObject.Find("Objective3");
        objectiveBars[3] = GameObject.Find("Objective4");

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            objectiveScreen.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.O))
        {
            objectiveScreen.enabled = false;
        }
	}

    public void addObjective(string objective)
    {
        objectives.Add(objective);

    }

    public void removeObjective(string objective)
    {
        foreach (string s in objectives)
        {
            if(s.Equals(objective))
            {
                objectives.Remove(s);
            }
        }
    }

    public void removeObjective(int objectiveIndex)
    {
        if (objectiveIndex < objectives.Count)
        {
            objectives.RemoveAt(objectiveIndex);
        }
    }

    private void updateObjectiveList()
    {
        int currentObjective = 0;
        foreach (string s in objectives)
        {
            if (s != null)
            {
                
            }
        }
    }
}
