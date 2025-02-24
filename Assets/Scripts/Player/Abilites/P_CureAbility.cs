using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Healing Spell for Player 

public class P_CureAbility : Def_Ability
{
    [SerializeField] private int healPercent;
    private float healPercentage;

    private bool cureKey;

    void Start()
    {
        castTime = 2.0f;

        coolDown = 4.0f;

        manaCost = 15.0f;

        readyToCast = true;

        playerMana = gameObject.GetComponent<P_ManaController>();

        healPercentage = healPercent / 100.0f;
    }

    void Update()
    {
        CastInput();

        Cast();
    }


    protected override void CastInput()
    {
        cureKey = Input.GetKeyDown(KeyCode.F);
    }

    protected override void Cast()
    {
        if (readyToCast && cureKey && ((playerMana.Mana - manaCost) >= 0.0f))
        {
            Debug.Log("Casting");
            readyToCast = false;
            playerMana.Mana -= manaCost;

            StartCoroutine(CastDelay());

        }
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(castTime);

        float heal = (gameObject.GetComponent<P_HealthController>().MaxHealth * healPercentage);

        gameObject.GetComponent<P_HealthController>().HealHealth(heal);

        StartCoroutine(CoolDownHandler());

        Debug.Log("Finished");
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Ready to cast agian");
    }
}
