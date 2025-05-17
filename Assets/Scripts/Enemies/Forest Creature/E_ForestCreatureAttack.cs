using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ForestCreatureAttackTest : E_EnemyAttack
{

    [SerializeField] private BoxCollider hitBox;

    [SerializeField] private GameObject healEffect;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        hitBox.enabled = false;
    }



    protected override void SpecialAttack()
    {
        if ((gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
        {
            CancelInvoke("UntilCanAct");

            canAct = false;
            specialReady = false;

            attackRate = 5.0f;

            //anim.SetTrigger("ProtectMyself"); ---> To do once animations are set
            Heal();

            Invoke("UntilCanAct", attackRate);

        }
    }


    protected override void BasicAttack()
    {
        if (canAct)
        {
            attackRate = 0.0f;

            canAct = false;

            if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig_Attack_Tr"))
            {

                enemyAnim.SetTrigger("Attack");
            }
            Invoke("UntilCanAct", attackRate);

        }
    }

    void ActivateHitBox()
    {
        hitBox.enabled = true;
    }

    void DeactivateHitBox()
    {
        hitBox.enabled = false;
    }


    void Heal()
    {
        Instantiate(healEffect, transform.position, healEffect.transform.rotation);
        gameObject.GetComponent<E_HealthController>().HealHealth(50.0f);
        Debug.Log("I'm getting Healed");
    }

}
