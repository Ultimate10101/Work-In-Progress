using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ForestCreatureAttackTest : MonoBehaviour
{

    [SerializeField] BoxCollider chompHitBox;

    private E_AIMovement enemyMoveState;

    private bool canAct;
    private bool specialReady;
    [SerializeField] private float timeUntilActAgain;

    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        canAct = true;

        specialReady = true;

        enemyMoveState = gameObject.GetComponent<E_AIMovement>();

        anim = GetComponent<Animator>();

        chompHitBox.enabled = false;
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
            if ((gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
            {
                CancelInvoke("UntilCanAct");
                specialReady = false;

                Invoke("UntilCanAct", 5.0f);

            }
            else if (canAct)
            {
                canAct = false;

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("FC_Attack_Test"))
                {
                    anim.SetTrigger("Attack");
                }

                Invoke("UntilCanAct", timeUntilActAgain);

            }
        }
        
    }

    void ActivateHitBox()
    {
        chompHitBox.enabled = true;
    }

    void DeactivateHitBox()
    {
        chompHitBox.enabled = false;
    }

   
    void SpecialAttack()
    {

    }

    void UntilCanAct()
    {
        canAct = true;
    }

}
