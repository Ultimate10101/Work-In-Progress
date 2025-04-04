using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_FireboltAbility : Def_Ability
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject fireboltPrefab;
    [SerializeField] private Camera gameCam;

    [SerializeField] private float shootForce;

    private bool fireboltKey;

    // Start is called before the first frame update
    void Start()
    {

        castTime = 1f;

        coolDown = 3.0f;

        manaCost = 15.0f;

        readyToCast = true;

        playerMana = gameObject.GetComponent<P_ManaController>();
    }


    protected override void CastInput()
    {
        fireboltKey = Input.GetKeyDown(KeyCode.E);
    }

    protected override void Cast()
    {
        if (readyToCast && fireboltKey && ((playerMana.Mana - manaCost) >= 0.0f))
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

        Fire();

        StartCoroutine(CoolDownHandler());

        Debug.Log("Finished");
    }

    protected override IEnumerator CoolDownHandler()
    {
        yield return new WaitForSeconds(coolDown);
        readyToCast = true;
        Debug.Log("Ready to cast again");
    }


    void Fire()
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

            GameObject projectile = Instantiate(fireboltPrefab, attackPoint.position, fireboltPrefab.transform.rotation);

            projectile.transform.forward = shootDir.normalized;

            projectile.GetComponent<Rigidbody>().AddForce(shootDir.normalized * shootForce, ForceMode.Impulse);
    }

}