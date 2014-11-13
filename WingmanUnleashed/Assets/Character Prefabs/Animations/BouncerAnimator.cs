using UnityEngine;
using System.Collections;

public class BouncerAnimator : CharacterAnimator
{
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    public void StartThrow()
    {
        CheckAnimator();
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("RaiseArms");
    }
    public void FinishThrow()
    {
        animator.SetTrigger("Throw");
        StartCoroutine("DelayDisableArms");
    }

    private IEnumerator DelayDisableArms()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetLayerWeight(1, 0);
    }

    public void fixWeight()
    {
        animator.SetLayerWeight(0, 0.2f);

    }
}
