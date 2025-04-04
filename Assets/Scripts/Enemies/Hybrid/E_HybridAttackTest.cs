using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HybridAttackTest : MonoBehaviour
{
    [SerializeField] private RUSH_DAMAGE specialDamageToggle;

    [SerializeField] private BoxCollider RushHitBox;

    private E_AIMovement enemyMoveState;

    private bool canAct;
    private bool specialReady;
    [SerializeField] private float timeUntilActAgain;

    private Animator anim;
    private Rigidbody HybridRb;

    // Start is called before the first frame update
    void Start()
    {
        canAct = true;

        specialReady = true;

        enemyMoveState = gameObject.GetComponent<E_AIMovement>();

        anim = GetComponent<Animator>();

        HybridRb = GetComponent<Rigidbody>();

        HybridRb.isKinematic = true;

        RushHitBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (enemyMoveState.currentState == EnemyState.ATTACKING)
        {
            if ((gameObject.GetComponent<E_HealthController>().Health <= 50.0f) && specialReady)
            {
                CancelInvoke("UntilCanAct");
                canAct = false;
                specialReady = false;

                //anim.SetTrigger("Berserk"); ---> To do once animations are set
                SpecialActivate();

                Invoke("UntilCanAct", 4.0f);

            }
            else if (canAct)
            {

                canAct = false;

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("H_Attack_Test"))
                {
                    
                    anim.SetTrigger("Rush");
                }

                Invoke("UntilCanAct", timeUntilActAgain);

            }
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


    void SpecialActivate()
    {
        timeUntilActAgain = 0.0f;
        specialDamageToggle.specialActivated = true;
       
    }

    void UntilCanAct()
    {
        canAct = true;
    }
}
