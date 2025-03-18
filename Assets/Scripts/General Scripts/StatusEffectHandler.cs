using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public enum StatusEffects
    {
        NEUTRAL,
        BURNING,
        STUNNED
    }

    public StatusEffects currentStatusEffect;

    public bool playerIsStunned;

    private List<int> burnTimer = new List<int>();


    void Start()
    {
        currentStatusEffect = StatusEffects.NEUTRAL;
    }

    void Update()
    {
        //Activates necessary status effects
        switch (currentStatusEffect)
        {
            case StatusEffects.BURNING:

                ApplyBurn();

                currentStatusEffect = StatusEffects.NEUTRAL;

                break;

            case StatusEffects.STUNNED:

                if(gameObject.CompareTag("Player"))
                {
                    if (playerIsStunned)
                    {
                        StartCoroutine(PlayerCanMove());
                        playerIsStunned = false;
                    }
                }
                
                if(gameObject.CompareTag("Enemy"))
                {

                }

                break;


        }
    }

    //Applies burning to an enemy hit by firebolt

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Firebolt_Projectile"))
        {
            currentStatusEffect = StatusEffects.BURNING;
        }
    }

    private void ApplyBurn()
    {
        if (burnTimer.Count <= 0)
        {
            burnTimer.Add(3);
            StartCoroutine(Burn());
        }
        else
        {
            burnTimer.Add(3);
        }

    }

    IEnumerator Burn()
    {
        while (burnTimer.Count > 0)
        {
            for (int i = 0; i < burnTimer.Count; i++)
            {
                burnTimer[i]--;
            }
            yield return new WaitForSeconds(1.0f);
            gameObject.GetComponent<E_HealthController>().TakeDamage(5);
            burnTimer.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator PlayerCanMove()
    {
        yield return new WaitForSeconds(3);
        currentStatusEffect = StatusEffects.NEUTRAL;
    }


}
