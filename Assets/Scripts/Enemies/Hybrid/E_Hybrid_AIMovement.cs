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
                else if (wayPoints.Length != 0)
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

                // GO TO ATTACK STATE
                if (PlayerInAttackRange())
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
