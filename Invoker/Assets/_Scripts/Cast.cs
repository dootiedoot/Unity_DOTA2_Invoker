using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour 
{
	// VARIABLES
	// spell attributes
	public float coldSnapDuration;
	public float coldSnapTickCooldown;
	public float coldSnapTickDamage;

    public float GhostWalkDuration;
	public float GhostWalkEnemySlow;
	public float GhostWalkSelfSlow;

	// Audio
	public AudioClip[] spellSoundClips;

	// Public method to call the Cold Snap method
	public void CastColdSnap()
	{
		StartCoroutine(ColdSnap(coldSnapDuration, coldSnapTickCooldown, coldSnapTickDamage));
	}
	public void CastGhostWalk()
	{
		StartCoroutine(GhostWalk(GhostWalkDuration, GhostWalkEnemySlow, GhostWalkSelfSlow));
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
				print("Casted: Cold Snap on " + hit.collider.name);
				ColdSnap coldSnap = hit.collider.gameObject.AddComponent<ColdSnap>();
				coldSnap.Duration = duration;
				coldSnap.TickCooldown = tickCooldown;
				coldSnap.TickDamage = tickDamage;
				coldSnap.ColdSnapSound = spellSoundClips[0];
				coldSnap.ColdSnapImpactSound = spellSoundClips[1];
            }
			yield return null;
		}
	}

	// Raycast at the mouse position until the Left-Click is performed. If an enemy falls under the Raycast when 
	// clicked, attach the Cold Snap script and associated properties.
	IEnumerator GhostWalk(float duration, float enemySlow, float selfSlow)
	{
		print("Casted: Ghost Walk");
        GhostWalk ghostWalk = gameObject.AddComponent<GhostWalk>();
        ghostWalk.Duration = duration;
        ghostWalk.SelfSlow = selfSlow;
        ghostWalk.EnemySlow = enemySlow;
		ghostWalk.GhostWalkSound = spellSoundClips[2];

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
			}
			yield return null;
		}
	}
}
