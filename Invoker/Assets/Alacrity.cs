using UnityEngine;
using System.Collections;

public class Alacrity : MonoBehaviour
{
    // ATTRIBUTES
    private float duration;
    private float attackSpeed;
    private float bonusDamage;

    private PlayerController _player;

    // Audio
    public AudioClip alacritySound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        _player = transform.parent.GetComponent<PlayerController>();
        _player.AttackSpeed /= attackSpeed;
        _player.Damage += bonusDamage;

        // Audio
        audioSource.PlayOneShot(alacritySound, 1f);

        // Start IEnumerator to destory object after x seconds.
        StartCoroutine(destroy(duration));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(120 * Time.deltaTime, 120 * Time.deltaTime, 120 * Time.deltaTime);
    }

    // Stop Cold Snap after duration is up and return material back to original.
    IEnumerator destroy(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Die();
    }

    public void Die()
    {
        _player.AttackSpeed *= attackSpeed;
        _player.Damage -= bonusDamage;
        Destroy(gameObject);
    }

    // Accessors and Mutators
    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
    public float BonusDamage
    {
        get { return bonusDamage; }
        set { bonusDamage = value; }
    }
}
