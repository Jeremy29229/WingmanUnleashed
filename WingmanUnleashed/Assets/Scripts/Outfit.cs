using UnityEngine;
using System.Collections;

public class Outfit : MonoBehaviour
{
	public string outfitName;
	private GameObject player;
    public float supression;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Wingman");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void changeTo(string newOutfit,float baseSupression)
	{
        player.transform.GetChild(1).renderer.material.mainTexture = Resources.Load<Texture2D>(newOutfit + "Outfit");
        supression = baseSupression;
		outfitName = newOutfit;
	}

	IEnumerator sleep(string newOutfit)
	{
		yield return new WaitForSeconds(0.1f);
		player.transform.GetChild(1).renderer.material.mainTexture = Resources.Load<Texture2D>(newOutfit + "Outfit");
	}

}
