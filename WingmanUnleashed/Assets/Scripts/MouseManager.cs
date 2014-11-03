using UnityEngine;

public class MouseManager : MonoBehaviour
{
	public bool wasMouseLocked = false;
	public bool IsMouseLocked = true;

	void Update()
	{
		if (wasMouseLocked != IsMouseLocked)
		{
			Screen.lockCursor = IsMouseLocked;
			wasMouseLocked = IsMouseLocked;
		}
	}
}
