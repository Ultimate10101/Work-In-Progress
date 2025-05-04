using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assess [information viewer] Menu Option for Dev Tool lite Menu

public class P_AssessAbility: MonoBehaviour 
{
    [SerializeField] private Camera gameCam;

    private E_CanvasController enemyCanvas;

    private float currentTarget;
    private float previousTarget;

    private float launchTime;
    private float coolDown;
  

    private bool accessKey;
    private bool readyToActivate;

    private int duration;

    private StatusEffectHandler playerStatus;

    void Start()
    {
        launchTime = 0.5f;
        coolDown = 0.5f;


        readyToActivate = true;

        duration = 18;

        playerStatus = gameObject.GetComponent<StatusEffectHandler>();

    }


    void ActivateAssess()
    {
        accessKey = Input.GetKeyDown(KeyCode.G);
    }

    void Assess()
    {
        if (readyToActivate && accessKey)
        {
            Debug.Log("Casting");
            readyToActivate = false;

            StartCoroutine(CastDelay());
        }
    }

    private IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(launchTime);

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

    private IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToActivate = true;
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
