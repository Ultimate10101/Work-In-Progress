using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class E_AIMovement : MonoBehaviour
{
    public EnemyState currentState;

    protected NavMeshAgent navMeshAgent;
    [SerializeField] private float agrroRange;
    [SerializeField] private float attackRange;

    [SerializeField] protected float patrollStoppingDistance;
    [SerializeField] protected float attackStoppingDistance;

    [SerializeField] protected float moveSpeed;

    [SerializeField] private bool willPatrolDelay;
    protected float delayTime;


    [SerializeField] private Transform[] wayPoints;

    protected Vector3 target;
    protected Vector3 startPos;
    private int index;

    public bool wasHit;

    private StatusEffectHandler enemyStatus;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = EnemyState.PATROLLING;
        navMeshAgent.stoppingDistance = patrollStoppingDistance;

        navMeshAgent.speed = moveSpeed;

        startPos = transform.position;

        wasHit = false;

        delayTime = 4.0f;

        index = 0;

        target = wayPoints[index].position;

        enemyStatus = GetComponent<StatusEffectHandler>();
    }

    // Update is called once per framez
    void Update()
    {
        if (!enemyStatus.GetState("STUNNED"))
        {
            MoveState();
        }
        
    }


    protected virtual void MoveState()
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




    protected void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(P_PlayerController.playerControllerRef.gameObject.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
    }

    protected void PatrollingLogic()
    {
        if (willPatrolDelay && (delayTime > 0.0f))
        {
            delayTime -= Time.deltaTime;

            return;
        }

        GoToPoint();

        if (Vector3.Distance(gameObject.transform.position, target) <= 0.5f)
        {
            UpdateDestination();

            delayTime = 4.0f;
        }

        return;
    }

    protected void ChasingLogic()
    {
        navMeshAgent.destination = P_PlayerController.playerControllerRef.gameObject.transform.position;
    }

    protected void AttackLogic()
    {
        LookAtPlayer();
    }

    protected void UpdateDestination()
    {
        index++;


        if (index == wayPoints.Length)
        {
            index = 0;
        }

        target = wayPoints[index].position;
    }

    protected void GoToPoint()
    {
        navMeshAgent.destination = target;
    }

    protected bool PlayerInAgrroRange()
    {
        return (Vector3.Distance(gameObject.transform.position, P_PlayerController.playerControllerRef.gameObject.transform.position) <= agrroRange);
    }

    protected bool PlayerInAttackRange()
    {
        return (Vector3.Distance(gameObject.transform.position, P_PlayerController.playerControllerRef.gameObject.transform.position) <= attackRange);
    }

}
