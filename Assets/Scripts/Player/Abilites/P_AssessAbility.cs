using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assess [information viewer] Spell for Player

public class P_AssessAbility : Def_Ability
{
    [SerializeField] private Camera gameCam;

    private E_CanvasController enemyCanvas;

    private float currentTarget;
    private float previousTarget;

    private bool accessKey;

    private int duration;

    private StatusEffectHandler playerStatus;

    void Start()
    {
        castTime = 1.0f;

        coolDown = 0.5f;

        manaCost = 2.0f;

        readyToCast = true;

        duration = 18;

        playerMana = gameObject.GetComponent<P_ManaController>();

        playerStatus = gameObject.GetComponent<StatusEffectHandler>();

    }


    protected override void CastInput()
    {
        accessKey = Input.GetKeyDown(KeyCode.G);
    }

    protected override void Cast()
    {
        if (readyToCast && accessKey && ((playerMana.Mana - manaCost) >= 0.0f))
        {
            Debug.Log("Casting");
            readyToCast = false;
            P_ManageAbility.abilityCurrentlyCasting = true;
            playerMana.Mana -= manaCost;

            StartCoroutine(CastDelay());
        }
    }

    protected override void InverseCast()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(castTime);

        P_ManageAbility.abilityCurrentlyCasting = false;

        if (IsValidTarget()) 
        { 
            if(DurationBugFix())
            {
                enemyCanvas.CanvasActive(true);
                DurationHanlder();

                enemyCanvas = null;
                previousTarget = currentTarget;
            }
            
        }
        else
        {
            Debug.Log("Spell missed target");
        }


        PlayerStun();


        StartCoroutine(CoolDownHandler());

        Debug.Log("Finished");
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Ready to cast agian");
    }
    
    void DurationHanlder()
    {
        enemyCanvas.ActiveFor(duration);
    }

    // Return true if Player is looking at Target the Spell is usuable on 
    bool IsValidTarget()
    {
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Targeted");
            enemyCanvas = hit.collider.gameObject.GetComponent<E_CanvasController>();

            currentTarget = enemyCanvas.GetInstanceID();

            return true;
        }
        else
        {
            Debug.Log("Nothing was hit");

            return false;
        }
    }


    // If access spell is already active on target, spell can't be casted on them again
    bool DurationBugFix()
    {
        if(currentTarget == previousTarget)
        {
            if(enemyCanvas.isActive)
            {
                Debug.Log("Still Running");
                return false;
            }
            return true;
        }
        return true;
    }

    void PlayerStun()
    {
        playerStatus.currentStatusEffect = StatusEffectHandler.StatusEffects.STUNNED;
        playerStatus.playerIsStunned = true;
    }


}
