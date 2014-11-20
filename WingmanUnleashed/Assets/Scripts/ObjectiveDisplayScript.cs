using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveDisplayScript : MonoBehaviour
{
	public GameObject ObjectiveBar;
	private GameObject ScrollBounds;
	private MouseManager mouseManager;
	private ConversationManager conversationManager;
	bool on;
	private GameObject player;
	private InteractionManager interactionManager;
	private Inventory inventory;

	private Camera_ThirdPerson cam;
	private Controller_ThirdPerson controller;

	// Use this for initialization
	void Start()
	{
		cam = Camera_ThirdPerson.Instance;
		controller = Controller_ThirdPerson.Instance;

		ScrollBounds = GameObject.Find("ScrollBounds");
		mouseManager = GameObject.Find("MouseManager").GetComponent<MouseManager>();
		gameObject.GetComponentInParent<Canvas>().enabled = false;
		interactionManager = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
		conversationManager = GameObject.Find("ConvoGUI").GetComponent<ConversationManager>();
		player = GameObject.Find("Wingman");
		inventory = player.GetComponent<Inventory>();
		on = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.O))
		{
			on = !on;

			if (on)
			{
				inventory.CloseInventory();
				conversationManager.Close();
				interactionManager.Hide();
				gameObject.GetComponent<Canvas>().enabled = true;
				mouseManager.IsMouseLocked = false;
				cam.IsInConversation = true;
				controller.IsInConversation = true;
			}
			else
			{
				gameObject.GetComponent<Canvas>().enabled = false;
				interactionManager.Show();
				mouseManager.IsMouseLocked = true;
				cam.IsInConversation = false;
				controller.IsInConversation = false;
			}
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

	public void Close()
	{
		gameObject.GetComponent<Canvas>().enabled = false;
		interactionManager.Show();
		mouseManager.IsMouseLocked = true;
		on = false;
	}

	
}
