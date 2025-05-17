using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HybridAttack : E_EnemyAttack
{
    [SerializeField] private RUSH_DAMAGE specialDamageToggle;

    [SerializeField] private BoxCollider RushHitBox;

    private Rigidbody hybridRb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        hybridRb = GetComponent<Rigidbody>();

        hybridRb.isKinematic = true;

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
            SpecialAttack();

            attackRate = 4.0f;

            Invoke("UntilCanAct", attackRate);

        }
    }


    protected override void BasicAttack()
    {

        if (canAct)
        {
            canAct = false;

            attackRate = 5.0f;

            if (!enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig_Attack_Tr"))
            {

                enemyAnim.SetTrigger("Rush"); //---> Using Animation Event will tigger Start Rush & End Rush
            }

            Invoke("UntilCanAct", attackRate);

        }
    }



    void StartRush()
    {
        RushHitBox.enabled = true;
        hybridRb.isKinematic = false;
        hybridRb.AddForce(transform.forward * 50.0f, ForceMode.Force);
    }

    void EndRush()
    {
        RushHitBox.enabled = false;
        hybridRb.isKinematic = true;
    }


    void Rage()
    {
        attackRate = 0.0f;
        specialDamageToggle.specialActivated = true;

    }

}
