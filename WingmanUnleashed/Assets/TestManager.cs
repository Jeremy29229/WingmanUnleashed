using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GameObject.Find("BouncerAngry1").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerAngry2").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerAngry3").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerAngry4").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerHappy1").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerHappy2").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerHappy3").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerHappy4").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerStoic1").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerStoic2").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerStoic3").GetComponent<CharacterAnimator>().StartDancingSamba();
        GameObject.Find("BouncerStoic4").GetComponent<CharacterAnimator>().StartDancingSamba();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
}
