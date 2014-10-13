using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Conversation : MonoBehaviour
{
	public Dialog start;
	public Dialog current;

	private Canvas UI;

	public void BeginConvo()
	{
		current = start;

	}

}
