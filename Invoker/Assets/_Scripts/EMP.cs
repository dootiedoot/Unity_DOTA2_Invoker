using UnityEngine;
using System.Collections;

public class EMP : MonoBehaviour
{
    public SphereCollider sphere;
    public float radius;

    // VARIABLES
    // Attributes
    private float manaBurned;
    private float dmgPerBurnPercent;
    private float manaGainPerBurnPercent;

    // Audio
    public AudioClip empSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
    // Use this for initialization
    void Start ()
    {
        sphere.radius = radius;
        audioSource.PlayOneShot(empSound, 1);
        StartCoroutine(destroy(2.9f));
    }

    // Stop Cold Snap after duration is up and return material back to original.
    IEnumerator destroy(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Emp();
        Die();
    }

    void Emp()
    {
        RaycastHit hit;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(transform.position, radius, Vector3.down, out hit, 10))
        {
            sphere.transform.position = transform.position;
        }
    }

    public void Die()
    {
        Destroy(gameObject, 1);
    }

    // Accessors and Mutators
    public float ManaBurned
    {
        get { return manaBurned; }
        set { manaBurned = value; }
    }
    public float DmgPerBurnPercent
    {
        get { return dmgPerBurnPercent; }
        set { dmgPerBurnPercent = value; }
    }
    public float ManaGainPerBurnPercent
    {
        get { return manaGainPerBurnPercent; }
        set { manaGainPerBurnPercent = value; }
    }
}
