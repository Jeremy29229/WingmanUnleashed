using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    bool end = false;

	// Use this for initialization
	void Start () 
    {
        gameObject.GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
    public void HideGameOver()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }
    public void ShowGameOver(string message="The End")
    {
        Show(message);
    }
    public void ShowGameOverWin(string message = "You Win")
    {
        Show(message);
    }
    public void ShowGameOVerLose(string message = "You Lose")
    {
        if (!end)
        {
            GameObject.Find("Wingman").GetComponent<Rigidbody>().AddForce(new Vector3(1000.0f, 1000.0f, -1000.0f));
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
            Show(message);
            end = true;
        }
    }
    private void Show(string message)
    {
        GameObject.Find("GameOverMessage").GetComponent<Text>().text = message;
        gameObject.GetComponent<Canvas>().enabled = true;
    }
}
