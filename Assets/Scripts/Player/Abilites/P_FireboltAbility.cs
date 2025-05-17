using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FireboltAbility : Def_Ability
{
    [SerializeField] private Camera gameCam;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject fireboltPrefab;
    [SerializeField] private GameObject ArcaneShotPrefab;


    [SerializeField] private AnimationClip fireBolt;
    [SerializeField] private AnimationClip arcaneShot;

    [SerializeField] private float shootForce;

    private bool fireboltKey;
    private bool arcaneShotKey;

    private float damageToSelf = 15.0f;

    P_HealthController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Regular Magic variables

        coolDown = 5.0f;

        manaCost = 20.0f;


        // Inverse Magic variables

        inverseCoolDown = 0.5f;

        inverseManaCost = 0.0f;


        readyToCast = true;
        readyToInverseCast = true;

        playerMana = gameObject.GetComponent<P_ManaController>();
        playerHealth = gameObject.GetComponent<P_HealthController>();
    }


    protected override void CastInput()
    {
        fireboltKey = Input.GetKeyDown(KeyCode.Mouse0);

        arcaneShotKey = Input.GetKey(KeyCode.Mouse0);
    }

    protected override void Cast()
    {
        if (!abilityCurrentlyCasting && readyToCast && fireboltKey && ((playerMana.Mana - manaCost) >= 0.0f))
        {
            Debug.Log("Casting Firebolt");
            readyToCast = false;
            abilityCurrentlyCasting = true;
            playerMana.Mana -= manaCost;

            playerAnim.SetTrigger("IsFireBolting");

            StartCoroutine(CastDelay());

        }
    }

    protected override IEnumerator CastDelay()
    {

        yield return new WaitForSeconds(fireBolt.length);

        AudioManager.audioManagerRef.PlaySFX(magicSFX);

        Debug.Log("Firebolt Casted");

        abilityCurrentlyCasting = false;

        playerHealth.TakeDamage(damageToSelf);

        Fire();

        StartCoroutine(CoolDownHandler());
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Firebolt cooldown recharged");
    }




    // Inverse of Ability

    protected override void InverseCast()
    {
        if (!abilityCurrentlyCasting && readyToInverseCast && arcaneShotKey && ((playerMana.Mana - inverseManaCost) >= 0.0f))
        {
            readyToInverseCast = false;
            abilityCurrentlyCasting = true;

            Debug.Log("Casting ArcaneShot");

            playerMana.Mana -= inverseManaCost;

            playerAnim.SetTrigger("IsArcaneShooting");


            StartCoroutine(InverseCastDelay());
        }

    }

    protected override IEnumerator InverseCastDelay()
    {
        yield return new WaitForSeconds(arcaneShot.length/2);

        AudioManager.audioManagerRef.PlaySFX(inverseMagicSFX);

        Debug.Log("ArcaneShot casted");

        abilityCurrentlyCasting = false;

        Fire();

        StartCoroutine(InverseCoolDownHandler());
    }

    protected override IEnumerator InverseCoolDownHandler()
    {
        yield return new WaitForSeconds(inverseCoolDown);

        readyToInverseCast = true;

        Debug.Log("ArcaneShot cooldown recharged");
    }

    private void Fire()
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

        GameObject projectile;

        if (!P_DTLMenu.DTLMenuRef.Inverse)
        {
            projectile = Instantiate(fireboltPrefab, attackPoint.position, fireboltPrefab.transform.rotation);
        }
        else
        {
            projectile = Instantiate(ArcaneShotPrefab, attackPoint.position, fireboltPrefab.transform.rotation);
        }

        projectile.transform.forward = shootDir.normalized;

        projectile.GetComponent<Rigidbody>().AddForce(shootDir.normalized * shootForce, ForceMode.Impulse);
    }

}