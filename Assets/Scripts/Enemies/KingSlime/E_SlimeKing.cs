using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SlimeKing : MonoBehaviour
{

    public enum SlimeBossStates
    {
        Idle,
        Attacking
    } 



    private float attackRange;

    SlimeBossStates currentBossState;


    public bool wasHit;



    // Start is called before the first frame update
    void Start()
    {
        wasHit = false;

        attackRange = 15.0f;

        currentBossState = SlimeBossStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        ActionState();
    }


    private void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(P_PlayerController.playerControllerRef.gameObject.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
    }

    private bool PlayerInAttackRange()
    {
        return (Vector3.Distance(gameObject.transform.position, P_PlayerController.playerControllerRef.gameObject.transform.position) <= attackRange);
    }

    private void ActionState()
    {
        switch (currentBossState)
        {
            case SlimeBossStates.Idle:

                if (PlayerInAttackRange() || wasHit)
                {
                    currentBossState = SlimeBossStates.Attacking;
                }

                break;

            case SlimeBossStates.Attacking:
                LookAtPlayer();

                if (!PlayerInAttackRange())
                {
                    SummonAdds();
                    currentBossState = SlimeBossStates.Idle;
                }
             
                break;
        }
    }


    private void SummonAdds()
    {

    }


}

