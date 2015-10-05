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
    public float coldSnapCooldown;

    public Material GhostWalkMaterial;
    public float ghostWalkDuration;
	public float ghostWalkEnemySlow;
	public float ghostWalkSelfSlow;
    public float ghostWalkCooldown;

    public GameObject tornadoPrefab;
    public float tornadoTravelTime;
    public float tornadoBonusDamage;
    public float tornadoLiftDuration;
    public float tornadoCooldown;

    public GameObject empPrefab;
    public int empManaBurned;
    public float empDmgPerManaPercent;
    public float empManaGainPercent;
    public float empCooldown;

    public GameObject alacrityPrefab;
    public float alacrityDuration;
    public float alacrityAtkSpeed;
    public float alacrityBonusDamage;

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
        StartCoroutine( GhostWalk(ghostWalkDuration, ghostWalkEnemySlow, ghostWalkSelfSlow, ghostWalkCooldown) );
	}
    public void CastTornado()
    {
        StartCoroutine( Tornado(tornadoTravelTime, tornadoBonusDamage, tornadoLiftDuration) );
    }
    public void CastEMP()
    {
        StartCoroutine( EMP(empPrefab, empManaBurned, empDmgPerManaPercent, empManaGainPercent) );
    }
    public void CastAlacrity()
    {
        StartCoroutine(Alacrity(alacrityPrefab, alacrityDuration, alacrityAtkSpeed, alacrityBonusDamage));
    }
    public void CastChaosMeteor()
    {
        StartCoroutine(Alacrity(alacrityPrefab, alacrityDuration, alacrityAtkSpeed, alacrityBonusDamage));
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
	IEnumerator GhostWalk(float duration, float enemySlow, float selfSlow, float cooldown)
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
        // Cooldown
        /*print("GW on Cooldown");
        while(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            yield return new WaitForSeconds(1);
            if (cooldown <= 0)
                print("GW ready");
        }*/
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
                audioSource.PlayOneShot(spellSoundClips[3], 1);
            }
            yield return null;
        }
    }

    // EMP
    IEnumerator EMP(GameObject prefab, float manaBurned, float dmgPerBurnPercent, float manaGainPerBurnPercent)
    {
        while (!Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if (Input.GetButtonDown("Fire1"))
            {
                print("Casted: EMP");
                GameObject emp = Instantiate(prefab, hit.point + Vector3.up, Quaternion.identity) as GameObject;
                EMP _emp = emp.GetComponent<EMP>();
                _emp.Affector = gameObject;
                _emp.ManaBurned = manaBurned;
                _emp.DmgPerBurnPercent = dmgPerBurnPercent;
                _emp.ManaGainPerBurnPercent = manaGainPerBurnPercent;
            }
            yield return null;
        }
    }

    // Alacrity
    IEnumerator Alacrity(GameObject prefab, float duration, float attackSpeed, float bonusDamage)
    {
        while (!Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            if ((hit.collider.CompareTag("Player") || hit.collider.CompareTag("Ally")) && Input.GetButtonDown("Fire1"))
            {
                print("Casted: Alacrity on " + hit.collider.name);
                GameObject alacrity = Instantiate(prefab, hit.transform.position + transform.up * 2, Quaternion.identity) as GameObject;
                alacrity.transform.SetParent(hit.transform);
                Alacrity _alacrity = alacrity.GetComponent<Alacrity>();
                _alacrity.Duration = duration;
                _alacrity.AttackSpeed = attackSpeed;
                _alacrity.BonusDamage = bonusDamage;

            }
            yield return null;
        }
    }

    // Chaos Meteor
    // WEX: 1.55/2.05/2.6/3.1/3.65/4.15/4.7/(5.25) travel time
    IEnumerator ChaosMeteor(GameObject prefab, float travelTime, float damage, float burnDamage)
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
                /*
                print("Casted: Chaos Meteor");
                GameObject alacrity = Instantiate(prefab, hit.transform.position + transform.up * 2, Quaternion.identity) as GameObject;
                alacrity.transform.SetParent(hit.transform);
                Alacrity _alacrity = alacrity.GetComponent<Alacrity>();
                _alacrity.Duration = duration;
                _alacrity.AttackSpeed = attackSpeed;
                _alacrity.BonusDamage = bonusDamage;
                */
            }
            yield return null;
        }
    }
}
