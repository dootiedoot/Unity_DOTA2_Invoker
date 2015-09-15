using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour 
{
	public float speed;
	private float damage;
    private GameObject affecter;
	private GameObject target;

    // Use this for initialization
    void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target)
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Enemy") && other.gameObject == target)
		{
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
