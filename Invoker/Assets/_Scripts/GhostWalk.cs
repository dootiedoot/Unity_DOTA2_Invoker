using UnityEngine;
using System.Collections;

public class GhostWalk : MonoBehaviour
{
    // VARAIBLES
	// Attributes
    private float duration = 1;
    private float selfSlowMultiplier;
    private float enemySlowMultiplier;

    // Scripts
    private NavMeshAgent navMeshAgent;
    private SphereCollider sphereCol;

    // Visuals
    private Material myMaterial;
    private Color enemyColor;

	// Audio
	private AudioClip ghostWalkSound;
	private AudioSource audioSource;

	// Assign references
	void Awake () 
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
        sphereCol = gameObject.AddComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
		myMaterial = GetComponent<Renderer>().sharedMaterial;
	}

	// Use this for initialization
	void Start ()
    {

        // Adjust movement speed of gameobject
        navMeshAgent.speed *= (1 + selfSlowMultiplier);

        // Sphere Collider attributes
        sphereCol.radius = 4.0f;
        sphereCol.isTrigger = true;

		// Play audio
		audioSource.PlayOneShot(ghostWalkSound, 1);
		// set visuals
		SetInvisibleMaterial(myMaterial, 0.4f, 3);

		// Start IEnumerator to destory object after x seconds.
		StartCoroutine(destroy(duration));
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            print("Entered: " + other.name);
            other.GetComponent<NavMeshAgent>().speed *= (1 + enemySlowMultiplier);
            enemyColor = other.GetComponent<Renderer>().sharedMaterial.color;
            SetColor(other.GetComponent<Renderer>().sharedMaterial, Color.blue);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<NavMeshAgent>().speed /= (1 + enemySlowMultiplier);
            SetColor(other.GetComponent<Renderer>().sharedMaterial, enemyColor);
        }
    }

    public void SetColor(Material material, Color value)
    {
        Color color = material.color;
        color = value;
        material.color = color;
    }

    // Changes rendering mode and alpha of the given material
    public void SetInvisibleMaterial(Material material, float value, int mode) 
	{
		Color color = material.color;
		color.a = value;
		material.color = color;
		myMaterial.SetInt("_Mode", mode);
		myMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		myMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		myMaterial.SetInt("_ZWrite", 0);
		myMaterial.DisableKeyword("_ALPHATEST_ON");
		myMaterial.EnableKeyword("_ALPHABLEND_ON");
		myMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		myMaterial.renderQueue = 3000;
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
        SetInvisibleMaterial(myMaterial, 1, 0);
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
}
