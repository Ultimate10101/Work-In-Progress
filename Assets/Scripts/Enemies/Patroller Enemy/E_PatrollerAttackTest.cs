using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PatrollerAttackTest : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform attackPoint1;
    [SerializeField] private Transform attackPoint2;

    [SerializeField] private GameObject specialAttack;

    private E_AIMovementTest enemyMoveState;

    private bool canFire;
    private bool specialReady;

    [SerializeField] private float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;

        specialReady = true;

        enemyMoveState = gameObject.GetComponent<E_AIMovementTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMoveState.currentState == EnemyState.ATTACKING && (gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
        {
            canFire = false;
            specialReady = false;

            SpecialAttack();

            Invoke("ShotCounter", 2.0f);

        } 
        else if (enemyMoveState.currentState == EnemyState.ATTACKING & canFire)
        {
            Debug.Log("fire");

            BaseAttack();

            Invoke("ShotCounter", timeBetweenShots);
        }
    }

    void BaseAttack()
    {
        canFire = false;

        GameObject projectile_ =  Instantiate(projectile, attackPoint1.position, Quaternion.identity);
        GameObject projectile_1 = Instantiate(projectile, attackPoint2.position, Quaternion.identity);

        projectile_.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        projectile_1.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);

    }

    void SpecialAttack()
    {
       Instantiate(specialAttack, transform.position + new Vector3(0.0f, 1f), specialAttack.transform.rotation);
    }

    void ShotCounter()
    {
        canFire = true;
    }
}
