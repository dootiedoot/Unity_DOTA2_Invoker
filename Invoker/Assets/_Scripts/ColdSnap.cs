using UnityEngine;
using System.Collections;

public class ColdSnap : MonoBehaviour 
{
	private float tickCooldown;
	private float duration;

	private AudioSource audioSource;

	private AudioClip coldSnapSound;
	private AudioClip coldSnapImpactSound;
	

	void Awake () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () 
	{
		audioSource.PlayOneShot(coldSnapSound, 1);
		Destroy (GetComponent<ColdSnap>(), duration);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Accessors and Mutators
	public float Duration
	{
		get { return duration; }
		set { duration = value; }
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
