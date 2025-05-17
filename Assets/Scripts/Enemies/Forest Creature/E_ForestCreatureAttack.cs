using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ForestCreatureAttack : E_EnemyAttack
{

    [SerializeField] BoxCollider hitBox;

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

            //anim.SetTrigger("ProtectMyself"); ---> To do once animations are set
            Heal();

            attackRate = 5.0f;

            Invoke("UntilCanAct", attackRate);

        }
    }


    protected override void BasicAttack()
    {
        if (canAct)
        {
            canAct = false;

            attackRate = 2.0f;

            if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig_Attack_Tr"))
            {
                enemyAnim.SetTrigger("Attack"); //---> Using Animation Event will tigger Ativate & Deactiveate HitBox
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
        gameObject.GetComponent<E_HealthController>().HealHealth(50.0f);
        Debug.Log("I'm getting Healed");
    }

    

}
