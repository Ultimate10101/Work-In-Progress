using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Healing Spell for Player when inverse is not toggled
// Cure shot that targets Enemy and gives Player Mana when inverse is toggled

public class P_CureAbility : Def_Ability
{
    [SerializeField] private Camera gameCam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject cureShotPrefab;

    private float shootForce;

    private P_HealthController playerHealth;

    [SerializeField] private int healPercent;
    private float healPercentage;

    private bool cureKey;

    void Start()
    {
        castTime = 2.0f;

        coolDown = 4.0f;

        manaCost = 15.0f;


        inverseCastTime = 0.5f;

        inverseCoolDown = 0.5f;

        inverseManaCost = 0.0f;

        readyToCast = true;

        playerMana = gameObject.GetComponent<P_ManaController>();
        playerHealth = gameObject.GetComponent<P_HealthController>();

        healPercentage = healPercent / 100.0f;


        shootForce = 10.0f;
    }


    protected override void CastInput()
    {
        cureKey = Input.GetKeyDown(KeyCode.F);
    }

    protected override void Cast()
    {
        if (!abilityCurrentlyCasting && readyToCast && cureKey && ((playerMana.Mana - manaCost) >= 0.0f))
        {
            Debug.Log("Casting");
            readyToCast = false;
            abilityCurrentlyCasting = true;
            playerMana.Mana -= manaCost;

            StartCoroutine(CastDelay());

        }
    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(castTime);

        abilityCurrentlyCasting = false;

        float heal = (playerHealth.MaxHealth * healPercentage);

        playerHealth.HealHealth(heal);

        StartCoroutine(CoolDownHandler());

        Debug.Log("Finished");
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Ready to cast agian");
    }


    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToCast && cureKey && ((playerMana.Mana - inverseManaCost) >= 0.0f))
        {
            readyToCast = false;
            abilityCurrentlyCasting = true;

            playerMana.Mana -= inverseManaCost;

            StartCoroutine(InverseCastDelay());
        }
    }


    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(inverseCastTime);

        abilityCurrentlyCasting = false;

        FireInverseCure();
        playerMana.ManaIncrease();

        StartCoroutine(InverseCoolDownHandler());
    }


    protected override IEnumerator InverseCoolDownHandler()
    {
        yield return new WaitForSeconds(inverseCoolDown);

        readyToCast = true;
    }



    void FireInverseCure()
    {
        // ray through middle of screen
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100.0f); // Point far away
        }

        Vector3 shootDir = targetPoint - attackPoint.position;

        GameObject projectile = Instantiate(cureShotPrefab, attackPoint.position, cureShotPrefab.transform.rotation);

        projectile.transform.forward = shootDir.normalized;

        projectile.GetComponent<Rigidbody>().AddForce(shootDir.normalized * shootForce, ForceMode.Impulse);
    }

}
