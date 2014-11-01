using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Client : MonoBehaviour
{

	private float confidence;
	private Image confidenceBar;

	// Use this for initialization
	void Start()
	{
		confidenceBar = (Image)GameObject.Find("ConfidenceBar").GetComponent(typeof(Image));

	}

	// Update is called once per frame
	void Update()
	{
		confidenceBar.fillAmount = confidence;
	}

	public void increaseConfidence(float amount)
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundAt("SmallSuccess", gameObject.transform.position);
		confidence += amount;
	}

	public void decreaseConfidence(float amount)
	{
		confidence -= amount;
	}

}
