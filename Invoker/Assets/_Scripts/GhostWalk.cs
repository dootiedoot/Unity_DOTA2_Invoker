using UnityEngine;
using System.Collections;

public class GhostWalk : MonoBehaviour
{
    // VARAIBLES
	// Attributes
    private float duration = 1;
    private float selfSlowMultiplier;
    private float enemySlowMultiplier;

    // Components
    private NavMeshAgent navMeshAgent;
    private SphereCollider sphereCol;

    // Visuals
    private Renderer render;
    private Material originalMaterial; 
    private Material invisMaterial;

	// Audio
	private AudioClip ghostWalkSound;
	private AudioSource audioSource;

	// Assign references
	void Awake () 
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
        sphereCol = gameObject.AddComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<Renderer>();
	}

	// Use this for initialization
	void Start ()
    {

        // Adjust movement speed of gameobject
        navMeshAgent.speed *= (1 + selfSlowMultiplier);

        // Sphere Collider attributes
        sphereCol.radius = 4.0f;
        sphereCol.isTrigger = true;

		// set visuals
        originalMaterial = render.sharedMaterial;
        render.sharedMaterial = invisMaterial;
		// Play audio
		audioSource.PlayOneShot(ghostWalkSound, 1);

		// Start IEnumerator to destory object after x seconds.
		StartCoroutine(destroy(duration));
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            print("Entered: " + other.name);
            other.GetComponent<NavMeshAgent>().speed *= (1 + enemySlowMultiplier);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<NavMeshAgent>().speed /= (1 + enemySlowMultiplier);
       
        }
    }

	// Stop ghost walk after duration is up and return material back to original.
	IEnumerator destroy(float Duration)
	{
		yield return new WaitForSeconds(Duration);
       
        Die();
	}

    // Destory object and return attributes to original state
    public void Die()
    {
        navMeshAgent.speed /= (1 + selfSlowMultiplier);
        Destroy(GetComponent<SphereCollider>());
        render.sharedMaterial = originalMaterial;
        Destroy(this);
    }

    // Accessors and Mutators
    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public float SelfSlow
    {
        get { return selfSlowMultiplier; }
        set { selfSlowMultiplier = value; }
    }
    public float EnemySlow
    {
        get { return enemySlowMultiplier; }
        set { enemySlowMultiplier = value; }
    }
	public AudioClip GhostWalkSound
	{
		get { return ghostWalkSound; }
		set { ghostWalkSound = value; }
	}
    public Material InvisMaterial
    {
        get { return invisMaterial; }
        set { invisMaterial = value; }
    }
}
