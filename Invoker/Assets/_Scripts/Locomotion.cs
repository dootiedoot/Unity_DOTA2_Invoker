using UnityEngine;
using System.Collections;

public class Locomotion : MonoBehaviour 
{
	// VARIABLES
	[SerializeField] private float speedMultiplier = 1;
	[SerializeField] private float currentSpeed;
	private NavMeshAgent navMeshAgent;

	// Assign references
	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		currentSpeed = navMeshAgent.speed;
	}

	public void adjustSpeed(float SpeedMultiplier)
	{
		speedMultiplier = SpeedMultiplier;
		navMeshAgent.speed += (speedMultiplier * currentSpeed);
		currentSpeed = navMeshAgent.speed;
	}

	// Accessors and Mutators
	public float SpeedMultiplier
	{
		get { return speedMultiplier; }
		set { speedMultiplier = value; }
	}
}
