using UnityEngine;
using System.Collections;

public class Outfit : MonoBehaviour
{
	public string outfitName;
	private GameObject player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Wingman");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void changeTo(string newOutfit)
	{
		if (outfitName == "wingsuit")
		{
			GameObject oldModel = GameObject.Find("wingedBody");
			if (oldModel == null) oldModel = GameObject.Find("wingedBody(Clone)");
			Quaternion orient = oldModel.transform.localRotation;
			Destroy(oldModel);
			GameObject newModel = (GameObject)Instantiate(Resources.Load<GameObject>("basicBody"));
			newModel.transform.parent = player.transform;
			newModel.transform.localPosition = new Vector3(0, 0, 0);
			newModel.transform.localRotation = orient;
			StartCoroutine(sleep(newOutfit));
		}
		else if (newOutfit == "wingsuit")
		{
			GameObject oldModel = GameObject.Find("basicBody");
			if (oldModel == null) oldModel = GameObject.Find("basicBody(Clone)");
			Quaternion orient = oldModel.transform.localRotation;
			Destroy(oldModel);
			GameObject newModel = (GameObject)Instantiate(Resources.Load<GameObject>("wingedBody"));
			newModel.transform.parent = player.transform;
			newModel.transform.localPosition = new Vector3(0, 0, 0);
			newModel.transform.localRotation = orient;
			StartCoroutine(sleep(newOutfit));
		}
		else
		{
			player.transform.GetChild(1).renderer.material.mainTexture = Resources.Load<Texture2D>(newOutfit + "Outfit");
		}
		outfitName = newOutfit;
	}

	IEnumerator sleep(string newOutfit)
	{
		yield return new WaitForSeconds(0.1f);
		player.transform.GetChild(1).renderer.material.mainTexture = Resources.Load<Texture2D>(newOutfit + "Outfit");
	}

}
