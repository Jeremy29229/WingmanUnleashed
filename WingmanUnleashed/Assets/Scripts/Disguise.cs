using UnityEngine;
using System.Collections;

public class Disguise : MonoBehaviour , IInteractable
{
	public string PlayerObjectName = "Wingman";
	private Outfit outfit;
    public string DisguiseName;
	
	void Start()
	{
		outfit = GameObject.Find(PlayerObjectName).GetComponent<Outfit>();
	}
	
	void IInteractable.InteractWith()
	{
        string playersOutfit = outfit.outfitName;
        gameObject.renderer.material.mainTexture = Resources.Load<Texture2D>(playersOutfit+"Disguise");
        outfit.changeTo(DisguiseName);
        DisguiseName = playersOutfit;
	}
}