using UnityEngine;
using System.Collections;

public class Tornado : MonoBehaviour
{
    // VARIABLES

    // Attributes
    private float travelTime;
    private float bonusDamage;
    private float liftDuration;

    // Components
    private GameObject particleObject;

    // Audio
    private AudioSource audioSource;
    private AudioClip tornadoTravelSound;
    private AudioClip tornadoLiftSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        particleObject = transform.GetChild(0).gameObject;
    }

	// Use this for initialization
	void Start ()
    {
        audioSource.PlayOneShot(tornadoTravelSound, .5f);

        // Start IEnumerator to destory object after x seconds.
        StartCoroutine(destroy(travelTime));
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * 12 * Time.deltaTime);
        particleObject.transform.Rotate(0, 0, 360 * Time.deltaTime); 
    }

    // Stop Cold Snap after duration is up and return material back to original.
    IEnumerator destroy(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    // Accessors and Mutators
    public float TravelTime
    {
        get { return travelTime; }
        set { travelTime = value; }
    }
    public float BonusDamage
    {
        get { return bonusDamage; }
        set { bonusDamage = value; }
    }
    public float LiftDuration
    {
        get { return liftDuration; }
        set { liftDuration = value; }
    }
    public AudioClip TornadoTravelSound
    {
        get { return tornadoTravelSound; }
        set { tornadoTravelSound = value; }
    }
    public AudioClip TornadoLiftSound
    {
        get { return tornadoLiftSound; }
        set { tornadoLiftSound = value; }
    }
}
