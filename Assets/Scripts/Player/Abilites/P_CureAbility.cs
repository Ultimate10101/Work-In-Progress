using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Healing Spell for Player when inverse is not toggled
// Cure shot that targets Enemy and gives Player Mana when inverse is toggled

public class P_CureAbility : Def_Ability
{
    [SerializeField] private Camera gameCam;

    private GameObject enemy;

    private P_HealthController playerHealth;

    [SerializeField] private int healPercent;
    private float healPercentage;

    private float healAmount;

    private float heal;

    private float healDelay;

    private bool cureKey;

    [SerializeField] private ParticleSystem healingEffect;

    void Start()
    {
        // Regular Magic variables
        castTimeLengthOffset = 0.0f;

        coolDown = 20.0f;

        manaCost = 15.0f;

        healDelay = 2.0f;

        healPercentage = healPercent / 100.0f;

        // Inverse Magic variables
        inverseCastTimeLengthOffset = 0.5f;

        inverseCoolDown = 0.5f;

        inverseManaCost = 0.0f;


        readyToCast = true;

        playerMana = gameObject.GetComponent<P_ManaController>();
        playerHealth = gameObject.GetComponent<P_HealthController>();
    }


    protected override void CastInput()
    {
        cureKey = Input.GetKeyDown(KeyCode.F);
    }

    protected override void Cast()
    {
        if (!abilityCurrentlyCasting && readyToCast && cureKey && ((playerMana.Mana - manaCost) >= 0.0f))
        {
            Debug.Log("Casting Restoration");
            readyToCast = false;
            abilityCurrentlyCasting = true;
            playerMana.Mana -= manaCost;

            heal = (playerHealth.MaxHealth * healPercentage);
            healAmount = heal / 8;

            playerAnim.SetTrigger("IsHealing");

            StartCoroutine(CastDelay());

            healingEffect.Play();
        }

    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length - castTimeLengthOffset);

        healingEffect.Stop();

        Debug.Log("Restoration Casted");

        abilityCurrentlyCasting = false;

        StartCoroutine(HealOverTime());
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Restoration cooldown recharged");
    }

    private IEnumerator HealOverTime()
    {
        for (float i = 0.0f; i < heal; i += healAmount)
        {
            yield return new WaitForSeconds(healDelay);

            playerHealth.HealHealth(healAmount);
        }
        StartCoroutine(CoolDownHandler());
    }



    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToCast && cureKey && ((playerMana.Mana - inverseManaCost) >= 0.0f))
        {
            Debug.Log("Casting Inverse Restoration");

            readyToCast = false;
            abilityCurrentlyCasting = true;

            playerMana.Mana -= inverseManaCost;

            playerAnim.SetTrigger("IsUsingDot");

            StartCoroutine(InverseCastDelay());
        }
    }


    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length - inverseCastTimeLengthOffset);

        Debug.Log("Inverse Restoration Casted");

        abilityCurrentlyCasting = false;

        if (IsTargetValid())
        {
            enemy.GetComponent<InverseRestorationDamage>().ApplyTicks(5, 1.5f, 4);

            if (enemy.GetComponent<E_AIMovement>().currentState == EnemyState.PATROLLING)
            {
                enemy.GetComponent<E_AIMovement>().wasHit = true;
            }

        }
        StartCoroutine(InverseCoolDownHandler());
    }


    protected override IEnumerator InverseCoolDownHandler()
    {
        yield return new WaitForSeconds(inverseCoolDown);

        readyToCast = true;

        Debug.Log("Inverse Restoration cooldown recharged");
    }


    private bool IsTargetValid()
    {
        Ray ray = gameCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        Physics.Raycast(ray, out hit, 30.0f);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Targeted for Inverse Restoration");

            enemy = hit.collider.gameObject;

            return true;
        }
        Debug.Log(" No Enemy Targeted for Inverse Restoration");
        return false;
    }

}
