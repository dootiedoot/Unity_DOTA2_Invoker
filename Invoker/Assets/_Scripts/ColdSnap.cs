﻿using UnityEngine;
using System.Collections;

public class ColdSnap : MonoBehaviour 
{
    // VARIABLES
    // Private
	private float duration = 1;
	private float tickCooldown;
	private float currentTickCooldown = 0;
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
        // Assign references
        _enemy = GetComponent<Enemy>();
        Stun _stun = gameObject.AddComponent<Stun>();
        _stun.Duration = 0.4f;
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () 
	{
        // Assignments
        currentTickCooldown = TickCooldown;

		// 1st Tick color change
		materialColorHolder = rend.sharedMaterial.color;
		coldSnapColor = Color.blue;
		SetColor(rend.sharedMaterial, coldSnapColor);
		
		// 1st tick audio
        audioSource.PlayOneShot(coldSnapSound, 1);
        
        // Start IEnumerator to destory object after x seconds.
        StartCoroutine(destroy(duration));
	}
	
	// Update is called once per frame
	void Update () 
	{
        // if the cooldown is not 0 or less, countdown cooldown and adjust material to original.
		if(currentTickCooldown > 0)
		{
			currentTickCooldown -= Time.deltaTime;
            if (currentTickCooldown <= 0.4f)
				SetColor(rend.sharedMaterial, materialColorHolder);
		}
	}

    // When damaged, check cooldown for the next snap; If snappable, reset cooldown, do damage, adjust visuals/audio.
	void OnDamage()
	{
		if(currentTickCooldown <= 0)
		{
            Stun stun = gameObject.AddComponent<Stun>();
            stun.Duration = 0.4f;
            currentTickCooldown = TickCooldown;
            _enemy.TakeDamage(tickDamage);

            rend.sharedMaterial.color = coldSnapColor;
			audioSource.PlayOneShot(coldSnapImpactSound, 1);
			print("Snapped!");
		}
	}

	public void SetColor(Material material, Color value) 
	{
		Color color = material.color;
		color = value;
		material.color = color;
	}

    // Stop Cold Snap after duration is up and return material back to original.
    IEnumerator destroy(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Die();
    }

    public void Die()
    {
        if (rend.sharedMaterial.color != materialColorHolder)
            SetColor(rend.sharedMaterial, materialColorHolder);
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
