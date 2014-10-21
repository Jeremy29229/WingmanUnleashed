using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour {

    private float interest;
    private Image interestBar;

	// Use this for initialization
	void Start () {
        interestBar = (Image)GameObject.Find("InterestBar").GetComponent(typeof(Image));

	}
	
	// Update is called once per frame
	void Update () {
        interestBar.fillAmount = interest;
	}

    public void increaseInterest(float amount)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySoundAt("SmallSuccess", gameObject.transform.position);
        interest += amount;
    }

    public void decreaseInterest(float amount)
    {
        interest -= amount;
    }
}
