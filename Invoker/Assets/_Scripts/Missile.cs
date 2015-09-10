using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour 
{
	public float speed;
	private GameObject target;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.collider.CompareTag("Enemy"))
		{
			Debug.Log("Hit!");
			Destroy(gameObject, 0.2f);
		}
	}

	// Accessors and Mutators
	public GameObject Target
	{
		get { return target; }
		set { target = value; }
	}
}
