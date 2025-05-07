using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines basics needs for Player Abilities 

public abstract class Def_Ability : MonoBehaviour
{
    protected float castTime;

    protected float inverseCastTime;

    protected float coolDown;

    protected float inverseCoolDown;

    protected float manaCost;

    public float ManaCost
    {
        get { return manaCost; }

        set { manaCost = value; }
    }

    protected float inverseManaCost;
   
    protected bool readyToCast;


    protected P_ManaController playerMana;


    public static bool abilityCurrentlyCasting;


    protected abstract void Cast();
    protected abstract void InverseCast();
    protected abstract void CastInput();
    protected abstract IEnumerator CastDelay();
    protected abstract IEnumerator InverseCastDelay();
    protected abstract IEnumerator CoolDownHandler();
    protected abstract IEnumerator InverseCoolDownHandler();



    private void Start()
    {
        abilityCurrentlyCasting = false;
    }

    protected virtual void Update()
    {
        CastInput();

        //if (DevTool lite Menu option Inverse is not toggled)
        //{
            Cast();
        //}
        //else
        //{
            InverseCast();
        //}
            

    }

}
