using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_SlimeKingAttack : E_EnemyAttack
{
    [SerializeField] private GameObject windWave;

    [SerializeField] private GameObject minions; 

    [SerializeField] private GroundCheck slimeGroundCheck;

    [SerializeField] private Transform[] spawnPoints;

    private Rigidbody slimeKingRb;

    private bool readToSummon;



    protected override void Start()
    {
        base.Start();

        slimeKingRb = GetComponent<Rigidbody>();

        slimeKingRb.isKinematic = true;

        readToSummon = true;

    }



    protected override void Update()
    {
        base.Update();


        if (slimeGroundCheck.HitGround)
        {
            slimeGroundCheck.HitGround = false;

            slimeGroundCheck.gameObject.SetActive(false);

            LaunchAirWave();

            slimeKingRb.isKinematic = true;

            gameObject.GetComponent<NavMeshAgent>().enabled = true;

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
            attackRate = 1.0f;

            canAct = false;

            slimeKingRb.isKinematic = false;

            gameObject.GetComponent<NavMeshAgent>().enabled = false;


            slimeKingRb.AddForce(Vector3.up * 20.0f, ForceMode.Impulse);

            StartCoroutine(GroundCheckEnableDelay());

        }

    }

    IEnumerator GroundCheckEnableDelay()
    {
        yield return new WaitForSeconds(1.0f);

        slimeGroundCheck.gameObject.SetActive(true);
    }


    private void LaunchAirWave()
    {
        Instantiate(windWave, transform.position + new Vector3(0.0f, -1f), windWave.transform.rotation);
    }


    public void SummonAdds()
    {
        if (readToSummon)
        {
            readToSummon = false;

            for (int i = 0; i < 5; i++)
            {
                Instantiate(minions, RandomPos(), minions.transform.rotation);
            }

            StartCoroutine(CoolDown());
        }
        
    }


    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(10.0f);
        readToSummon = true;
    }

    private Vector3 RandomPos()
    {
       Vector3 pos = Vector3.zero;

       Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];


        float xPos = spawnPoint.position.x;
        float zPos = spawnPoint.position.z;

        pos = new Vector3(xPos, minions.transform.position.y, zPos);

        return pos;


    }
}
