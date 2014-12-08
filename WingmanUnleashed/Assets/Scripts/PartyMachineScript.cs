using UnityEngine;
using System.Collections;

public class PartyMachineScript : MonoBehaviour {

    public float EffectDuration = 10;
    public float IntermissionBetweenEffects = 1;
    public ParticleSystem[] PartyEffects;
    private ParticleSystem currentEffect;
	// Use this for initialization
	void Start () 
    {
        if (PartyEffects != null && PartyEffects.Length > 0)
        {
            for (int i = 0; i < PartyEffects.Length; i++)
            {
                PartyEffects[i].Stop();
            }
            StartRandomEffect();
        }

	}

    private void StartRandomEffect()
    {
        bool foundRandomEffect = false;
        while (!foundRandomEffect)
        {
            int index = Random.Range(0, PartyEffects.Length);
			if (PartyEffects[index] != currentEffect || PartyEffects.Length == 1)
            {
                foundRandomEffect = true;
                currentEffect = PartyEffects[index];
                currentEffect.Play();
            }
        }
        StartCoroutine("WaitForEffectDuration");
    }
    
    

    private IEnumerator WaitForEffectDuration()
    {
        yield return new WaitForSeconds(EffectDuration);
        if (currentEffect != null)
        {
            currentEffect.Stop();
        }
        yield return new WaitForSeconds(IntermissionBetweenEffects);
        StartRandomEffect();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
