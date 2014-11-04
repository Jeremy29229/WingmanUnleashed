using UnityEngine;
using System.Collections;

public class ObjectiveInfo : MonoBehaviour
{
	private bool complete;

	// Use this for initialization
	void Start()
	{
		complete = false;
	}

	public bool IsComplete()
	{
		return complete;
	}

	public void SetCompleted(bool isCompleted = true)
	{
		complete = isCompleted;
	}
}
