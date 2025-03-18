using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_Hybrid_AIMovement : E_AIMovement
{

    protected override void MoveState()
    {
        switch (currentState)
        {
            case EnemyState.PATROLLING:

                // PLAYER TAUNT
                if (PlayerInAgrroRange())
                {
                    PlayerTaunt();
                }
                // PATROL
                else
                {
                    PatrollingLogic();
                }

                // GO TO CHASE STATE
                if (wasHit)
                {
                    currentState = EnemyState.CHASING;

                    navMeshAgent.stoppingDistance = attackStoppingDistance;

                    wasHit = false;
                }
                // GO TO ATTACK STATE
                else if (PlayerInAttackRange())
                {
                    navMeshAgent.speed = 0f;
                    currentState = EnemyState.ATTACKING;
                }

                break;

            case EnemyState.CHASING:

                ChasingLogic();

                // GO TO PATROLLING STATE
                if ((Vector3.Distance(gameObject.transform.position, startPos) > 35.0f))
                {
                    currentState = EnemyState.PATROLLING;

                    navMeshAgent.stoppingDistance = patrollStoppingDistance;

                    transform.position = startPos;
                    Debug.Log("Back to Patrolling");
                }
                // GO TO ATTACK STATE
                else if (PlayerInAttackRange())
                {
                    navMeshAgent.speed = 0f;
                    currentState = EnemyState.ATTACKING;
                }

                break;

            case EnemyState.ATTACKING:
                AttackLogic();

                // GO TO PATROLLING STATE
                if (!PlayerInAttackRange())
                {
                    navMeshAgent.speed = moveSpeed;
                    currentState = EnemyState.PATROLLING;

                    navMeshAgent.stoppingDistance = patrollStoppingDistance;
                }
                break;
        }
    }

    void PlayerTaunt()
    {
        LookAtPlayer();
        // Plays Taunt Animation
        Debug.Log("Come Closer LOSER");
    }
}
