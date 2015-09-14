using UnityEngine;
using System.Collections;

public class ColdSnap : MonoBehaviour 
{
	private float duration;
	private float tickCooldown;
	[SerializeField] private float currentTickCooldown;
	private float tickDamage;

	private Enemy _enemy;

	private AudioSource audioSource;

	private AudioClip coldSnapSound;
	private AudioClip coldSnapImpactSound;

	void OnEnable()
	{
		Enemy.OnDamaged += SnapTick;
	}
		

	void OnDisable()
	{
		Enemy.OnDamaged -= SnapTick;
	}

	void Awake () 
	{
		_enemy = GetComponent<Enemy>();
		audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () 
	{
		currentTickCooldown = TickCooldown;
		audioSource.PlayOneShot(coldSnapSound, 1);
		Destroy (GetComponent<ColdSnap>(), duration);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentTickCooldown > 0)
		{
			currentTickCooldown -= Time.deltaTime;
		}
	}

	void SnapTick()
	{
		if(currentTickCooldown <= 0)
		{
			//_enemy.TakeDamage(tickDamage);
			currentTickCooldown = TickCooldown;
			audioSource.PlayOneShot(coldSnapImpactSound, 1);
			Debug.Log("Snapped!");
		}
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
