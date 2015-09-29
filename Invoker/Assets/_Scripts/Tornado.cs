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
    public GameObject miniTornado;
    private CapsuleCollider capsuleCol;

    // Visuals
    private GameObject particleObject;

    // Audio
    public AudioClip tornadoTravelSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        capsuleCol = GetComponent<CapsuleCollider>();
        particleObject = transform.FindChild("Particles").gameObject;
    }

	// Use this for initialization
	void Start ()
    {
        // Audio
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Stun _stun = other.gameObject.AddComponent<Stun>();
            _stun.Duration = liftDuration;
            GameObject tornado = Instantiate(miniTornado, other.transform.position, Quaternion.identity) as GameObject;
            MiniTornado _tornado = tornado.GetComponent<MiniTornado>();
            _tornado.LiftDuration = liftDuration;
            _tornado.Target = other.gameObject;
        }
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
}
