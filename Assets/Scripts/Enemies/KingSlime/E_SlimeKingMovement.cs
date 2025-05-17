public class E_SlimeKingMovement : E_AIMovement
{

    protected override void MoveState()
    {
        switch (currentState)
        {
            case EnemyState.PATROLLING:
                PatrollingLogic();

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

                    navMeshAgent.stoppingDistance = patrollStoppingDistance;
                }
                break;
        }
    }

}
