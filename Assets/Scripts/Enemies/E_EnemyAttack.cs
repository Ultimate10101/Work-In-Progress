using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_EnemyAttack : MonoBehaviour
{

    protected E_AIMovement enemyMoveState;

    protected bool canAct;
    protected bool specialReady;
    [SerializeField] protected float attackRate;

    protected Animator enemyAnim;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        canAct = true;

        specialReady = true;

        enemyMoveState = gameObject.GetComponent<E_AIMovement>();

        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!GameManager.gameManagerRef.GameOver && enemyMoveState.currentState == EnemyState.ATTACKING)
        {
            SpecialAttack();
            BasicAttack();
        }
    }



    void UntilCanAct()
    {
        canAct = true;
    }


    protected virtual void BasicAttack() { }
    protected virtual void SpecialAttack() { }

}
