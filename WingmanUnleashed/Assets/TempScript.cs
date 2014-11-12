using UnityEngine;

public class TempScript : MonoBehaviour
{
	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == GameObject.Find("Wingman"))
		{
			var objectiveHolder = GameObject.Find("ScrollBounds");
			GameObject neededObjective = null;

			foreach (Transform transform in objectiveHolder.transform)
			{
				if (transform.gameObject.name == "Start1")
				{
					neededObjective = transform.gameObject;
				}
			}

			if (neededObjective == null)
			{
				GameObject.Find("ObjectiveCanvas").GetComponent<ObjectiveDisplayScript>().AddObjective("Start1", "Talk to <color=purple>Bruce Ludolf McGinnis</color>.");
			}
		}
	}
}
