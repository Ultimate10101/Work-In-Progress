using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Healing Spell for Player when inverse is not toggled
// Cure shot that targets Enemy and gives Player Mana when inverse is toggled

public class P_CureAbility : Def_Ability
{
    [SerializeField] private Camera gameCam;

    private GameObject enemy;

    private float heal;
    private float healDelay;
    private int  healAmount;

    private bool cureKey;

    [SerializeField] private ParticleSystem healingEffect;
    [SerializeField] private ParticleSystem takingDamageEffect;

    [SerializeField] private AnimationClip restoration;
    [SerializeField] private AnimationClip inverseRestoration;

    private float inverseDamage = 5.0f;

    public float InverseDamage
    {
        get { return inverseDamage; }
        set { inverseDamage = value; }
    }


    private P_HealthController playerHealth;

    void Start()
    {
        // Regular Magic variables

        coolDown = 20.0f;

        manaCost = 15.0f;

        healDelay = 2.0f;

        healAmount = 5;

        heal = 45;

        // Inverse Magic variables

        inverseCoolDown = 8.0f;

        inverseManaCost = 10.0f;


        readyToCast = true;
        readyToInverseCast = true;

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
  
            playerAnim.SetTrigger("IsHealing");

            StartCoroutine(CastDelay());

            AudioManager.audioManagerRef.PlaySFX(magicSFX);

            healingEffect.Play();
        }

    }

    protected override IEnumerator CastDelay()
    {
        yield return new WaitForSeconds(restoration.length);


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
        for (int i = 0; i < heal; i += healAmount)
        {
            yield return new WaitForSeconds(healDelay);

            playerHealth.HealHealth(healAmount);
        }
        StartCoroutine(CoolDownHandler());
    }



    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToInverseCast && cureKey && ((playerMana.Mana - inverseManaCost) >= 0.0f))
        {

            AudioManager.audioManagerRef.PlaySFX(inverseMagicSFX);

            Debug.Log("Casting Inverse Restoration");

            readyToInverseCast = false;
            abilityCurrentlyCasting = true;

            playerMana.Mana -= inverseManaCost;

            playerAnim.SetTrigger("IsUsingDot");

            takingDamageEffect.Play();

            StartCoroutine(InverseCastDelay());
        }
    }


    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(inverseRestoration.length);

        Debug.Log("Inverse Restoration Casted");

        abilityCurrentlyCasting = false;

        takingDamageEffect.Stop();

        if (IsTargetValid())
        {
            enemy.GetComponent<InverseRestorationDamage>().ApplyTicks(5, 1.5f, inverseDamage);

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

        readyToInverseCast = true;

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
