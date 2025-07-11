using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HybridAttack : E_EnemyAttack
{
    [SerializeField] private RUSH_DAMAGE specialDamageToggle;

    [SerializeField] private BoxCollider RushHitBox;
    
    private Rigidbody HybridRb;

    private float rageActionDelay;

    // Start is called before the first frame update
    protected override void Start()
    {
        rageActionDelay = 3.0f;

        base.Start();

        HybridRb = GetComponent<Rigidbody>();

        HybridRb.isKinematic = true;

        RushHitBox.enabled = false;
    }



    protected override void SpecialAttack()
    {
        if ((gameObject.GetComponent<E_HealthController>().Health <= 50.0f) && specialReady)
        {
            CancelInvoke("UntilCanAct");
            canAct = false;
            specialReady = false;

            //anim.SetTrigger("Berserk"); ---> To do once animations are set
            RageActivate();

            Invoke("UntilCanAct", rageActionDelay);

        }
    }


    protected override void BasicAttack()
    {
        if (canAct)
        {
            attackRate = 2.0f;

            canAct = false;

            if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig_Attack_Tr"))
            {

                enemyAnim.SetTrigger("Rush");
            }

            Invoke("UntilCanAct", attackRate);

        }
    }

    void StartRush()
    {
        RushHitBox.enabled = true;
        HybridRb.isKinematic = false;
        HybridRb.AddForce(transform.forward * 50.0f, ForceMode.Force);
    }

    void EndRush()
    {
        RushHitBox.enabled = false;
        HybridRb.isKinematic = true;
    }


    void RageActivate()
    {
        attackRate = 0.0f;
        specialDamageToggle.specialActivated = true;
       
    }

}
