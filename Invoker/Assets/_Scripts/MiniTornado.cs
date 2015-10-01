using UnityEngine;
using System.Collections;

public class MiniTornado : MonoBehaviour
{
    // Attributes
    public float liftHeight;
    private float liftDuration;
    private float damage;
    private GameObject target;
    private string targetTag;

    // Animation
    private Animator animator;

    // Visuals
    public Transform liftPoint;
    private ParticleSystem particle;
    public GameObject particleObject;

    // Audio
    private AudioSource audioSource;
    public AudioClip tornadoLiftSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        particle = particleObject.GetComponent<ParticleSystem>();
    }

    // Use this for initialization
    void Start () 
    {
        targetTag = target.tag;
        target.tag = "Untagged";

        // Visuals
        particle.Play();
        target.transform.SetParent(transform);
        target.transform.position = target.transform.position + Vector3.up * liftHeight;

        // Audio
        audioSource.PlayOneShot(tornadoLiftSound, .5f);

        // Start IEnumerator to destory object after x seconds.
        StartCoroutine(destroy(liftDuration));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 360 * Time.deltaTime, 0);
        particleObject.transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    // Stop ghost walk after duration is up and return material back to original.
    IEnumerator destroy(float Duration)
    {
        yield return new WaitForSeconds(Duration-0.2f);
        particle.Stop();
        //animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.2f);
        Die();
    }

    // Destory object and return attributes to original state
    public void Die()
    {
        // Restore attributes and visuals
        target.tag = targetTag;
        target.transform.position = target.transform.position + Vector3.down * liftHeight;
        target.GetComponent<Enemy>().AdjustHP(-damage);
        target.transform.SetParent(null);

        Destroy(gameObject);
    }
    // Accessors and Mutators
    public float LiftDuration
    {
        get { return liftDuration; }
        set { liftDuration = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }
}
