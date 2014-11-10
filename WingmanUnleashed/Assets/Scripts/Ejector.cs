//using UnityEngine;

//public class Ejector : MonoBehaviour
//{
//    public float ThrowingForce = 1000.0f;

//    private GameObject wingman;
//    private bool IsWingmanInRange = false;
//    private bool WingmanGrabbed = false;
//    public bool IsInHouse = false;
//    public Vector3 kickPosition = new Vector3(1449.3f, 244.4f, 546.0f);

//    void Start()
//    {
//        wingman = GameObject.Find("Wingman");
//    }

//    void Update()
//    {
//        if (IsWingmanInRange && wingman.GetComponent<Player>().getDetectionLevel() >= 1.0f && !IsInHouse)
//        {
//            if (!WingmanGrabbed)
//            {
//                //GameObject.Find("Wingman").GetComponent<Rigidbody>().AddForce(transform.parent.forward.normalized.x * ThrowingForce, ThrowingForce, transform.parent.forward.normalized.z * ThrowingForce); 
//                //GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
//                GameObject.Find("Wingman").transform.position = gameObject.transform.position + new Vector3(0f, 1.8f, 0f);
//                GameObject.Find("Detection").audio.Stop();
//                IsWingmanInRange = false;
//                WingmanGrabbed = true;
//            }
           
//        }
//        else if (IsWingmanInRange && wingman.GetComponent<Player>().getDetectionLevel() >= 1.0f && IsInHouse)
//        {
//            GameObject.Find("Wingman").transform.position = kickPosition;
//            //GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("RecordScratch");
//            GameObject.Find("Detection").audio.Stop();
//            IsWingmanInRange = false;
//        }
//    }

//    void OnTriggerEnter(Collider c)
//    {
//        if (c.gameObject == wingman)
//        {
//            IsWingmanInRange = true;
//        }
//    }

//    void OnTriggerExit(Collider c)
//    {
//        if (c.gameObject == wingman)
//        {
//            IsWingmanInRange = false;
//        }
//    }
//}
