using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ForestCreatureAttackTest : MonoBehaviour
{

    [SerializeField] BoxCollider hitBox;

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

        hitBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManagerRef.GameOver)
        {
            Heal();
            Attack();
        }
           
    }


    void Heal()
    {
        if ((gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
        {
            CancelInvoke("UntilCanAct");

            canAct = false;
            specialReady = false;

            //anim.SetTrigger("ProtectMyself"); ---> To do once animations are set
            SpecialActivate();

            Invoke("UntilCanAct", 5.0f);

        }
    }


    void Attack()
    {
        if (enemyMoveState.currentState == EnemyState.ATTACKING)
        {
            if (canAct)
            {
                canAct = false;

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig_Attack_Tr"))
                {

                    anim.SetTrigger("Attack");
                }
                Invoke("UntilCanAct", timeUntilActAgain);

            }
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

   
    void SpecialActivate()
    {
        gameObject.GetComponent<E_HealthController>().HealHealth(50.0f);
        Debug.Log("I'm getting Healed");
    }

    void UntilCanAct()
    {
        canAct = true;
    }

}
