using UnityEngine;
using System.Collections;

public class ChaosMeteor : MonoBehaviour
{
    // VARIABLES
    private GameObject meteorObj;
    private float travelTime;
    private float burnDamage;
    private float damage;
    private bool canRoll = false;
    private Transform Meteor1;
    private Transform Meteor2;

    // Audio
    private AudioSource audioSource;
    public AudioClip ChaosMeteorSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Meteor1 = transform.GetChild(0);
        Meteor2 = transform.GetChild(1);
    }

    // Use this for initialization
    void Start ()
    {
        audioSource.PlayOneShot(ChaosMeteorSound,1);
        StartCoroutine(destroy(travelTime));
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(canRoll)
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
    }

    // Stop ghost walk after duration is up and return material back to original.
    IEnumerator destroy(float durtaion)
    {
        yield return new WaitForSeconds(1.3f);
        Meteor1.gameObject.SetActive(false);
        Meteor2.gameObject.SetActive(true);
        canRoll = true;

        yield return new WaitForSeconds(durtaion);
        //Meteor2.GetChild(0).GetComponent<Renderer>().enabled = false;
        Meteor2.GetChild(2).SetParent(null);

        Die();
    }

    // Destory object and return attributes to original state
    public void Die()
    {
        Destroy(gameObject);
    }

    // Accessors and Mutators
    public GameObject MeteorObj
    {
        get { return meteorObj; }
        set { meteorObj = value; }
    }
    public float TravelTime
    {
        get { return travelTime; }
        set { travelTime = value; }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public float BurnDamage
    {
        get { return burnDamage; }
        set { burnDamage = value; }
    }
}
