using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    // VARIABLES
    private float health = 100;
    private float maxHealth = 100;
    private float mana = 100;
    private float maxMana = 100;

    void Start()
    {
        health = maxHealth;
        mana = maxMana;
    }

    public void TakeDamage(float dmg)
    {
        BroadcastMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        AdjustHP(dmg);
    }

    public void AdjustHP(float dmg)
    {
        health += dmg;

        if (health > maxHealth)
            health = maxHealth;
        else if (health <= 0)
        {
            //Die();
        }
    }

    public void AdjustMP(float amt)
    {
        mana += amt;

        if (mana > maxMana)
            mana = maxMana;
        else if (mana <= 0)
        {
            mana = 0;
        }
    }

    // Accessors and Mutators
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public float Mana
    {
        get { return mana; }
        set { mana = value; }
    }
    public float MaxMana
    {
        get { return maxMana; }
        set { maxMana = value; }
    }
}
