using UnityEngine;
using System.Collections;

public class ColdSnap : MonoBehaviour 
{
	private float duration = 1;
	private float tickCooldown;
	[SerializeField] private float currentTickCooldown = 0;
	private float tickDamage;

	private Enemy _enemy;

    private Renderer rend;
    private Color materialColorHolder;
    private Color coldSnapColor;

    private AudioSource audioSource;

	private AudioClip coldSnapSound;
	private AudioClip coldSnapImpactSound;

	void Awake () 
	{
		_enemy = GetComponent<Enemy>();
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () 
	{
		currentTickCooldown = TickCooldown;
        materialColorHolder = rend.material.color;
        coldSnapColor = Color.blue;
        audioSource.PlayOneShot(coldSnapSound, 1);
        StartCoroutine(destroy(6));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentTickCooldown > 0)
		{
			currentTickCooldown -= Time.deltaTime;
            if (rend.material.color != materialColorHolder && currentTickCooldown <= 0.30f)
                rend.material.color = materialColorHolder;
		}
	}

	void OnDamage()
	{
		if(currentTickCooldown <= 0)
		{
            //_enemy.TakeDamage(tickDamage);
            rend.material.color = coldSnapColor;
			currentTickCooldown = TickCooldown;
			audioSource.PlayOneShot(coldSnapImpactSound, 1);
			Debug.Log("Snapped!");
		}
	}

    IEnumerator destroy(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        if (rend.material.color != materialColorHolder)
            rend.material.color = materialColorHolder;
        Destroy(this);
    }

// Accessors and Mutators
public float Duration
	{
		get { return duration; }
		set { duration = value; }
	}
	public float TickCooldown
	{
		get { return tickCooldown; }
		set { tickCooldown = value; }
	}
	public float TickDamage
	{
		get { return tickDamage; }
		set { tickDamage = value; }
	}
	public AudioClip ColdSnapSound
	{
		get { return coldSnapSound; }
		set { coldSnapSound = value; }
	}
	public AudioClip ColdSnapImpactSound
	{
		get { return coldSnapImpactSound; }
		set { coldSnapImpactSound = value; }
	}
}
