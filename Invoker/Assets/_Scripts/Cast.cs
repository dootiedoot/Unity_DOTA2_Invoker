using UnityEngine;
using System.Collections;

public class Cast : MonoBehaviour 
{
    // VARIABLES
    // spell attributes
    public Material ColdSnapMaterial;
	public float coldSnapDuration;
	public float coldSnapTickCooldown;
	public float coldSnapTickDamage;

    public Material GhostWalkMaterial;
    public float GhostWalkDuration;
	public float GhostWalkEnemySlow;
	public float GhostWalkSelfSlow;

    public GameObject tornadoPrefab;
    public float tornadoTravelTime;
    public float tornadoBonusDamage;
    public float tornadoLiftDuration;

    public GameObject empPrefab;
    public float empDuration;
    public float empManaBurned;
    public float empDmgPerManaPercent;
    public float empManaGainPercent;

    // Scripts
    private PlayerController _playerController;

    // Audio
    private AudioSource audioSource;
	public AudioClip[] spellSoundClips;

    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

	// Public methods that are called to execute the spell methods
	public void CastColdSnap()
    {
        if (GetComponent<ColdSnap>())
            GetComponent<ColdSnap>().Die();
        StartCoroutine( ColdSnap(coldSnapDuration, coldSnapTickCooldown, coldSnapTickDamage) );
	}
	public void CastGhostWalk()
    {
        _playerController.Target = null;
        if (GetComponent<GhostWalk>())
            GetComponent<GhostWalk>().Die();
        StartCoroutine( GhostWalk(GhostWalkDuration, GhostWalkEnemySlow, GhostWalkSelfSlow) );
	}
    public void CastTornado()
    {
        StartCoroutine( Tornado(tornadoTravelTime, tornadoBonusDamage, tornadoLiftDuration) );
    }
    public void CastEMP()
    {
        StartCoroutine( EMP(empPrefab, empDuration, empManaBurned, empDmgPerManaPercent, empManaGainPercent) );
    }

    // Coldsnap
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
                coldSnap.SnapMaterial = ColdSnapMaterial;
				coldSnap.ColdSnapSound = spellSoundClips[0];
				coldSnap.ColdSnapImpactSound = spellSoundClips[1];
            }
			yield return null;
		}
	}

    // Ghost Walk
	IEnumerator GhostWalk(float duration, float enemySlow, float selfSlow)
	{
		print("Casted: Ghost Walk");
        GhostWalk ghostWalk = gameObject.AddComponent<GhostWalk>();
        ghostWalk.Duration = duration;
        ghostWalk.SelfSlow = selfSlow;
        ghostWalk.EnemySlow = enemySlow;
        ghostWalk.InvisMaterial = GhostWalkMaterial;
		ghostWalk.GhostWalkSound = spellSoundClips[2];

        bool GhostWalk = true;
		while(GhostWalk)
		{
			if (_playerController.Target != null) 
			{
                ghostWalk.Die();
				GhostWalk = false;
			}
			yield return null;
		}
	}

    // Tornado
    IEnumerator Tornado(float travelTime, float bonusDamage, float liftDuration)
    {
        while (!Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 relativePos = hit.point - transform.position;
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z)); 
                print("Casted: Tornado");
                GameObject tornado = Instantiate(tornadoPrefab, transform.position, rotation) as GameObject;
                Tornado _tornado = tornado.GetComponent<Tornado>();
                _tornado.TravelTime = travelTime;
                _tornado.BonusDamage = bonusDamage;
                _tornado.LiftDuration = liftDuration;
                _tornado.TornadoTravelSound = spellSoundClips[4];
                audioSource.PlayOneShot(spellSoundClips[3], 1);
            }
            yield return null;
        }
    }

    // EMP
    IEnumerator EMP(GameObject prefab, float duration, float manaBurned, float dmgPerBurnPercent, float manaGainPerBurnPercent)
    {
        while (!Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if (Input.GetButtonDown("Fire1"))
            {
                print("Casted: EMP");
                GameObject emp = Instantiate(prefab, hit.point + Vector3.up * 2, Quaternion.identity) as GameObject;
                EMP _emp = emp.GetComponent<EMP>();

                //_tornado.TornadoTravelSound = spellSoundClips[4];
                audioSource.PlayOneShot(spellSoundClips[3], 1);
            }
            yield return null;
        }
    }
}
