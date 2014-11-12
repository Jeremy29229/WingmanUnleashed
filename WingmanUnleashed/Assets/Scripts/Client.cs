using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Client : MonoBehaviour
{

	public float confidence =0;
	private Image confidenceBar;

	// Use this for initialization
	void OnEnable()
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
		ConfidenceBoundsCheck();
	}

	public void decreaseConfidence(float amount)
	{
		confidence -= amount;
		ConfidenceBoundsCheck();
	}

	private void ConfidenceBoundsCheck()
	{
		if (confidence < 0.0f)
		{
			confidence = 0.0f;
		}
		else if (confidence > 1.0f)
		{
			confidence = 1.0f;
		}
	}

	public float GetConfidence()
	{
		return confidence;
	}

}
