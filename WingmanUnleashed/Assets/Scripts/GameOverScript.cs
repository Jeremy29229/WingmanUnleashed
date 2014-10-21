using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

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
        //GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
        Show(message);
    }
    private void Show(string message)
    {
        GameObject.Find("GameOverMessage").GetComponent<Text>().text = message;
        gameObject.GetComponent<Canvas>().enabled = true;
    }
}
