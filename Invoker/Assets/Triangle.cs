using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour 
{
	public bool canSpin = false;
	public float distance = 1;
	public float spinSpeed = 1;
	public GameObject prefab;
	public GameObject centerPrefab;

	// Use this for initialization
	void Awake () 
	{
		createTriangle ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(canSpin)
			transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
	}

	void createTriangle()
	{
		for(int i = 0; i < 3; i++)
		{
			GameObject tempOBJ = Instantiate(prefab, centerPrefab.transform.forward * distance, Quaternion.identity) as GameObject;
			tempOBJ.transform.SetParent(transform);
			centerPrefab.transform.Rotate(0, 120, 0);		
		}
	}
}
