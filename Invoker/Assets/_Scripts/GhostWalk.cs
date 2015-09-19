using UnityEngine;
using System.Collections;

public class GhostWalk : MonoBehaviour
{
    // VARAIBLES
    private float duration;
    private float selfSlowMultiplier;
    private float enemySlowMultiplier;

	// Use this for initialization
	void Start ()
    {
	
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
    public float SelfSlow
    {
        get { return selfSlowMultiplier; }
        set { selfSlowMultiplier = value; }
    }
    public float EnemySlow
    {
        get { return enemySlowMultiplier; }
        set { enemySlowMultiplier = value; }
    }
}
