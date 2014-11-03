using UnityEngine;
using System.Collections;

public class animationTestScript : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
        //animator.Play("walking");
        //animator.Play("walking", 0, 50);
	}

    int frame = 0;
	// Update is called once per frame
	void Update () 
    {
        frame++;
        if (frame == 200)
        {
            animator.SetBool("IsDancingSamba", true);
        }
        if (frame == 400)
        {
            animator.SetBool("IsDancingSamba", false);
        }
        if (frame == 600)
        {
            //animator.SetBool("IsDancing", false);
            animator.SetTrigger("Jump");
        }
        if (frame == 800)
        {
            //animator.SetBool("IsDancing", false);
            animator.SetBool("IsWalking", true);    
            //animator.SetTrigger("Jump");
        }
        if (frame == 1000)
        {
            animator.SetBool("IsStrafingRight", true);
        }
        if (frame == 1200)
        {
            animator.SetBool("IsStrafingRight", false);
        }
        if (frame == 1400)
        {
            //animator.SetBool("IsDancing", false);
            animator.SetBool("IsWalking", false);
            //animator.SetTrigger("Jump");
        }

        //Animator animator = GetComponent<Animator>();
        //AnimatorStateInfo info =  animator.GetCurrentAnimatorStateInfo(0);
        
        //if (info.nameHash == Animator.StringToHash("walking") || info.IsName("walking"))
        //{
        //    print("WALKING");
        //}
        //else if (info.nameHash == Animator.StringToHash("idle") || info.IsName("idle"))
        //{
        //    print("idle");
        //}
        //else if (info.nameHash == Animator.StringToHash("idle Repeat") || info.IsName("idle Repeat"))
        //{
        //    print("idle Repeat");
        //}
        //else if (info.nameHash == Animator.StringToHash("left_turn") || info.IsName("left_turn"))
        //{
        //    print("left_turn");
        //}
        //else if (info.nameHash == Animator.StringToHash("right_turn") || info.IsName("right_turn"))
        //{
        //    print("right_turn");
        //}
	}
}
