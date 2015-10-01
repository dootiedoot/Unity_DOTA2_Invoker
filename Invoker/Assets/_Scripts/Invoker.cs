using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Invoker : MonoBehaviour 
{
	public bool canCast = true;
	public List<int> elements;
	public List<int> spells;
    public Sprite[] spellIcons;

    public Button buttonD;
    public Button buttonF;

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

	private AudioSource audioSource;
	private Cast _cast;

	// Use this for initialization
	void Awake () 
	{
		audioSource = GetComponent<AudioSource>();
		_cast = GetComponent<Cast>();
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
        // keys: Q,W,E are to create the elements by adding the associated element number the element list and 
        // calls the method to update the visual of it. The oldest element will be removed from the element list if
        // the list exceeds the maximum(3).
        if (Input.GetKeyDown(KeyCode.Q))
            castQuas();
        else if (Input.GetKeyDown(KeyCode.W))
            castWex();
        else if (Input.GetKeyDown(KeyCode.E))
            castExort();

		// Keys: R will call the Invoke() method that evaluate the current elements in element list.
		// Keys: D,F will call the spell casting method based on the order of spells in the spell list with 1 = newest and 0 = oldest
		if(canCast)
		{
			if(Input.GetKeyDown(KeyCode.R))
				Invoke();
			else if(Input.GetKeyUp(KeyCode.D))
				castSpell(spells[1]);
			else if(Input.GetKeyUp(KeyCode.F))
				castSpell(spells[0]);
		}
	}

    public void castQuas()
    {
        elements.Add(1);
        if (elements.Count > 3)
            elements.RemoveAt(0);
        updateVisuals();
    }
    public void castWex()
    {
        elements.Add(2);
        if (elements.Count > 3)
            elements.RemoveAt(0);
        updateVisuals();
    }
    public void castExort()
    {
        elements.Add(3);
        if (elements.Count > 3)
            elements.RemoveAt(0);
        updateVisuals();
    }

    // Evaluate current elements numbers and add the associated spell number to the spell list while tracking
    // permutations of the element number combination. 
    public void Invoke()
	{
        // Update button visuals beforehand
        Sprite previousSprite = null;
        if (spells[1] != 0)
            previousSprite = buttonD.image.sprite;

        // 1 = Quas, 2 = Wex, 3 = Exort
        if (elements[0] == 0 || elements[1] == 0 || elements[2] == 0){
			print("Nothing to invoke");
			audioSource.PlayOneShot(failSound, 1);
		}
		else
		{
            string spellName;
			if(elements[0] == 1 && elements[1] == 1 && elements[2] == 1)
			{
				// QQQ
				spellName = "Cold Snap";
                if(spells[1] != 1)
				    spells.Add(1);
                buttonD.image.sprite = spellIcons[0];
            }
			else if((elements[0] == 1 && elements[1] == 1 && elements[2] == 2) || (elements[0] == 1 && elements[1] == 2 && elements[2] == 1) || (elements[0] == 2 && elements[1] == 1 && elements[2] == 1))
			{
				// QQW, QWQ, WQQ
				spellName = "Ghost Walk";
                if (spells[1] != 2)
                    spells.Add(2);
                buttonD.image.sprite = spellIcons[1];
            }
			else if((elements[0] == 1 && elements[1] == 2 && elements[2] == 2) || (elements[0] == 2 && elements[1] == 1 && elements[2] == 2) || (elements[0] == 2 && elements[1] == 2 && elements[2] == 1))
			{
				// QWW, WQW, WWQ
				spellName = "Tornado";
                if (spells[1] != 3)
                    spells.Add(3);
                buttonD.image.sprite = spellIcons[2];
            }
			else if(elements[0] == 2 && elements[1] == 2 && elements[2] == 2)
			{
				// WWW
				spellName = "EMP";
                if (spells[1] != 4)
                    spells.Add(4);
                buttonD.image.sprite = spellIcons[3];
            }				
			else if((elements[0] == 2 && elements[1] == 2 && elements[2] == 3) || (elements[0] == 2 && elements[1] == 3 && elements[2] == 2) || (elements[0] == 3 && elements[1] == 2 && elements[2] == 2))
			{
				// WWE, WEW, EWW
				spellName = "Alacrity";
                if (spells[1] != 5)
                    spells.Add(5);
                buttonD.image.sprite = spellIcons[4];
            }
			else if((elements[0] == 2 && elements[1] == 3 && elements[2] == 3) || (elements[0] == 3 && elements[1] == 2 && elements[2] == 3) || (elements[0] == 3 && elements[1] == 3 && elements[2] == 2))
			{
				// WEE, EWE, EEW
				spellName = "Chaos Meteor";
                if (spells[1] != 6)
                    spells.Add(6);
                buttonD.image.sprite = spellIcons[5];
            }
			else if(elements[0] == 3 && elements[1] == 3 && elements[2] == 3)
			{
				// EEE
				spellName = "Sun Strike";
                if (spells[1] != 8)
                    spells.Add(7);
                buttonD.image.sprite = spellIcons[6];
            }	
			else if((elements[0] == 3 && elements[1] == 3 && elements[2] == 1) || (elements[0] == 3 && elements[1] == 1 && elements[2] == 3) || (elements[0] == 1 && elements[1] == 3 && elements[2] == 3))
			{
				// EEQ, EQE, QEE
				spellName = "Forge Spirit";
                if (spells[1] != 8)
                    spells.Add(8);
                buttonD.image.sprite = spellIcons[7];
            }
			else if((elements[0] == 3 && elements[1] == 1 && elements[2] == 1) || (elements[0] == 1 && elements[1] == 3 && elements[2] == 1) || (elements[0] == 1 && elements[1] == 1 && elements[2] == 3))
			{
				// EQQ, QEQ, QQE
				spellName = "Ice Wall";
                if (spells[1] != 9)
                    spells.Add(9);
                buttonD.image.sprite = spellIcons[8];
            }
			else
			{
				// QWE, QEW, WQE, WEQ, EQW, EWQ
				spellName = "Deafening Blast";
                if (spells[1] != 10)
                    spells.Add(10);
                buttonD.image.sprite = spellIcons[9];
            }

            if (spells.Count > 2){
				spells.RemoveAt(0);
			}

            // Incase there are more then 3 elements and Permutation is needed. Use 'Else' instead to save code.
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

            // Update button visuals afterhand
            if (spells[0] != 0 && buttonD.image.sprite != previousSprite && previousSprite != null){
                buttonF.image.sprite = previousSprite;
                buttonF.interactable = true;
            }
            buttonD.interactable = true;

            // Update Audio
            audioSource.PlayOneShot(invokeSound, 1);

			print("Invoked: " + spellName);
		}
	}

	// Enable & Disable the element gameobjects to visually represent element ints
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

	// Evaluate the spell number and call the method for the associated spell
	void castSpell(int spellNum)
	{
		switch (spellNum)
		{
			case 1:
				_cast.CastColdSnap();
				break;
			case 2:
                _cast.CastGhostWalk();
                break;
			case 3:
                _cast.CastTornado();
				break;
			case 4:
                _cast.CastEMP();
				break;
			case 5:
				_cast.CastAlacrity();
				break;
			case 6:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 7:
				print ("Ulg, glib, Pblblblblb");
				break;
			case 8:
				print("Cast: ");
				break;
			case 9:
				print("Cast: Forge Spirit");
				break;
			case 10:
				print ("Ulg, glib, Pblblblblb");
				break;
			default:
				print ("Invalid cast");
				break;
		}
	}

	// Accessors and Mutators
	public bool CanCast
	{
		get { return canCast; }
		set { canCast = value; }
	}
}
