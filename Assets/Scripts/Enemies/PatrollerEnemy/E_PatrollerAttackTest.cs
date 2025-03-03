using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PatrollerAttackTest : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform attackPoint1;
    [SerializeField] private Transform attackPoint2;

    private E_AIMovementTest enemyMoveState;

    private bool canFire;

    [SerializeField] private float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;

        enemyMoveState = GetComponent<E_AIMovementTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMoveState.currentState == EnemyState.ATTACKING & canFire)
        {
            Debug.Log("fire");

            Attack();

            Invoke("ShotCounter", timeBetweenShots);
        }
    }

    void Attack()
    {
        canFire = false;

        GameObject projectile_ =  Instantiate(projectile, attackPoint1.position, Quaternion.identity);
        GameObject projectile_1 = Instantiate(projectile, attackPoint2.position, Quaternion.identity);

        projectile_.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        projectile_1.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);

    }

    void ShotCounter()
    {
        canFire = true;
    }
}
