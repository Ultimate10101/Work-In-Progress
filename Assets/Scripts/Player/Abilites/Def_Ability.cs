using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines basics needs for Player Abilities 

public abstract class Def_Ability : MonoBehaviour
{

    [SerializeField] protected Animator playerAnim;

    private StatusEffectHandler playerStatus;

    protected float castTimeLengthOffset;

    protected float inverseCastTimeLengthOffset;

    protected float coolDown;

    protected float inverseCoolDown;

    protected float manaCost;

    public float ManaCost
    {
        get { return manaCost; }

        set { manaCost = value; }
    }

    protected float inverseManaCost;

    public float InverseManaCost
    {
        get { return inverseManaCost; }
        set { inverseManaCost = value; }
    }
   
    protected bool readyToCast;

    public bool ReadyToCast
    {
        get { return readyToCast; }
    }


    protected bool readyToInverseCast;

    public bool InverseReadyToCast
    {
        get { return readyToInverseCast; }
    }


    protected P_ManaController playerMana;


    protected static bool abilityCurrentlyCasting;


    [SerializeField] protected AudioClip magicSFX;

    [SerializeField] protected AudioClip inverseMagicSFX;

    protected abstract void Cast();
    protected abstract void InverseCast();
    protected abstract void CastInput();
    protected abstract IEnumerator CastDelay();
    protected abstract IEnumerator InverseCastDelay();
    protected abstract IEnumerator CoolDownHandler();
    protected abstract IEnumerator InverseCoolDownHandler();



    protected virtual void Start()
    {
        playerStatus = gameObject.GetComponent<StatusEffectHandler>();

        Debug.Log("I am running notice me please");

        abilityCurrentlyCasting = false;
    }

    protected virtual void Update()
    {
        if (GameManager.gameManagerRef.GameMangerConditions() && !playerStatus.GetState("STUNNED"))
        {
            CastInput();
        }

        if(!P_DTLMenu.DTLMenuRef.Inverse)
        {
            Cast();
            Debug.Log("Inverse not toggled");
        }
        else
        {
            InverseCast();
            Debug.Log("Inverse toggled");
        }
    }

}
