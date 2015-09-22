using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float shootDistance = 10f;
	public float shootRate = .5f;
	public float damage = 1;
	public GameObject misslePrefab;
    public GameObject locationMarker;
    public GameObject selectionMarker;
    private Renderer selectionMarkerRend;

	private NavMeshAgent navMeshAgent;
    private GameObject affecter;
	private GameObject target;
	private Ray shootRay;
	private RaycastHit shootHit;
	private bool walking;
	private bool enemyClicked;
	private float nextFire;
	
	// Use this for initialization
	void Awake () 
	{
        selectionMarkerRend = selectionMarker.GetComponent<Renderer>();
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

    void Start()
    {
        affecter = gameObject;
    }

    // Update is called once per frame
	void Update()
	{
		// Stop actions if the "Stop" command is pressed
		if(Input.GetButtonDown("Stop"))
		{
			navMeshAgent.Stop();
			walking = false;
			target = null;
		}

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{
            selectionMarker.transform.position = hit.point;
            if (hit.collider.CompareTag("Enemy"))
            {
                selectionMarker.SetActive(true);
                selectionMarker.transform.position = hit.transform.position + transform.up * 2;
                selectionMarkerRend.material.SetColor("_Color", Color.red);
            }
            else if (hit.collider.CompareTag("Ally"))
            {
                selectionMarker.SetActive(true);
                selectionMarker.transform.position = hit.transform.position + transform.up * 2;
                selectionMarkerRend.material.SetColor("_Color", Color.green);
            }
            else
            {
                selectionMarker.SetActive(false);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (hit.collider.CompareTag("Enemy"))
				{
					target = hit.collider.gameObject;
					enemyClicked = true;
				}
				else
				{
					walking = true;
					enemyClicked = false;
					navMeshAgent.destination = hit.point;
					navMeshAgent.Resume();
                    Destroy(Instantiate(locationMarker, hit.point, Quaternion.identity), .5f);
                }
			}
		}
		
		if (enemyClicked) {
			MoveAndShoot();
		}
		
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
				walking = false;
		} else {
			walking = true;
		}		
	}
	
	private void MoveAndShoot()
	{
		if (target == null)
			return;
		navMeshAgent.destination = target.transform.position;
		if (navMeshAgent.remainingDistance >= shootDistance) 
		{
			navMeshAgent.Resume();
			walking = true;
		}
		
		if (navMeshAgent.remainingDistance <= shootDistance) 
		{
			transform.LookAt(target.transform);
			Vector3 dirToShoot = target.transform.position - transform.position;
			if (Time.time > nextFire)
			{
				nextFire = Time.time + shootRate;
				GameObject tempMissile = Instantiate(misslePrefab, transform.position + transform.up + transform.forward, Quaternion.identity) as GameObject;
				tempMissile.GetComponent<Missile>().Damage = damage;
                tempMissile.GetComponent<Missile>().Affecter = affecter;
                tempMissile.GetComponent<Missile>().Target = target;
			}
			navMeshAgent.Stop();
			walking = false;
		}
	}
}
