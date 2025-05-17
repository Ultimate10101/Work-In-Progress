using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PatrollerAttack : E_EnemyAttack
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform attackPoint1;
    [SerializeField] private Transform attackPoint2;

    [SerializeField] private GameObject specialAttack;


    protected override void SpecialAttack()
    {
        if ((gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
        {
            CancelInvoke("UntilCanAct");

            canAct = false;
            specialReady = false;

            attackRate = 5.0f;

            LaunchShockWave();

            Invoke("UntilCanAct", attackRate);

        }
    }


    protected override void BasicAttack()
    {
        if (canAct)
        {
            attackRate = 1.0f;

            canAct = false;

            if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig|Attack"))
            {
                enemyAnim.SetTrigger("Fire"); //---> Using Animation Event will tigger FireProjectile
            }
            Invoke("UntilCanAct", attackRate);

        }

    }

    void FireProjectile()
    {

        GameObject projectile_ = Instantiate(projectile, attackPoint1.position, Quaternion.identity);
        GameObject projectile_1 = Instantiate(projectile, attackPoint2.position, Quaternion.identity);

        projectile_.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        projectile_1.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);

    }

    void LaunchShockWave()
    {
        Instantiate(specialAttack, transform.position + new Vector3(0.0f, 1f), specialAttack.transform.rotation);
    }


}
