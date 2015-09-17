using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour 
{
	public float coldSnapDuration;
	public float coldSnapTickCooldown;
	public float coldSnapTickDamage;

	public int GhostWalkEnemySlow;
	public int GhostWalkSelfSlow;

	public AudioClip coldSnapSound;
	public AudioClip coldSnapImpactSound;
	public AudioClip ghostWalkSound;
	public AudioClip iceWallSound;
	public AudioClip empSound;
	public AudioClip tornadoSound;
	public AudioClip alacritySound;
	public AudioClip sunStrikeSound;
	public AudioClip forgeSpiritSound;
	public AudioClip chaosMeteorSound;
	public AudioClip defeaningBlastSound;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Public method to call the Cold Snap method
	public void CastColdSnap()
	{
		StartCoroutine(ColdSnap(coldSnapDuration, coldSnapTickCooldown, coldSnapTickDamage));
	}
	public void CastGhostWalk()
	{
		StartCoroutine(GhostWalk(GhostWalkEnemySlow, GhostWalkSelfSlow));
	}

	// Raycast at the mouse position until the Left-Click is performed. If an enemy falls under the Raycast when 
	// clicked, attach the Cold Snap script and associated properties.
	IEnumerator ColdSnap(float duration, float tickCooldown, float tickDamage)
	{
		while(!Input.GetButtonUp("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			Physics.Raycast(ray, out hit);
			if(hit.collider.CompareTag("Enemy") && Input.GetButtonDown("Fire1"))
			{
				Debug.Log("Casted: Cold Snap on " + hit.collider.name);
				ColdSnap coldSnap = hit.collider.gameObject.AddComponent<ColdSnap>();
				coldSnap.Duration = duration;
				coldSnap.TickCooldown = tickCooldown;
				coldSnap.TickDamage = tickDamage;
				coldSnap.ColdSnapSound = coldSnapSound;
				coldSnap.ColdSnapImpactSound = coldSnapImpactSound;
            }
			yield return null;
		}
	}

	// Raycast at the mouse position until the Left-Click is performed. If an enemy falls under the Raycast when 
	// clicked, attach the Cold Snap script and associated properties.
	IEnumerator GhostWalk(int enemySlow, int selfSlow)
	{
		bool GhostWalk = true;
		while(GhostWalk)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Input.GetButtonDown ("Fire2") && Physics.Raycast(ray, out hit, 100)) 
			{
				if (hit.collider.CompareTag("Enemy"))
				{
					GhostWalk = false;
				}
				else
				{

				}
			}
			yield return null;
		}
	}
}
