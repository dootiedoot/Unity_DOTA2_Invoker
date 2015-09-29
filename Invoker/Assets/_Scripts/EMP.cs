using UnityEngine;
using System.Collections;

public class EMP : MonoBehaviour
{
    // VARIABLES
    // Attributes
    public float radius;
    private GameObject affector;
    private Player _player;
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
        _player = affector.GetComponent<Player>();
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
        // Cast a overlap sphere to see if it is about to hit anything.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in hitColliders)
        {
            if(col.CompareTag("Enemy"))
            {
                print("Hit: "  + col.name);
                Enemy _enemy = col.GetComponent<Enemy>();
                _enemy.AdjustMP(-manaBurned);
                _enemy.AdjustHP(-manaBurned * dmgPerBurnPercent);
                _player.AdjustMP(manaBurned * manaGainPerBurnPercent);
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject, 1);
    }

    // Accessors and Mutators
    public GameObject Affector
    {
        get { return affector; }
        set { affector = value; }
    }
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
