using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_SlimeKingMovement : E_AIMovement
{

    protected override void MoveState()
    {
        switch (currentState)
        {
            case EnemyState.PATROLLING:

                if(wayPoints.Length != 0)
                {
                    PatrollingLogic();
                }
                    

                // GO TO CHASE STATE
                if (PlayerInAgrroRange() || wasHit)
                {
                    currentState = EnemyState.CHASING;

                    navMeshAgent.stoppingDistance = attackStoppingDistance;

                    wasHit = false;
                }
                break;

            case EnemyState.CHASING:

                ChasingLogic();

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

                    Debug.Log("X-Men To Me");

                    gameObject.GetComponent<E_SlimeKingAttack>().SummonAdds();

                    navMeshAgent.stoppingDistance = patrollStoppingDistance;
                }
                break;
        }
    }

}
