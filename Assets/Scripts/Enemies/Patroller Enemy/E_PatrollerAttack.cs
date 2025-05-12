using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PatrollerAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform attackPoint1;
    [SerializeField] private Transform attackPoint2;

    [SerializeField] private GameObject specialAttack;

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

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManagerRef.GameOver)
        {
            Attack();
        }
    }



    void Attack()
    {

        if (enemyMoveState.currentState == EnemyState.ATTACKING)
        {
            if ((gameObject.GetComponent<E_HealthController>().Health <= 40.0f) && specialReady)
            {
                CancelInvoke("UntilCanAct");

                canAct = false;
                specialReady = false;

                //anim.SetTrigger("AllOrNothing"); ---> To do once animations are set
                SpecialAttack();

                Invoke("UntilCanAct", 5.0f);

            }
            else if (canAct)
            { 
                canAct = false;

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Rig|Attack"))
                {
                    anim.SetTrigger("Fire");
                }
                Invoke("UntilCanAct", timeUntilActAgain);

            }
        }
        
        
    }

    void BaseAttack()
    {

        GameObject projectile_ =  Instantiate(projectile, attackPoint1.position, Quaternion.identity);
        GameObject projectile_1 = Instantiate(projectile, attackPoint2.position, Quaternion.identity);

        projectile_.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
        projectile_1.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);

    }

    void SpecialAttack()
    {
       Instantiate(specialAttack, transform.position + new Vector3(0.0f, 1f), specialAttack.transform.rotation);
    }

    void UntilCanAct()
    {
        canAct = true;
    }
}
