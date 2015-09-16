using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour 
{
    // VARIABLES
    // Public
	public float speed;

    // Private
	private float damage;
    private GameObject affecter;
	private GameObject target;
	
	// Update is called once per frame
	void Update () 
	{
        // IF target exsist, missile will move towards target, ELSE destory the missile
		if(target)
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }

    // IF missile has hit its designated target and is a enemy, apply damage function and destroy missile.
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Enemy") && other.gameObject == target)
		{
            //other.SendMessage("TakeDamage", damage); // Attemp to call TakeDamage() anywhere.
            other.GetComponent<Enemy>().TakeDamage(damage);
			Destroy(gameObject, 0.5f);
		}
	}

	// Accessors and Mutators
	public float Damage
	{
		get { return damage; }
		set { damage = value; }
	}
    public GameObject Affecter
    {
        get { return affecter; }
        set { affecter = value; }
    }
    public GameObject Target
	{
		get { return target; }
		set { target = value; }
	}
}
