using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour 
{
	public float shootDistance = 10f;
	public float shootRate = .5f;

	private NavMeshAgent navMeshAgent;
	private Transform targetedEnemy;
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
					targetedEnemy = hit.transform;
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
		if (targetedEnemy == null)
			return;
		navMeshAgent.destination = targetedEnemy.position;
		if (navMeshAgent.remainingDistance >= shootDistance) {
			
			navMeshAgent.Resume();
			walking = true;
		}
		
		if (navMeshAgent.remainingDistance <= shootDistance) {
			
			transform.LookAt(targetedEnemy);
			Vector3 dirToShoot = targetedEnemy.transform.position - transform.position;
			if (Time.time > nextFire)
			{
				nextFire = Time.time + shootRate;
				Debug.Log("Shoot");
				//shootingScript.Shoot(dirToShoot);
			}
			navMeshAgent.Stop();
			walking = false;
		}
	}
	
}
