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
    private CapsuleCollider collider;
    private Transform cameraLookAt;

    private float CROUCHING = 1;
    private float STANDING = 0;

    void Awake()
    {
        Instance = this;
        currentCrouchAmount = 1.0f;
        collider = GetComponent<CapsuleCollider>() as CapsuleCollider;
        originalColliderHeight = collider.height;
        crouchColliderHeight = originalColliderHeight * crouchAmount;
    }

    void Update()
    {
        cameraLookAt = Camera_ThirdPerson.Instance.TargetObjectLookAt;
        previousColliderHeight = collider.height;

        collider.height = Mathf.Lerp(originalColliderHeight, crouchColliderHeight, currentCrouchAmount);

        if (Motor_ThirdPerson.Instance.isCrouching)
        {
            currentCrouchAmount = Mathf.SmoothDamp(currentCrouchAmount, CROUCHING, ref crouchVelocity, transitionInSeconds);

        }
        else
        {
            currentCrouchAmount = Mathf.SmoothDamp(currentCrouchAmount, STANDING, ref crouchVelocity, transitionInSeconds);
        }

        cameraLookAt.position += new Vector3(0.0f, (collider.height - previousColliderHeight) * 0.5f, 0.0f);
        collider.center += new Vector3(0.0f, (collider.height - previousColliderHeight) * 0.5f, 0.0f);
    }


}
