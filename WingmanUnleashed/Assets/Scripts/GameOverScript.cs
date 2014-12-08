using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour
{

	bool end = false;
	private GameObject player;
	private Camera_ThirdPerson cam;
	private Controller_ThirdPerson controller;

	//// Use this for initialization
	void Start()
	{
		gameObject.GetComponent<Canvas>().enabled = false;
		player = GameObject.Find("Wingman");
		cam = Camera_ThirdPerson.Instance;
		controller = Controller_ThirdPerson.Instance;
	}

	// Update is called once per frame
	void Update()
	{
		if ((player.transform.position.y <= 0 && player.transform.position.y > -5))
		{
			//Screen.showCursor = true;
			Screen.lockCursor = false;
			gameObject.GetComponent<Canvas>().enabled = true;
			cam.IsInConversation = true;
			controller.IsInConversation = true;

		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	//public void HideGameOver()
	//{
	//	gameObject.GetComponent<Canvas>().enabled = false;
	//}
	//public void ShowGameOver(string message="The End")
	//{
	//	Show(message);
	//}
	//public void ShowGameOverWin(string message = "You Win")
	//{
	//	Show(message);
	//}
	public void ShowGameOVerLose(string message = "You Lose")
	{
		if (!end)
		{
			//Vector2 direction = Random.insideUnitCircle;
			//GameObject.Find("Wingman").GetComponent<Rigidbody>().AddForce(new Vector3(direction.x * 1000.0f, 1000.0f, direction.y * 1000.0f));
			//GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
			//GameObject.Find("Detection").audio.Stop();
			//Show(message);
			end = true;
		}
	}

	public void Restart()
	{
		Application.LoadLevel("NotGotham");
	}

	public void Quit()
	{
#if UNITY_EDITOR
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE
		Application.Quit();
#endif
	}

	//private void Show(string message)
	//{
	//	GameObject.Find("GameOverMessage").GetComponent<Text>().text = message;
	//	gameObject.GetComponent<Canvas>().enabled = true;
	//}
}
