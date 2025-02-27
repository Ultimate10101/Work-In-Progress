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

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.PATROLLING:
                PatrollignLogic();

                if (PlayerInAgrroRange() || wasHit) 
                {
                    currentState = EnemyState.CHASING;

                    wasHit = false;
                }
                break;

            case EnemyState.CHASING:
                ChasingLogic();

                if ((Vector3.Distance(gameObject.transform.position, startPos) > 35.0f))
                {
                    currentState = EnemyState.PATROLLING;
                    Debug.Log("Back to Patrolling");
                }
                else if (PlayerInAttackRange())
                {
                    navMeshAgent.speed = 0f;
                    currentState = EnemyState.ATTACKING;
                }
                break;

            case EnemyState.ATTACKING:
                AttackLogic();

                if (!PlayerInAttackRange())
                {
                    navMeshAgent.speed = 8f;
                    currentState = EnemyState.PATROLLING;
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
        navMeshAgent.stoppingDistance = 6.5f;
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
        navMeshAgent.stoppingDistance = 0;
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
