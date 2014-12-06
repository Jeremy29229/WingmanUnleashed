using UnityEngine;
using System.Collections;

public class Smashable : MonoBehaviour
{
	void OnCollisionEnter(Collision c)
	{
		//was 8, 5
		DistractionManager.Instance.AddDistraction(10.0f, 10.0f, gameObject.transform.position);
		SoundManager.Instance.PlaySoundAt("Shatter", gameObject.transform.position);
	}
}
