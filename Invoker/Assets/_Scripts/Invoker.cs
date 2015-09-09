using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Invoker : MonoBehaviour 
{
	public bool canCast = true;
	public bool isAttacking;
	public int elementSize = 3;
	public List<int> elements;
	public int spellsSize = 2;
	public List<int> spells;
	public GameObject[] spellPrefab;

	public AudioClip invokeSound;
	public AudioClip failSound;

	private Transform E1Q;
	private Transform E1W;
	private Transform E1E;
	private Transform E2Q;
	private Transform E2W;
	private Transform E2E;
	private Transform E3Q;
	private Transform E3W;
	private Transform E3E;

	private string spellName;

	private AudioSource audioSource;

	// Use this for initialization
	void Awake () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		E1Q = transform.FindChild("Circle").FindChild("Element1").GetChild(0);
		E1W = transform.FindChild("Circle").FindChild("Element1").GetChild(1);
		E1E = transform.FindChild("Circle").FindChild("Element1").GetChild(2);
		E2Q = transform.FindChild("Circle").FindChild("Element2").GetChild(0);
		E2W = transform.FindChild("Circle").FindChild("Element2").GetChild(1);
		E2E = transform.FindChild("Circle").FindChild("Element2").GetChild(2);
		E3Q = transform.FindChild("Circle").FindChild("Element3").GetChild(0);
		E3W = transform.FindChild("Circle").FindChild("Element3").GetChild(1);
		E3E = transform.FindChild("Circle").FindChild("Element3").GetChild(2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			elements.Add(1);
			if(elements.Count > elementSize){
				elements.RemoveAt(0);
			}
			updateVisuals();
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			elements.Add(2);
			if(elements.Count > elementSize){
				elements.RemoveAt(0);
			}
			updateVisuals();
		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			elements.Add(3);
			if(elements.Count > elementSize){
				elements.RemoveAt(0);
			}
			updateVisuals();
		}

		if(canCast)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Invoke();
			}
			else if(Input.GetKeyUp(KeyCode.D))
			{
				CastSpell(spells[0]);
			}
			else if(Input.GetKeyUp(KeyCode.F))
			{
				CastSpell(spells[1]);
			}
		}
	}

	void Invoke()
	{
		// 1 = Quas, 2 = Wex, 3 = Exort
		if(elements[0] == 0 || elements[1] == 0 || elements[2] == 0){
			Debug.Log("Nothing to invoke");
			audioSource.PlayOneShot(failSound, 1);
		}
		else
		{
			if(elements[0] == 1 && elements[1] == 1 && elements[2] == 1)
			{
				// QQQ
				spellName = "Cold Snap";
				spells.Add(0);
			}
			else if((elements[0] == 1 && elements[1] == 1 && elements[2] == 2) || (elements[0] == 1 && elements[1] == 2 && elements[2] == 1) || (elements[0] == 2 && elements[1] == 1 && elements[2] == 1))
			{
				// QQW, QWQ, WQQ
				spellName = "Ghost Walk";
				spells.Add(1);
			}
			else if((elements[0] == 1 && elements[1] == 2 && elements[2] == 2) || (elements[0] == 2 && elements[1] == 1 && elements[2] == 2) || (elements[0] == 2 && elements[1] == 2 && elements[2] == 1))
			{
				// QWW, WQW, WWQ
				spellName = "Tornado";
				spells.Add(2);
			}
			else if(elements[0] == 2 && elements[1] == 2 && elements[2] == 2)
			{
				// WWW
				spellName = "EMP";
				spells.Add(3);
			}				
			else if((elements[0] == 2 && elements[1] == 2 && elements[2] == 3) || (elements[0] == 2 && elements[1] == 3 && elements[2] == 2) || (elements[0] == 3 && elements[1] == 2 && elements[2] == 2))
			{
				// WWE, WEW, EWW
				spellName = "Alacrity";
				spells.Add(4);
			}
			else if((elements[0] == 2 && elements[1] == 3 && elements[2] == 3) || (elements[0] == 3 && elements[1] == 2 && elements[2] == 3) || (elements[0] == 3 && elements[1] == 3 && elements[2] == 2))
			{
				// WEE, EWE, EEW
				spellName = "Chaos Meteor";
				spells.Add(5);
			}
			else if(elements[0] == 3 && elements[1] == 3 && elements[2] == 3)
			{
				// EEE
				spellName = "Sun Strike";
				spells.Add(6);
			}	
			else if((elements[0] == 3 && elements[1] == 3 && elements[2] == 1) || (elements[0] == 3 && elements[1] == 1 && elements[2] == 3) || (elements[0] == 1 && elements[1] == 3 && elements[2] == 3))
			{
				// EEQ, EQE, QEE
				spellName = "Forge Spirit";
				spells.Add(7);
			}
			else if((elements[0] == 3 && elements[1] == 1 && elements[2] == 1) || (elements[0] == 1 && elements[1] == 3 && elements[2] == 1) || (elements[0] == 1 && elements[1] == 1 && elements[2] == 3))
			{
				// EQQ, QEQ, QQE
				spellName = "Ice Wall";
				spells.Add(8);
			}
			else
			{
				// QWE, QEW, WQE, WEQ, EQW, EWQ
				spellName = "Deafening Blast";
				spells.Add(9);
			}

			if(spells.Count > spellsSize){
				spells.RemoveAt(0);
			}
			// Incase there are more then 3 elements and Permutation is needed. Use Else instead to save code.
			/*else if((elements[0] == 1 && elements[1] == 2 && elements[2] == 3) || 
				        (elements[0] == 1 && elements[1] == 3 && elements[2] == 2) || 
				        (elements[0] == 2 && elements[1] == 1 && elements[2] == 3) || 
				        (elements[0] == 2 && elements[1] == 3 && elements[2] == 2) ||
				        (elements[0] == 3 && elements[1] == 1 && elements[2] == 2) || 
				        (elements[0] == 3 && elements[1] == 2 && elements[2] == 1))
					{
					// QWE, QEW, WQE, WEQ, EQW, EWQ
					spellName = "Deafening Blast";
					}*/
			audioSource.PlayOneShot(invokeSound, 1);
			Debug.Log("Invoked: " + spellName);
		}
	}
	
	void updateVisuals()
	{
		/*if(elements[0] == 0 || elements[1] == 0 || elements[2] == 0){
			E1Q.gameObject.SetActive(false);
			E1W.gameObject.SetActive(false);
			E1E.gameObject.SetActive(false);
			E2Q.gameObject.SetActive(false);
			E2W.gameObject.SetActive(false);
			E2E.gameObject.SetActive(false);
			E3Q.gameObject.SetActive(false);
			E3W.gameObject.SetActive(false);
			E3E.gameObject.SetActive(false);
		}*/
		// Quas
		if(elements[0] == 1){
			E1Q.gameObject.SetActive(true);
			E1W.gameObject.SetActive(false);
			E1E.gameObject.SetActive(false);
		}
		if(elements[1] == 1){
			E2Q.gameObject.SetActive(true);
			E2W.gameObject.SetActive(false);
			E2E.gameObject.SetActive(false);
		}
		if(elements[2] == 1){
			E3Q.gameObject.SetActive(true);
			E3W.gameObject.SetActive(false);
			E3E.gameObject.SetActive(false);
		}

		// Wex
		if(elements[0] == 2){
			E1Q.gameObject.SetActive(false);
			E1W.gameObject.SetActive(true);
			E1E.gameObject.SetActive(false);
		}
		if(elements[1] == 2){
			E2Q.gameObject.SetActive(false);
			E2W.gameObject.SetActive(true);
			E2E.gameObject.SetActive(false);
		}
		if(elements[2] == 2){
			E3Q.gameObject.SetActive(false);
			E3W.gameObject.SetActive(true);
			E3E.gameObject.SetActive(false);
		}

		// Exort
		if(elements[0] == 3){
			E1Q.gameObject.SetActive(false);
			E1W.gameObject.SetActive(false);
			E1E.gameObject.SetActive(true);
		}
		if(elements[1] == 3){
			E2Q.gameObject.SetActive(false);
			E2W.gameObject.SetActive(false);
			E2E.gameObject.SetActive(true);
		}
		if(elements[2] == 3){
			E3Q.gameObject.SetActive(false);
			E3W.gameObject.SetActive(false);
			E3E.gameObject.SetActive(true);
		}
	}

	void CastSpell(int spellNum)
	{
		switch (spellNum)
		{
			case 0:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 1:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 2:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 3:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 4:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 5:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 6:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 7:
				Debug.Log("Cast: Forge Spirit");
				break;
			case 8:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 9:
				print ("Ulg, glib, Pblblblblb");
				break;
			default:
				print ("Invalid cast");
				break;
		}
	}

	/*
	IEnumerator castFlamethrower() 
	{
		GameObject temp = Instantiate(spells[0], transform.position + transform.forward, transform.rotation) as GameObject;
		temp.transform.SetParent(transform);
		while(Input.GetButton("Fire1")) 
		{
			isAttacking = true;
			anim.SetBool(CastingHash, true);
			//_Movement.CurrentSpeed = _Movement.MaxSpeed * 0.5f;
			//_Movement.CanRotate = false;
			Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * 2, 4);
			foreach(Collider other in hitColliders)
			{
				if(other.CompareTag(Tags.enemy) && other != other.GetComponent<SphereCollider>())
				{
					// Create a vector from the enemy to the player and store the angle between it and forward.
					Vector3 direction = other.transform.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					// If the angle between forward and where the player is, is less than half the angle of view...
					if(angle < 130f * 0.5f)
					{
						_combatModifier.modifyDamage(Random.Range(5, 11), other.gameObject, gameObject);
					}
				}
			}
			yield return new WaitForSeconds(0.2f);
		}
		isAttacking = false;
		anim.SetBool(CastingHash, false);
		_Movement.CurrentSpeed = _Movement.MaxSpeed;
		_Movement.CanRotate = true;
		temp.GetComponent<ParticleSystem>().enableEmission = false;
		yield return new WaitForSeconds(0.5f);
		Destroy(temp);
	}

	void castMud()
	{
		GameObject temp = Instantiate(spells[1], transform.position - transform.up + transform.forward*6, Quaternion.Euler(90, 0, 0)) as GameObject;
		temp.GetComponent<MudField>().DeathTimer = 10f;
	}
*/
	public bool CanCast
	{
		get { return canCast; }
		set { canCast = value; }
	}
}
