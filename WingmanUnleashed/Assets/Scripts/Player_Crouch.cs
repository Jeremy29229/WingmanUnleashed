using UnityEngine;
using System.Collections;

public class Player_Crouch : MonoBehaviour
{
    public static Player_Crouch Instance;

    public float crouchAmount = 0.5f;
    public float transitionInSeconds = 0.3f;

    private float crouchVelocity;
    private float currentCrouchAmount;
    private float originalColliderHeight;
    private float crouchColliderHeight;
    private float previousColliderHeight;
    private CapsuleCollider capCollider;
    private Transform cameraLookAt;

    private float CROUCHING = 1;
    private float STANDING = 0;

    void Awake()
    {
        Instance = this;
        currentCrouchAmount = 1.0f;
        capCollider = GetComponent<CapsuleCollider>() as CapsuleCollider;
        originalColliderHeight = capCollider.height;
        crouchColliderHeight = originalColliderHeight * crouchAmount;
    }

    void Update()
    {
        cameraLookAt = Camera_ThirdPerson.Instance.TargetObjectLookAt;
        previousColliderHeight = capCollider.height;

        capCollider.height = Mathf.Lerp(originalColliderHeight, crouchColliderHeight, currentCrouchAmount);

        if (Motor_ThirdPerson.Instance.isCrouching)
        {
            currentCrouchAmount = Mathf.SmoothDamp(currentCrouchAmount, CROUCHING, ref crouchVelocity, transitionInSeconds);
        }
        else
        {
            currentCrouchAmount = Mathf.SmoothDamp(currentCrouchAmount, STANDING, ref crouchVelocity, transitionInSeconds);
        }

        float capsuleHeight = (capCollider.height - previousColliderHeight) * 0.5f;
        cameraLookAt.position += new Vector3(0.0f, capsuleHeight, 0.0f);
        capCollider.center += new Vector3(0.0f, capsuleHeight, 0.0f);
    }


}
