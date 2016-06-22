using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour 
{
	public bool canSpin = false;
	public float radius = 1;
	public float heightOffset = 2;
	public float spinSpeed = 1;
	public GameObject Quas;
	public GameObject Wex;
	public GameObject Exort;
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
		for(int i = 1; i < 4; i++)
		{
			GameObject element = new GameObject("Element" + i.ToString());
			element.transform.position = centerPrefab.transform.position + centerPrefab.transform.forward * radius + transform.up * heightOffset;
			element.transform.SetParent(transform);

			GameObject tempQuas = Instantiate(Quas, element.transform.position, Quaternion.identity) as GameObject;
			tempQuas.transform.SetParent(element.transform);
			GameObject tempWex = Instantiate(Wex, element.transform.position, Quaternion.identity) as GameObject;
			tempWex.transform.SetParent(element.transform);
			GameObject tempExort = Instantiate(Exort, element.transform.position, Quaternion.identity) as GameObject;
			tempExort.transform.SetParent(element.transform);

			centerPrefab.transform.Rotate(0, 120, 0);		
		}
	}
}
