using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public delegate void DamageAction();
	public static event DamageAction OnDamaged;
	
	public float health = 100;
	public float maxHealth = 100;
	public bool canMove;

	void Start()
	{
		health = maxHealth;
	}
	
	void Update()
	{
		
	}

	public void TakeDamage(float dmg) 
	{
		if(OnDamaged != null)
			OnDamaged();

		health -= dmg;
		
		if(health > maxHealth)
			health = maxHealth;
		else if (health <= 0) 
		{
			//Die();
		}
	}
}
