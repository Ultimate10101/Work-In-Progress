using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines basics needs for Player Abilities 

public abstract class Def_Ability : MonoBehaviour
{
    protected float castTime;
    protected float coolDown;

    protected float manaCost;
    
    protected bool targetSelf;
    protected bool readyToCast;


    protected P_ManaController playerMana;


    protected abstract void Cast();
    protected abstract void InverseCast();
    protected abstract void CastInput();
    protected abstract IEnumerator CastDelay();
    protected abstract IEnumerator CoolDownHandler();


    protected virtual void Update()
    {
        CastInput();

        //if (!Inverse)
        //{
            Cast();
        //}
        //else
        //{
            InverseCast();
        //}
            

    }

}
