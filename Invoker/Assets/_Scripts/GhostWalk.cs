using UnityEngine;
using System.Collections;

public class GhostWalk : MonoBehaviour
{
    // VARAIBLES
    private float selfSlowMultiplier;
    private float enemySlowMultiplier;

    private float duration = 

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Accessors and Mutators
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
