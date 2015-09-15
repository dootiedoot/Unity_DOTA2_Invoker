using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour 
{
	public float shootDistance = 10f;
	public float shootRate = .5f;
	public float damage = 1;
	public GameObject misslePrefab;

	private NavMeshAgent navMeshAgent;
    private GameObject affecter;
	private GameObject target;
	private Ray shootRay;
	private RaycastHit shootHit;
	private bool walking;
	private bool enemyClicked;
	private float nextFire;
	
	// Use this for initialization
	void Awake () 
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

    void Start()
    {
        affecter = gameObject;
    }
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetButtonDown ("Fire2")) 
		{
			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.collider.CompareTag("Enemy"))
				{
					target = hit.collider.gameObject;
					enemyClicked = true;
				}
				else
				{
					walking = true;
					enemyClicked = false;
					navMeshAgent.destination = hit.point;
					navMeshAgent.Resume();
				}
			}
		}
		
		if (enemyClicked) {
			MoveAndShoot();
		}
		
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
				walking = false;
		} else {
			walking = true;
		}		
	}
	
	private void MoveAndShoot()
	{
		if (target == null)
			return;
		navMeshAgent.destination = target.transform.position;
		if (navMeshAgent.remainingDistance >= shootDistance) 
		{
			navMeshAgent.Resume();
			walking = true;
		}
		
		if (navMeshAgent.remainingDistance <= shootDistance) 
		{
			transform.LookAt(target.transform);
			Vector3 dirToShoot = target.transform.position - transform.position;
			if (Time.time > nextFire)
			{
				nextFire = Time.time + shootRate;
				GameObject tempMissile = Instantiate(misslePrefab, transform.position + transform.up + transform.forward, Quaternion.identity) as GameObject;
				tempMissile.GetComponent<Missile>().Damage = damage;
                tempMissile.GetComponent<Missile>().Affecter = affecter;
                tempMissile.GetComponent<Missile>().Target = target;
			}
			navMeshAgent.Stop();
			walking = false;
		}
	}
	
}
