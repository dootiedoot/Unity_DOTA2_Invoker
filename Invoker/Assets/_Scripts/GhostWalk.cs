using UnityEngine;
using System.Collections;

public class GhostWalk : MonoBehaviour
{
    // VARAIBLES
	// Attributes
    private float duration = 1;
    private float selfSlowMultiplier;
    private float enemySlowMultiplier;

	private Locomotion _locomotion;

	// Visuals
	private Material myMaterial;

	// Audio
	private AudioClip ghostWalkSound;
	private AudioSource audioSource;

	// Assign references
	void Awake () 
	{
		_locomotion = GetComponent<Locomotion>();
		audioSource = GetComponent<AudioSource>();
		myMaterial = GetComponent<Renderer>().sharedMaterial;
	}

	// Use this for initialization
	void Start ()
    {
		_locomotion.adjustSpeed(-selfSlowMultiplier);

		// Play audio
		audioSource.PlayOneShot(ghostWalkSound, 1);
		// set visuals
		SetInvisibleMaterial(myMaterial, 0.4f, 3);

		// Start IEnumerator to destory object after x seconds.
		StartCoroutine(destroy(duration));
	}

	// Changes rendering mode and alpha of the given material
	public void SetInvisibleMaterial(Material material, float value, int mode) 
	{
		Color color = material.color;
		color.a = value;
		material.color = color;
		myMaterial.SetInt("_Mode", mode);
		myMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		myMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		myMaterial.SetInt("_ZWrite", 0);
		myMaterial.DisableKeyword("_ALPHATEST_ON");
		myMaterial.EnableKeyword("_ALPHABLEND_ON");
		myMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		myMaterial.renderQueue = 3000;
	}

	// Stop ghost walk after duration is up and return material back to original.
	IEnumerator destroy(float Duration)
	{
		yield return new WaitForSeconds(Duration);

		_locomotion.adjustSpeed(100*-selfSlowMultiplier);

		SetInvisibleMaterial(myMaterial, 1, 0);

		Destroy(this);
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
	public AudioClip GhostWalkSound
	{
		get { return ghostWalkSound; }
		set { ghostWalkSound = value; }
	}
}
