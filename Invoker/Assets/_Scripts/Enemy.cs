using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    // VARIABLES
	public float health = 100;
	public float maxHealth = 100;
    public Transform[] wayPoints;
    private int nextWayPoint;

    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
	{
		health = maxHealth;
	}

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        navMeshAgent.destination = wayPoints[nextWayPoint].position;
        //navMeshAgent.Resume();

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
        }
    }

    public void TakeDamage(float dmg) 
	{
        BroadcastMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
		health -= dmg;
		
		if(health > maxHealth)
			health = maxHealth;
		else if (health <= 0) 
		{
			//Die();
		}
	}
}
