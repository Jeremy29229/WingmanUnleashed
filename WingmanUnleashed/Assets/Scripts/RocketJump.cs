using UnityEngine;

public class RocketJump : Equipment
{
    GameObject wingman;
    public RocketJump()
    {
        wingman = GameObject.Find("Wingman");
    }
    public override void Use()
    {
        wingman.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 1600.5f, 0.0f) + wingman.GetComponent<Controller_ThirdPerson>().player.transform.forward * 500.5f);
    }
}