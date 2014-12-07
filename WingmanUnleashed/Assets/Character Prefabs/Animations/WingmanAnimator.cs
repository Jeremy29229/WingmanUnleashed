using UnityEngine;
using System.Collections;

public class WingmanAnimator : MonoBehaviour 
{
    //IsWalking
    //IsWalkingDrunk
    //IsDancingSamba
    //IsDancingGangnam
    //Jump
    //TurnLeft
    //TurnRight
    //IsStrafingLeft
    //IsStrafingRight
    private Animator animator;
	void Start () 
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            throw new MissingComponentException("Wingman " + gameObject.name + " has no animation component, cannot be animated by CharacterAnimation script.");
        }
	}
    private void CheckAnimator()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
	void Update () 
    {
        if (animator == null)
        {
            animator = GetComponent <Animator>();
        }
	}
    public void SetSpeed(int spd)
    {
        animator.speed = spd;
    }
    /// <summary>
    /// The character will jump once.
    /// </summary>
    public void Jump()
    {
        CheckAnimator();
        animator.SetTrigger("Jump");
    }
    /// <summary>
    /// The character will start walking until StopWalking is called.
    /// The character can strafe right or left while walking.
    /// The character can turn left or right while walking.
    /// </summary>
    public void StartWalking()
    {
        CheckAnimator();
        animator.SetBool("IsWalking", true);
    }
    /// <summary>
    /// The character will stop walking.
    /// </summary>
    public void StopWalking()
    {
        animator.SetBool("IsWalking", false);
    }
    /// <summary>
    /// The character will start walking drunkenly, and will not stop until StopWalkingDrunk is called.
    /// The character can strafe right or left while walking drunkenly.
    /// The character can turn left or right while walking drunkenly.
    /// </summary>
    public void StartWalkingDrunk()
    {
        CheckAnimator();
        animator.SetBool("IsWalkingDrunk", true);
    }
    /// <summary>
    /// The character will stop walking drunkenly.
    /// </summary>
    public void StopWalkingDrunk()
    {
        animator.SetBool("IsWalkingDrunk", false);
    }
    /// <summary>
    /// The character will start samba dancing and will dance until StopDancingSamba is called.
    /// </summary>
    public void StartDancingSamba()
    {
        CheckAnimator();
        animator.SetBool("IsDancingSamba", true);
    }
    /// <summary>
    /// The character will stop samba dancing.
    /// </summary>
    public void StopDancingSamba()
    {
        animator.SetBool("IsDancingSamba", false);
    }
    /// <summary>
    /// The character will start dancing gangnam style and will dance until StopDancingGangnam is called.
    /// </summary>
    public void StartDancingGangnam()
    {
        CheckAnimator();
        animator.SetBool("IsDancingGangnam", true);
    }
    /// <summary>
    /// The character will stop dancing gangnam style.
    /// </summary>
    public void StopDancingGangnam()
    {
        animator.SetBool("IsDancingGangnam", false);
    }
    /// <summary>
    /// The character will start strafing right, and will continue strafing right until StopStrafingRight is called.
    /// The character can start strafing right while idling or while walking.
    /// </summary>
    public void StartStrafingRight()
    {
        CheckAnimator();
        animator.SetBool("IsStrafingRight", true);
    }
    /// <summary>
    /// The character will stop strafing right. If they were walking previously and StopWalking was not called, they will go back to walking normally.
    /// </summary>
    public void StopStrafingRight()
    {
        animator.SetBool("IsStrafingRight", false);
    }
    /// <summary>
    /// The character will start strafing left, and will continue strafing left until StopStrafingLeft is called.
    /// The character can start strafing left while idling or while walking.
    /// </summary>
    public void StartStrafingLeft()
    {
        CheckAnimator();
        animator.SetBool("IsStrafingLeft", true);

    }
    /// <summary>
    /// The character will stop strafing left. If they were walking previously and StopWalking wa snot called, they will go back to walking normally.
    /// </summary>
    public void StopStrafingLeft()
    {
        animator.SetBool("IsStrafingLeft", false);

    }
    /// <summary>
    /// The character will turn right once.
    /// The character can turn right while idling, or while walking.
    /// </summary>
    public void TurnRight()
    {
        CheckAnimator();
        animator.SetTrigger("TurnRight");
    }
    /// <summary>
    /// The character will turn left once.
    /// The character can turn left while idling, or while walking.
    /// </summary>
    public void TurnLeft()
    {
        CheckAnimator();
        animator.SetTrigger("TurnLeft");
    }
    /// <summary>
    /// The character will enter the TPose
    /// </summary>
    public void EnterTPose()
    {
        ResetToIdle();
        animator.SetBool("IsInTPose", true);
    }
    /// <summary>
    /// Get out of TPose
    /// </summary>
    public void ExitTPose()
    {
        animator.SetBool("IsInTPose", false);
    }

    public void StartCrouching()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
        animator.SetBool("IsCrouching", true);
    }
    public void StopCrouching()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetBool("IsCrouching", false);
    }

    /// <summary>
    /// The character will stand in an idle state. This does NOT need to be called to have the character idle, as that is already its default state if no other animation has been called.
    /// Use this method instead as a fail safe to end any lingering animations that may have been started but not stopped.
    /// </summary>
    public void ResetToIdle()
    {
        CheckAnimator();
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsWalkingDrunk", false);
        animator.SetBool("IsDancingSamba", false);
        animator.SetBool("IsDancingGangnam", false);
        animator.SetBool("IsStrafingRight", false);
        animator.SetBool("IsStrafingLeft", false);
        animator.SetBool("IsInTPose", false);
    }

    /// <summary>
    /// Gets whether or not the character currently walking normally.
    /// </summary>
    /// <returns>Whether or not the character is currently walking normally.</returns>
    public bool IsWalking()
    {
        return animator.GetBool("IsWalking");
    }
    /// <summary>
    /// Gets whether or not the character is currently walking drunkenly.
    /// </summary>
    /// <returns>Whether or not the character is currently walking drunkenly.</returns>
    public bool IsWalkingDrunk()
    {
        return animator.GetBool("IsWalkingDrunk");
    }
    /// <summary>
    /// Gets whether or not the character is currently crouched.
    /// </summary>
    /// <returns>Whether or not the character is currently crouched.</returns>
    public bool IsCrouching()
    {
        return animator.GetBool("IsCrouching");
    }
    /// <summary>
    /// Gets whether or not the character is currently samba dancings.
    /// </summary>
    /// <returns>Whether or not the character is currently samba dancing.</returns>
    public bool IsDancingSamba()
    {
        return animator.GetBool("IsDancingSamba");
    }
    /// <summary>
    /// Gets whether or not the character is currently gangnam dancing.
    /// </summary>
    /// <returns>Whether or not the character is currently gangnam dancing.</returns>
    public bool IsDancingGangnam()
    {
        return animator.GetBool("IsDancingGangnam");
    }
    /// <summary>
    /// Gets whether or not the character is currently strafing to the right.
    /// </summary>
    /// <returns>Whether or not the character is currently strafing to the right.</returns>
    public bool IsStrafingRight()
    {
        return animator.GetBool("IsStrafingRight");
    }
    /// <summary>
    /// Gets whether or not the character is currently strafing to the left.
    /// </summary>
    /// <returns>Whether or not the character is currently strafing to the left.</returns>
    public bool IsStrafingLeft()
    {
        return animator.GetBool("IsStrafingLeft");
    }

    public bool IsInTPose()
    {
        return animator.GetBool("IsInTPose");
    }

    public bool IsIdle()
    {
        return (!IsWalking() && !IsWalkingDrunk() && !IsDancingGangnam() && !IsDancingSamba() && !IsStrafingLeft() && !IsStrafingRight() && !IsInTPose());
    }
}
