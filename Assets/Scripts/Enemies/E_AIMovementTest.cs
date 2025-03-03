using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class E_AIMovementTest : MonoBehaviour
{
    public EnemyState currentState;

    private NavMeshAgent navMeshAgent;
    private float agrroRange = 15.0f;
    private float attackRange = 6.5f;

    private float patrollStoppingDistance = 0.0f;
    private float attackStoppingDistance = 6.0f;


    [SerializeField] Transform[] wayPoints;

    private Vector3 target;
    private Vector3 startPos;
    int index;

    public bool wasHit;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = EnemyState.PATROLLING;
        navMeshAgent.stoppingDistance = 0;

        navMeshAgent.speed = 8f;

        startPos = transform.position;

        wasHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.PATROLLING:
                PatrollignLogic();

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
                    navMeshAgent.speed = 8f;
                    currentState = EnemyState.PATROLLING;

                    navMeshAgent.stoppingDistance = patrollStoppingDistance;
                }
                break;
        }      
    }

    void LookAtPlayer()
    {
        Vector3 lookAt = new Vector3(P_PlayerController.playerControllerRef.gameObject.transform.position.x, gameObject.transform.position.y, P_PlayerController.playerControllerRef.gameObject.transform.position.z);
        transform.LookAt(lookAt);
    }

    void PatrollignLogic()
    {
        GoToPoint();

        if (Vector3.Distance(gameObject.transform.position, target) <= 0.3)
        {
            UpdateDestination();
        }
    }

    void ChasingLogic()
    {

        navMeshAgent.destination = P_PlayerController.playerControllerRef.gameObject.transform.position;
    }

    void AttackLogic()
    {
        LookAtPlayer();
    }

    void UpdateDestination()
    {
        index++;

        if (index == wayPoints.Length)
        {
            index = 0;
        }
    }

    void GoToPoint()
    {
        target = wayPoints[index].position;
        navMeshAgent.SetDestination(target);
    }

    bool PlayerInAgrroRange()
    {
        return (Vector3.Distance(gameObject.transform.position, P_PlayerController.playerControllerRef.gameObject.transform.position) <= agrroRange);
    }

    bool PlayerInAttackRange()
    {
        return (Vector3.Distance(gameObject.transform.position, P_PlayerController.playerControllerRef.gameObject.transform.position) <= attackRange);
    }

}
