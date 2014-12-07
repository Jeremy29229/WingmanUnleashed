using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour
{

	private float interest;
	private Image interestBar;
    private GameObject loveEffect;
    public GameObject clientObject;
    private Client client;

	// Use this for initialization
	void OnEnable()
	{
		interestBar = (Image)GameObject.Find("InterestBar").GetComponent(typeof(Image));
        client = clientObject.GetComponentInChildren<Client>();
        loveEffect = gameObject.transform.parent.transform.FindChild("Loooooooooooove").gameObject;
        loveEffect.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		interestBar.fillAmount = interest;
	}

	public void increaseInterest(float amount)
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundAt("SmallSuccess", gameObject.transform.position);
		interest += amount;
		InterestBoundsCheck();
        if (interest == 1.0f && client.GetConfidence() == 1.0f)
        {
            TurnOnLoveEffect();
            client.TurnOnLoveEffect();

        }
	}

    public void TurnOnLoveEffect()
    {
        loveEffect.SetActive(true);
    }

	public void decreaseInterest(float amount)
	{
		interest -= amount;
		InterestBoundsCheck();
	}

	public float GetInterest()
	{
		return interest;
	}

	private void InterestBoundsCheck()
	{
		if (interest < 0.0f)
		{
			interest = 0.0f;
		}
		else if (interest > 1.0f)
		{
			interest = 1.0f;
		}
	}
}
