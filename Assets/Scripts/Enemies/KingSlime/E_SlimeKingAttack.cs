using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_SlimeKingAttack : E_EnemyAttack
{
    [SerializeField] private GameObject windWave;

    [SerializeField] private GroundCheck slimeGroundCheck;

    private Rigidbody slimeKingRb;


    protected override void Start()
    {
        base.Start();

        slimeKingRb = GetComponent<Rigidbody>();

        slimeKingRb.isKinematic = true;

    }



    protected override void Update()
    {
        base.Update();


        if (slimeGroundCheck.HitGround)
        {
            slimeGroundCheck.HitGround = false;

            slimeGroundCheck.gameObject.SetActive(false); 

            Debug.Log("Five Big Booms baby");

            LaunchShockWave();

            slimeKingRb.isKinematic = true;

            GetComponent<NavMeshAgent>().enabled = true;

            Invoke("UntilCanAct", attackRate);


        }

    }


    protected override void SpecialAttack()
    {
        if ((gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
        {
            CancelInvoke("UntilCanAct");

            canAct = false;
            specialReady = false;

            attackRate = 5.0f;


            Invoke("UntilCanAct", attackRate);

        }
    }


    protected override void BasicAttack()
    {
        if (canAct)
        {
            Debug.Log("Up We GOOOOOO");

            attackRate = 1.0f;

            canAct = false;

            slimeKingRb.isKinematic = false;

            GetComponent<NavMeshAgent>().enabled = false;


            slimeKingRb.AddForce(Vector3.up * 20.0f, ForceMode.Impulse);

            StartCoroutine(GroundCheckEnableDelay());

        }

    }

    IEnumerator GroundCheckEnableDelay()
    {
        yield return new WaitForSeconds(1.0f);

        slimeGroundCheck.gameObject.SetActive(true);
    }


    private void LaunchShockWave()
    {

        Debug.Log("I HIT THE GROUND ARRRRRRGGGGG");
        //Instantiate(windWave, transform.position + new Vector3(0.0f, 1f), windWave.transform.rotation);
    }


    public void SummonAdds()
    {

    }
}
