using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour
{
    private float duration;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(stun(duration));
    }

    IEnumerator stun(float Duration)
    {

        yield return new WaitForSeconds(Duration);
        Destroy(this);
    }

    // Accessors and Mutators
    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }
}
